using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SampleSPAwithbackend.Models;

public partial class SampleSpaContext : DbContext
{
    public SampleSpaContext()
    {
    }

    public SampleSpaContext(DbContextOptions<SampleSpaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogError> LogErrors { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=ARTURO\\SQLEXPRESS; database=SampleSPA; integrated security=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogError>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogError__3213E83F01D55181");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.RegistrationDate)
                .HasComputedColumnSql("(getdate())", false)
                .HasColumnType("datetime")
                .HasColumnName("registrationDate");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F37BD765F");

            entity.HasIndex(e => e.UserName, "UQ__Users__66DCF95C6F0504B1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.RegistrationDate)
                .HasComputedColumnSql("(getdate())", false)
                .HasColumnType("datetime")
                .HasColumnName("registrationDate");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
