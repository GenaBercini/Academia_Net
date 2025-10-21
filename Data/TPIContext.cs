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
                var adminUser = new User(1,"admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Juan Jose Paso 123", Domain.Model.UserType.Admin, "42789654");

                entity.HasData(new
                {
                    Id = adminUser.Id,
                    UserName = adminUser.UserName,
                    Name = adminUser.Name,
                    LastName = adminUser.LastName,
                    Email = adminUser.Email,
                    Dni = adminUser.Dni,
                    Adress = adminUser.Adress,
                    TypeUser = adminUser.TypeUser,
                    Status = adminUser.Status,
                    PasswordHash = adminUser.PasswordHash,
                    Salt = adminUser.Salt
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

                entity.HasOne(p => p.Specialty)
                .WithMany(s => s.Plans)
                .HasForeignKey(p => p.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(c => c.Specialty)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CourseSubject>(entity =>
            {
                entity.HasKey(cs => new { cs.CourseId, cs.SubjectId });

                entity.HasOne(cs => cs.Course)
                      .WithMany(c => c.CoursesSubjects)
                      .HasForeignKey(cs => cs.CourseId);

                entity.HasOne(cs => cs.Subject)
                      .WithMany(s => s.CoursesSubjects)
                      .HasForeignKey(cs => cs.SubjectId);

                entity.Property(e => e.DiaHoraDictado).HasMaxLength(200);
            });

             modelBuilder.Entity<UserCourseSubject>(entity =>
             {
                 entity.HasKey(e => new { e.UserId, e.CourseId, e.SubjectId });

                 entity.HasOne(e => e.User)
                       .WithMany(u => u.CoursesSubjects)
                       .HasForeignKey(e => e.UserId);

                 entity.HasOne(e => e.CourseSubject)
                       .WithMany(cs => cs.Users)
                       .HasForeignKey(e => new { e.CourseId, e.SubjectId });

                 entity.Property(e => e.FechaInscripcion).IsRequired(false);
                 entity.Property(e => e.NotaFinal).HasColumnType("decimal(5,2)").IsRequired(false);
             });
        }
    }
}
