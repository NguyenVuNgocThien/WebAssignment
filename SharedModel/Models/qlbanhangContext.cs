using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SharedModel.Models
{
    public partial class qlbanhangContext : DbContext
    {
        public qlbanhangContext()
        {
        }

        public qlbanhangContext(DbContextOptions<qlbanhangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthd> Cthds { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSp> LoaiSps { get; set; }
        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoanDangNhap> TaiKhoanDangNhaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CQCHTRM;Initial Catalog=qlbanhang;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cthd>(entity =>
            {
                entity.HasKey(e => new { e.MaHd, e.MaSp });

                entity.ToTable("CTHD");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(5)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(4)
                    .HasColumnName("MaSP");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK_CTHD_HoaDon");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_CTHD_SanPham");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.MaGh);

                entity.ToTable("GioHang");

                entity.Property(e => e.MaGh)
                    .HasMaxLength(4)
                    .HasColumnName("MaGH");

                entity.Property(e => e.MaKh)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaSp)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("MaSP");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioHang_KhachHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioHang_SanPham");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(5)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(4)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayGiaoHang).HasColumnType("datetime");

                entity.Property(e => e.NgayLapHd)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayLapHD");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_KhachHang1");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HoaDon_Nhanvien");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(4)
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(30);

                entity.Property(e => e.DienThoai).HasMaxLength(7);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(12);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(30)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSp);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoaiSp)
                    .ValueGeneratedNever()
                    .HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenLoaiSp)
                    .HasMaxLength(255)
                    .HasColumnName("TenLoaiSP");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("Nhanvien");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.HoNv)
                    .HasMaxLength(50)
                    .HasColumnName("HoNV");

                entity.Property(e => e.Ten).HasMaxLength(50);
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PhanQuyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(20);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(4)
                    .HasColumnName("MaSP");

                entity.Property(e => e.Donvitinh).HasMaxLength(8);

                entity.Property(e => e.HinhSp).HasColumnName("HinhSP");

                entity.Property(e => e.MaLoaiSp).HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(20)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiSpNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoaiSp)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SanPham_LoaiSP");
            });

            modelBuilder.Entity<TaiKhoanDangNhap>(entity =>
            {
                entity.HasKey(e => e.TaiKhoan)
                    .HasName("PK__TaiKhoan__D5B8C7F19D10E37D");

                entity.ToTable("TaiKhoanDangNhap");

                entity.Property(e => e.TaiKhoan).HasMaxLength(20);

                entity.Property(e => e.MaKh)
                    .HasMaxLength(4)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.MatKhau).HasMaxLength(10);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.TaiKhoanDangNhaps)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_TaiKhoanDangNhap_KhachHang");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
