namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopThoiTrangModel : DbContext
    {
        public ShopThoiTrangModel()
            : base("name=ShopThoiTrangModel")
        {
        }

        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<CauHinh> CauHinhs { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<ChuongTrinhChiTiet> ChuongTrinhChiTiets { get; set; }
        public virtual DbSet<ChuongTrinhUuDai> ChuongTrinhUuDais { get; set; }
        public virtual DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }
        public virtual DbSet<Hang> Hangs { get; set; }
        public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhatKy> NhatKies { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<QuocGia> QuocGias { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<VaiTro> VaiTroes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CauHinh>()
                .Property(e => e.TenCauHinh)
                .IsUnicode(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.GiaTri)
                .IsUnicode(false);

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.TenForm)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongTrinhUuDai>()
                .HasMany(e => e.ChuongTrinhChiTiets)
                .WithOptional(e => e.ChuongTrinhUuDai)
                .HasForeignKey(e => e.ChuongTrinhId);

            modelBuilder.Entity<HoaDonBan>()
                .HasMany(e => e.HoaDonChiTiets)
                .WithOptional(e => e.HoaDonBan)
                .HasForeignKey(e => e.HoaDonId);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiKhachHang>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.LoaiKhachHang)
                .HasForeignKey(e => e.LoaiKhachId);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.DanhGiaSanPhams)
                .WithOptional(e => e.NguoiDung)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.NhatKies)
                .WithOptional(e => e.NguoiDung)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<NhatKy>()
                .Property(e => e.DiaChiIP)
                .IsUnicode(false);

            modelBuilder.Entity<QuocGia>()
                .Property(e => e.MaQuocGia)
                .IsUnicode(false);

            modelBuilder.Entity<QuocGia>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.QuocGia)
                .HasForeignKey(e => e.XuatXuId);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.AnhSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.KichCo)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Mau)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.XuatXuId)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Anh)
                .IsUnicode(false);
        }
    }
}
