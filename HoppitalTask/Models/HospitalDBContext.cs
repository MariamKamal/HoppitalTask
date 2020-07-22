using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HoppitalTask.Models
{
    public partial class HospitalDBContext : DbContext
    {
        public HospitalDBContext()
        {
        }

        public HospitalDBContext(DbContextOptions<HospitalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientDoctor> PatientDoctor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=HospitalDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .HasColumnName("AppointmentID")
                   ;

                entity.Property(e => e.Clinic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Appointment_Patient");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ClinicName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.Location)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Clinic)
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("FK_Clinic_Doctor");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DocId);

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.DocName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Specialization)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.BloodGroub)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PartientName)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PatientDoctor>(entity =>
            {
                entity.HasKey(e => new { e.DocId, e.PatientId });

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.PatientDoctor)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientDoctor_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientDoctor)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientDoctor_Patient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
