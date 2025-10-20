using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Numerics;
using System.Security.Principal;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<CourseSubject> CoursesSubjects { get; set; }
        public DbSet<UserCourseSubject> UsersCoursesSubjects { get; set; }

        internal TPIContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
                entity.Property(entity => entity.UserName).IsRequired().HasMaxLength(50);
                entity.Property(entity => entity.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(entity => entity.Email).IsUnique();
                entity.Property(entity => entity.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(entity => entity.Salt).IsRequired().HasMaxLength(255);
                entity.Property(entity => entity.TypeUser).HasConversion<int>();
                entity.Property(entity => entity.JobPosition).HasConversion<int>();
                entity.Property(entity => entity.Status).HasConversion<int>();

                // Seed: usuario admin inicial

                var admin = User.CreateAdminSeed(
                    1,
                   "admin",
                   "Admin",
                   "Admin",
                   "admin@admin.com",
                   "Calle Falsa 123",
                   UserType.Admin,
                   "admin123"
                   );
                entity.HasData(new
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    Nombre = admin.Nombre,
                    Apellido = admin.Apellido,
                    Email = admin.Email,
                    Dni = admin.Dni ?? "",
                    StudentNumber = admin.StudentNumber,
                    Adress = admin.Adress ?? "",
                    TypeUser = admin.TypeUser,
                    JobPosition = admin.JobPosition,
                    DateOfAdmission = admin.DateOfAdmission,
                    DateOfHire = admin.DateOfHire,
                    Status = admin.Status,
                    PasswordHash = admin.PasswordHash,
                    Salt = admin.Salt
                });
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DescEspecialidad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DuracionAnios)
                    .IsRequired();
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.HsSemanales)
                    .IsRequired();

                entity.Property(e => e.Obligatoria)
                    .IsRequired();
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Año_calendario)
                    .IsRequired();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Cupo).IsRequired();

                entity.Property(e => e.Año_calendario).IsRequired();

                entity.Property(e => e.Turno)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Comision)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // Especialidad 1 a M Planes 
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Specialty)
                .WithMany(s => s.Plans)
                .HasForeignKey(p => p.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Especialidad 1 a M Cursos 
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Specialty)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Curso  Materia (M:N con atributo) 
            modelBuilder.Entity<CourseSubject>()
                .HasKey(cs => new { cs.CourseId, cs.SubjectId });

            modelBuilder.Entity<CourseSubject>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CoursesSubjects)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.CoursesSubjects)
                .HasForeignKey(cs => cs.SubjectId);

            // Usuario relación con CursoMateria (M:N con atributo)
            modelBuilder.Entity<UserCourseSubject>()
                .HasKey(ucs => new { ucs.UserId, ucs.CourseId, ucs.SubjectId });

            modelBuilder.Entity<UserCourseSubject>()
                .HasOne(ucs => ucs.User)
                .WithMany(u => u.CoursesSubjects) 
                .HasForeignKey(ucs => ucs.UserId);

            modelBuilder.Entity<UserCourseSubject>()
                .HasOne(ucs => ucs.CourseSubject)
                .WithMany(cs => cs.Users)
                .HasForeignKey(ucs => new { ucs.CourseId, ucs.SubjectId });
        }
    }
}
