using Data;
using Domain.Model;
using DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using System.Diagnostics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Shared.Types;

namespace Application.Services
{
    public class UserService
    {
        private readonly string _outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedReports");
        private readonly UserRepository _userRepository;
        private readonly CourseRepository _courseRepository;
        private readonly EnrollmentRepository _enrollmentRepository;
        private readonly SubjectRepository _subjectRepository;

        public UserService(UserRepository userRepository, CourseRepository courseRepository, SubjectRepository subjectRepository, EnrollmentRepository enrollmentRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _enrollmentRepository = enrollmentRepository;
            QuestPDF.Settings.License = LicenseType.Community;
            if (!Directory.Exists(_outputDirectory))
                Directory.CreateDirectory(_outputDirectory);
        }
        public async Task<UserDTO> AddAsync(UserCreateDTO createDto)
        {

            User user = new(0,
                createDto.UserName,
                createDto.Password,
                createDto.Name,
                createDto.LastName,
                createDto.Email,
                createDto.Adress,
                createDto.TypeUser,
                createDto.Dni,
                createDto.TypeUser == UserType.Student ? createDto.StudentNumber : null,
                createDto.TypeUser == UserType.Teacher ? createDto.JobPosition : null,
                createDto.TypeUser == UserType.Student ? createDto.DateOfAdmission : null,
                createDto.TypeUser == UserType.Teacher ? createDto.DateOfHire : null
            );

            if (!string.IsNullOrWhiteSpace(createDto.Dni))
                user.SetDni(createDto.Dni);

            if (createDto.DateOfAdmission.HasValue && createDto.TypeUser == UserType.Student)
                user.SetDateOfAdmission(createDto.DateOfAdmission.Value);

            if (createDto.DateOfHire.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetDateOfHire(createDto.DateOfHire.Value);

            if (createDto.JobPosition.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetJobPosition(createDto.JobPosition.Value);

            if (!string.IsNullOrWhiteSpace(createDto.StudentNumber) && createDto.TypeUser == UserType.Student)
                user.SetStudentNumber(createDto.StudentNumber);

            await _userRepository.AddAsync(user);

            return MapToDTO(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var specialty = await _userRepository.GetAsync(id);
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDTO?> GetAsync(int id)
        {
            User? user = await _userRepository.GetAsync(id);

            if (user == null)
                return null;

            return MapToDTO(user);
        }
   
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var usuarios = await _userRepository.GetAllAsync();

            return usuarios.Select(user => MapToDTO(user));
        }
        public async Task<bool> UpdateAsync(UserUpdateDTO updateDto)
        {
            var usuario = await _userRepository.GetAsync(updateDto.Id);
            if (usuario == null)
                return false;

            usuario.SetUserName(updateDto.UserName);
            usuario.SetName(updateDto.Name);
            usuario.SetLastName(updateDto.LastName);
            usuario.SetEmail(updateDto.Email);
            usuario.SetStatus(updateDto.Status);
            usuario.SetTypeUser(updateDto.TypeUser);

            if (!string.IsNullOrWhiteSpace(updateDto.Dni))
                usuario.SetDni(updateDto.Dni);

            if (!string.IsNullOrWhiteSpace(updateDto.StudentNumber))
                usuario.SetStudentNumber(updateDto.StudentNumber);

            if (!string.IsNullOrWhiteSpace(updateDto.Adress))
                usuario.SetAdress(updateDto.Adress);

            if (updateDto.DateOfAdmission.HasValue)
                usuario.SetDateOfAdmission(updateDto.DateOfAdmission.Value);

            if (updateDto.DateOfHire.HasValue)
                usuario.SetDateOfHire(updateDto.DateOfHire.Value);

            if (updateDto.JobPosition.HasValue)
                usuario.SetJobPosition(updateDto.JobPosition.Value);

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
                usuario.SetPassword(updateDto.Password);

            return await _userRepository.UpdateAsync(usuario);
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            if (userId <= 0) throw new ArgumentException("UserId inválido", nameof(userId));
            if (string.IsNullOrWhiteSpace(currentPassword)) throw new ArgumentException("La contraseña actual es obligatoria.", nameof(currentPassword));
            if (string.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException("La nueva contraseña no puede estar vacía.", nameof(newPassword));

            var userEntity = await _userRepository.GetAsync(userId);
            if (userEntity == null)
                return false;

            if (!userEntity.ValidatePassword(currentPassword))
                return false; 

            return await _userRepository.UpdatePasswordAsync(userId, newPassword);
        }
        private UserDTO MapToDTO(User user) => new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Dni = user.Dni,
            StudentNumber = user.StudentNumber,
            Adress = user.Adress,
            TypeUser = user.TypeUser,
            JobPosition = user.JobPosition,
            DateOfAdmission = user.DateOfAdmission,
            DateOfHire = user.DateOfHire,
            Status = user.Status
        };

        public async Task<byte[]> GenerateUsersGradesReportAsync(bool onlyStudents = true)
        {
            var users = (await _userRepository.GetAllADOAsync(onlyStudents)).ToList();
            users = users.OrderBy(u => u.Id).ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(24);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Listado de Usuarios")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Column(col =>
                        {
                            col.Spacing(8);
                            col.Item().Text($"Generado: {DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)}").FontSize(10).FontColor(Colors.Grey.Darken1);

                            foreach (var u in users)
                            {
                                col.Item().Padding(8).Border(1).BorderColor(Colors.Grey.Lighten2).Column(student =>
                                {
                                    student.Spacing(4);
                                    student.Item().Text($"{u.Id} - {u.UserName}").SemiBold().FontSize(12);
                                    student.Item().Text($"{u.Name} {u.LastName}").FontSize(11);
                                    student.Item().Text($"Email: {u.Email}    |    DNI: {u.Dni ?? "N/A"}").FontSize(10);
                                    student.Item().Text($"Legajo: {u.StudentNumber ?? "N/A"}    |    Dirección: {u.Adress ?? "N/A"}").FontSize(10);

                                    student.Item().Text($"Tipo: {u.TypeUser}    |    Cargo: {(u.JobPosition.HasValue ? u.JobPosition.Value.ToString() : "N/A")}")
                                           .FontSize(10);

                                    student.Item().Text($"Fecha de ingreso: {(u.DateOfAdmission.HasValue ? u.DateOfAdmission.Value.ToString("yyyy-MM-dd") : "N/A")}    |    Fecha de contratación: {(u.DateOfHire.HasValue ? u.DateOfHire.Value.ToString("yyyy-MM-dd") : "N/A")}")
                                           .FontSize(10);

                                    student.Item().Text($"Estado: {u.Status}").FontSize(10);
                                });
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Página ");
                            text.CurrentPageNumber();
                            text.Span(" de ");
                            text.TotalPages();
                        });
                });
            });

