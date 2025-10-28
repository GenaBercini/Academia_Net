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

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            QuestPDF.Settings.License = LicenseType.Community;
            if (!Directory.Exists(_outputDirectory))
                Directory.CreateDirectory(_outputDirectory);
        }
        public async Task<UserDTO> AddAsync(UserCreateDTO createDto)
        {
            //var userRepository = new UserRepository();

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
            //var userRepository = new UserRepository();
            var specialty = await _userRepository.GetAsync(id);
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDTO?> GetAsync(int id)
        {
            //var userRepository = new UserRepository();
            User? user = await _userRepository.GetAsync(id);

            if (user == null)
                return null;

            return MapToDTO(user);
        }
   
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            //var userRepository = new UserRepository();
            var usuarios = await _userRepository.GetAllAsync();

            return usuarios.Select(user => MapToDTO(user));
        }
        public async Task<bool> UpdateAsync(UserUpdateDTO updateDto)
        {
            //var userRepository = new UserRepository();
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
            var users = await _userRepository.GetAllAsync();
            if (onlyStudents)
                users = users.Where(u => u.TypeUser == UserType.Student).ToList();

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

                                    var enrollments = _userRepository.GetEnrollmentsByUser(u.Id).ToList();
                                    if (enrollments.Any())
                                    {
                                        student.Item().Text("Inscripciones:").FontSize(10).SemiBold();
                                        foreach (var e in enrollments)
                                        {
                                            student.Item().Text($"  Curso {e.CourseId} - Materia {e.SubjectId} - Nota: {(e.NotaFinal.HasValue ? e.NotaFinal.Value.ToString("N2") : "N/A")} - Inscripción: {(e.FechaInscripcion.HasValue ? e.FechaInscripcion.Value.ToString("yyyy-MM-dd") : "N/A")}").FontSize(9);
                                        }
                                    }
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

            string filePath = Path.Combine(_outputDirectory, $"ListaEstudiantes_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            File.WriteAllBytes(filePath, pdfBytes);

            return pdfBytes;
        }
        // (añade este método público en la clase UserService)

        public async Task<byte[]> GenerateGradesPieChartAsync()
        {
            // Buckets: ajustar según la agrupación que quieras
            var buckets = new Dictionary<string, int>
    {
        { "0-3", 0 },
        { "4-6", 0 },
        { "7-8", 0 },
        { "9-10", 0 }
    };

            // Obtener todos los usuarios y sus inscripciones
            var users = await _userRepository.GetAllAsync();

            foreach (var u in users)
            {
                var enrollments = _userRepository.GetEnrollmentsByUser(u.Id) ?? Enumerable.Empty<UserCourseSubject>();
                foreach (var e in enrollments)
                {
                    if (e.NotaFinal.HasValue)
                    {
                        // redondear o truncar según prefieras
                        var nota = (int)Math.Round(e.NotaFinal.Value);
                        if (nota <= 3) buckets["0-3"]++;
                        else if (nota <= 6) buckets["4-6"]++;
                        else if (nota <= 8) buckets["7-8"]++;
                        else buckets["9-10"]++;
                    }
                }
            }

            // Reusar el generador gráfico existente (método privado GenerateGradesPieChart)
            return GenerateGradesPieChart(buckets);
        }


        public byte[] GenerateGradesPieChart(Dictionary<string, int> buckets)
        {
            const int width = 700;
            const int height = 400;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var total = buckets.Values.Sum();
            var rect = new SKRect(20, 20, 320, 320);
            float startAngle = -90f;

            SKColor[] colors = new[]
            {
                    SKColors.LightGray,
                    SKColors.IndianRed,
                    SKColors.Orange,
                    SKColors.Gold,
                    SKColors.MediumSeaGreen,
                    SKColors.SteelBlue
                };

            var paint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill };

            int i = 0;
            foreach (var kv in buckets)
            {
                var count = kv.Value;
                float sweep = total == 0 ? 0f : 360f * count / total;
                paint.Color = colors[i % colors.Length];
                if (sweep > 0)
                {
                    canvas.DrawArc(rect, startAngle, sweep, true, paint);
                }
                startAngle += sweep;
                i++;
            }

            var legendX = 360;
            var legendY = 40;
            var legendPaint = new SKPaint { IsAntialias = true, TextSize = 14, Color = SKColors.Black };
            i = 0;
            foreach (var kv in buckets)
            {
                var color = colors[i % colors.Length];
                var boxPaint = new SKPaint { IsAntialias = true, Color = color, Style = SKPaintStyle.Fill };
                canvas.DrawRect(legendX, legendY + i * 30, 18, 18, boxPaint);

                var label = kv.Key;
                var percent = total == 0 ? 0 : Math.Round((double)kv.Value * 100.0 / total, 1);
                canvas.DrawText($"{label} ({kv.Value}) - {percent}%", legendX + 26, legendY + 14 + i * 30, legendPaint);
                i++;
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }

        private record UserAverageDto(int Id, string UserName, string FullName, string Email, decimal? Average);
    }
}