            var pdfBytes = document.GeneratePdf();

            string filePath = Path.Combine(_outputDirectory, $"ListaEstudiantes.pdf");
            File.WriteAllBytes(filePath, pdfBytes);

            return pdfBytes;
        }

        public async Task<byte[]> GenerateAdvancedReportAsync(bool onlyStudents = true)
        {
            var users = (await _userRepository.GetAllADOAsync(onlyStudents)).ToList();
            var userIds = users.Select(u => u.Id).ToHashSet();

            var courses = await _courseRepository.GetAllAsync();
            var courseToSpecialty = courses.ToDictionary(c => c.Id, c => c.Specialty?.DescEspecialidad ?? "Sin especialidad");

            var subjects = await _subjectRepository.GetAllAsync();
            var subjectMap = subjects.ToDictionary(s => s.Id, s => s.Desc);

            var enrollments = (await _enrollmentRepository.GetAllAsync())
                                .Where(e => userIds.Contains(e.UserId))
                                .ToList();

            var specialtyStudents = new Dictionary<string, HashSet<int>>(StringComparer.OrdinalIgnoreCase);
            var subjectSumCount = new Dictionary<int, (decimal sum, int count)>();

            foreach (var e in enrollments)
            {
                var specialty = courseToSpecialty.TryGetValue(e.CourseId, out var sp) ? sp : "Sin especialidad";
                if (!specialtyStudents.TryGetValue(specialty, out var set))
                {
                    set = new HashSet<int>();
                    specialtyStudents[specialty] = set;
                }
                set.Add(e.UserId);

                if (e.NotaFinal.HasValue)
                {
                    if (!subjectSumCount.TryGetValue(e.SubjectId, out var cur)) cur = (0m, 0);
                    cur.sum += e.NotaFinal.Value;
                    cur.count += 1;
                    subjectSumCount[e.SubjectId] = cur;
                }
            }

            var specialtyCounts = specialtyStudents.ToDictionary(kv => kv.Key, kv => kv.Value.Count);
            var subjectAverages = subjectSumCount.ToDictionary(
                kv => subjectMap.TryGetValue(kv.Key, out var nm) ? nm : $"Materia {kv.Key}",
                kv => kv.Value.count == 0 ? 0.0 : Math.Round((double)(kv.Value.sum / kv.Value.count), 2)
            );

            var pieBytes = GeneratePieChart(specialtyCounts, 700, 420);
            var avgBarBytes = GenerateBarChart(subjectAverages, 1000, 420);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(24);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Reporte Académico - Distribución y Promedios")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(12);
                        col.Item().Text($"Generado: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(10).FontColor(Colors.Grey.Darken1);

                        col.Item().Text("1) Distribución de estudiantes por especialidad").SemiBold();
                        if (pieBytes?.Length > 0)
                        {
                            col.Item().Element(e =>
                            {
                                using var ms = new MemoryStream(pieBytes);
                                e.Image(ms);
                            });
                        }

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(80); });
                            table.Header(header =>
                            {
                                header.Cell().Element(h => h.Text("Especialidad").SemiBold());
                                header.Cell().AlignCenter().Element(h => h.Text("Alumnos").SemiBold());
                            });

                            foreach (var kv in specialtyCounts.OrderByDescending(x => x.Value))
                            {
                                table.Cell().Element(c => c.Text(kv.Key));
                                table.Cell().AlignCenter().Element(c => c.Text(kv.Value.ToString()));
                            }
                        });
                        col.Item().PaddingTop(8).Text("2) Promedio de calificaciones por materia").SemiBold();
                        if (avgBarBytes?.Length > 0)
                        {
                            col.Item().Element(e =>
                            {
                                using var ms = new MemoryStream(avgBarBytes);
                                e.Image(ms);
                            });
                        }

                        col.Item().PaddingTop(4).Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(80); });
                            table.Header(header =>
                            {
                                header.Cell().Element(h => h.Text("Materia").SemiBold());
                                header.Cell().AlignRight().Element(h => h.Text("Promedio").SemiBold());
                            });

                            foreach (var kv in subjectAverages.OrderByDescending(x => x.Value))
                            {
                                table.Cell().Element(c => c.Text(kv.Key));
                                table.Cell().AlignRight().Element(c => c.Text(kv.Value.ToString("0.##")));
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Página ");
                            text.CurrentPageNumber();
                            text.Span(" de ");
                            text.TotalPages();
                        });
                });
            });

            var pdfBytes = document.GeneratePdf();
            string filePath = Path.Combine(_outputDirectory, $"ReporteAvanzado_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            File.WriteAllBytes(filePath, pdfBytes);

            return pdfBytes;
        }
        private byte[] GeneratePieChart(Dictionary<string, int> data, int width = 700, int height = 400)
        {
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var total = data.Values.Sum();
            var rectSize = Math.Min(width, height) * 0.45f;
            var rect = new SKRect(20, 20, 20 + rectSize, 20 + rectSize);
            float startAngle = -90f;

            SKColor[] colors = new[]
            {
                SKColors.SteelBlue, SKColors.IndianRed, SKColors.Orange, SKColors.Gold,
                SKColors.MediumSeaGreen, SKColors.SlateBlue, SKColors.CadetBlue, SKColors.DeepPink
            };

            var paint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill };

            int i = 0;
            foreach (var kv in data)
            {
                var count = kv.Value;
                float sweep = total == 0 ? 0f : 360f * count / total;
                paint.Color = colors[i % colors.Length];
                if (sweep > 0)
                    canvas.DrawArc(rect, startAngle, sweep, true, paint);
                startAngle += sweep;
                i++;
            }

            var legendX = (int)(rect.Right + 20);
            var legendY = 40;
            var legendPaint = new SKPaint { IsAntialias = true, TextSize = 14, Color = SKColors.Black };
            i = 0;
            foreach (var kv in data)
            {
                var color = colors[i % colors.Length];
                var boxPaint = new SKPaint { IsAntialias = true, Color = color, Style = SKPaintStyle.Fill };
                canvas.DrawRect(legendX, legendY + i * 28, 18, 18, boxPaint);

                var percent = total == 0 ? 0 : Math.Round((double)kv.Value * 100.0 / total, 1);
                var labelText = $"{kv.Key} ({kv.Value}) - {percent}%";
                canvas.DrawText(labelText, legendX + 26, legendY + 14 + i * 28, legendPaint);
                i++;
            }

            using var image = surface.Snapshot();
            using var dataEnc = image.Encode(SKEncodedImageFormat.Png, 100);
            return dataEnc.ToArray();
        }

        private byte[] GenerateBarChart(Dictionary<string, double> values, int width = 1000, int height = 420)
        {
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var textPaint = new SKPaint { IsAntialias = true, Color = SKColors.Black, TextSize = 14 };
            var axisPaint = new SKPaint { IsAntialias = true, Color = SKColors.Black, StrokeWidth = 1, Style = SKPaintStyle.Stroke };
            var barPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill };

            if (values == null || values.Count == 0)
            {
                canvas.DrawText("No hay datos para mostrar", width / 2 - 80, height / 2, textPaint);
                using var imgEmpty = surface.Snapshot();
                using var dataEmpty = imgEmpty.Encode(SKEncodedImageFormat.Png, 100);
                return dataEmpty.ToArray();
            }

            var labels = values.Keys.ToList();
            var vals = values.Values.ToList();
            double maxVal = Math.Max(1.0, vals.DefaultIfEmpty(0).Max());

            int n = labels.Count;
            int left = 120, right = 40, top = 40, bottom = 80;
            float availableWidth = width - left - right;
            float barAreaWidth = availableWidth / n;
            float barWidth = Math.Max(20, barAreaWidth * 0.6f);
            var originY = height - bottom;

            // Ejes
            canvas.DrawLine(left - 10, top, left - 10, originY + 10, axisPaint);
            canvas.DrawLine(left - 10, originY + 10, width - right + 10, originY + 10, axisPaint);

            // Grid y labels
            int steps = 5;
            for (int i = 0; i <= steps; i++)
            {
                float y = top + (originY - top) * i / steps;
                double val = Math.Round(maxVal * (1 - (double)i / steps), 2);
                canvas.DrawLine(left - 15, y, width - right + 5, y, new SKPaint { Color = SKColors.LightGray, StrokeWidth = 1 });
                canvas.DrawText(val.ToString("0.##"), 8, y + 5, textPaint);
            }

            for (int i = 0; i < n; i++)
            {
                var label = labels[i];
                var val = vals[i];
                float xCenter = left + i * barAreaWidth + (barAreaWidth / 2);
                float barLeft = xCenter - (barWidth / 2);
                float barRight = xCenter + (barWidth / 2);
                float barHeight = (float)((val / maxVal) * (originY - top));
                float barTop = originY - barHeight + 10;

                barPaint.Color = SKColor.FromHsl((i * 40) % 360, 60, 50);
                var barRect = new SKRect(barLeft, barTop, barRight, originY + 5);
                canvas.DrawRect(barRect, barPaint);

                // value text
                var valueText = val.ToString("0.##");
                var textWidth = textPaint.MeasureText(valueText);
                canvas.DrawText(valueText, xCenter - textWidth / 2, barTop - 6, textPaint);

                // label
                var labelPaint = new SKPaint { IsAntialias = true, Color = SKColors.Black, TextSize = 12 };
                var displayLabel = label.Length > 24 ? label.Substring(0, 22) + "..." : label;
                float labelX = xCenter - labelPaint.MeasureText(displayLabel) / 2;
                canvas.DrawText(displayLabel, labelX, originY + 28, labelPaint);
            }

            using var image = surface.Snapshot();
            using var dataEnc = image.Encode(SKEncodedImageFormat.Png, 100);
            return dataEnc.ToArray();
        }

        private record UserAverageDto(int Id, string UserName, string FullName, string Email, decimal? Average);
    }
}
