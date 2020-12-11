using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopThoiTrang.Models;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    [CheckPhanQuyenLogin(tenChucNang = "SanPham")]
    public class SanPhamController : Controller
    {
        public ActionResult DanhSachSanPham(string txtTuKhoa, string txtNgayBatDau, string txtNgayKetThuc, int? loaiSanPham, int? hangSanXuat, int? chuDe, string xuatXu, string txtGiaStart, string txtGiaEnd)
        {
            //Lấy VaiTroId và các quyền
            var vaiTroId = Int32.Parse(Session["VaiTroId"].ToString());
            PhanQuyen objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId != null && p.VaiTroId == vaiTroId && p.ChucNangId == 46).First<PhanQuyen>();
            //Gán các nút hiển thị
            ViewBag.Them = objPhanQuyen.Them == true ? "normal" : "none";
            ViewBag.Sua = objPhanQuyen.Sua == true ? "normal" : "none";
            ViewBag.Xoa = objPhanQuyen.Xoa == true ? "normal" : "none";
            CultureInfo ci = new CultureInfo("vi-VN");
            HienThiChuDe();
            HienThiHangSanXuat();
            HienThiLoaiSanPham();
            HienThiXuatXu();
            IQueryable<SanPham> lstDanhSachSP = DataProvider.ShopEntities.SanPhams;
            if (!string.IsNullOrEmpty(txtTuKhoa))
            {
                lstDanhSachSP = lstDanhSachSP.Where(x => x.MaSanPham == txtTuKhoa || x.TenSanPham.ToLower().Contains(txtTuKhoa.ToLower()));
            }
            if (!string.IsNullOrEmpty(txtNgayBatDau) && !string.IsNullOrEmpty(txtNgayKetThuc))
            {
                DateTime ngayStart = DateTime.Parse(txtNgayBatDau, ci);
                DateTime ngayEnd = DateTime.Parse(txtNgayKetThuc, ci);
                lstDanhSachSP = lstDanhSachSP.Where(x => x.NgayTao >= ngayStart && x.NgayTao <= ngayEnd);
            }
            int loaiId = 0, hangId = 0, chuDeId = 0;
            loaiId = (loaiSanPham.HasValue) ? loaiSanPham.Value : 0;
            hangId = (hangSanXuat.HasValue) ? hangSanXuat.Value : 0;
            chuDeId = (chuDe.HasValue) ? chuDe.Value : 0;
            if (loaiSanPham.HasValue)
            {
                lstDanhSachSP = lstDanhSachSP.Where(x => x.LoaiId == loaiId);
            }
            if (hangSanXuat.HasValue)
            {
                lstDanhSachSP = lstDanhSachSP.Where(x => x.HangId == hangId);
            }
            if (chuDe.HasValue)
            {
                lstDanhSachSP = lstDanhSachSP.Where(x => x.ChuDeId == chuDeId);
            }
            if (!string.IsNullOrEmpty(xuatXu))
            {
                lstDanhSachSP = lstDanhSachSP.Where(x => x.XuatXuId == xuatXu);
            }
            if (!string.IsNullOrEmpty(txtGiaStart) && !string.IsNullOrEmpty(txtGiaEnd))
            {
                double GiaStart = double.Parse(txtGiaStart);
                double GiaEnd = double.Parse(txtGiaEnd);
                lstDanhSachSP = lstDanhSachSP.Where(x => x.Gia >= GiaStart && x.Gia <= GiaEnd);
            }
            return View(lstDanhSachSP.ToList());
        }
        private void HienThiChuDe()
        {
            List<ChuDe> lstChuDe = DataProvider.ShopEntities.ChuDes.ToList();
            ViewBag.ChuDe = new SelectList(lstChuDe, "Id", "TenChuDe");
        }
        private void HienThiHangSanXuat()
        {
            List<Hang> lstHangSanXuat = DataProvider.ShopEntities.Hangs.ToList();
            ViewBag.HangSanXuat = new SelectList(lstHangSanXuat, "Id", "TenHang");
        }
        private void HienThiLoaiSanPham()
        {
            List<Loai> lstLoaiSanPham = DataProvider.ShopEntities.Loais.ToList();
            ViewBag.LoaiSanPham = new SelectList(lstLoaiSanPham, "Id", "TenLoai");
        }
        private void HienThiXuatXu()
        {
            List<QuocGia> lstXuatXu = DataProvider.ShopEntities.QuocGias.ToList();
            ViewBag.XuatXu = new SelectList(lstXuatXu, "MaQuocGia", "TenQuocGia");
        }
        private void HienThiDanhSachaMauSacSanPham()
        {
            List<SelectListItem> lstMau = new List<SelectListItem>();
            SelectListItem item = new SelectListItem() { Value = "Đen", Text = "Đen" };
            lstMau.Add(item);         
            SelectListItem item1 = new SelectListItem() { Value = "Đỏ", Text = "Đỏ" };
            lstMau.Add(item1);       
            SelectListItem item2 = new SelectListItem() { Value = "Xanh", Text = "Xanh" };
            lstMau.Add(item2);         
            ViewBag.ddlMau = new MultiSelectList(lstMau, "Value", "Text");
        }
        private void HienThiDanhSachKichCoSanPham()
        {
            List<SelectListItem> lstKichCo=new List<SelectListItem>();
            lstKichCo.Add(new SelectListItem() {Value="S",Text="S" });
            lstKichCo.Add(new SelectListItem() { Value = "M", Text = "M" });
            lstKichCo.Add(new SelectListItem() { Value = "L", Text = "L" });
            ViewBag.ddlKichCo = new MultiSelectList(lstKichCo, "Value", "Text");
        }
        private void HienThiDanhSachTagsSanPham()
        {
            List<SelectListItem> lstTags = new List<SelectListItem>();
            lstTags.Add(new SelectListItem() {Value="Lựa chọn tốt nhất",Text= "Lựa chọn tốt nhất" });
            lstTags.Add(new SelectListItem() { Value = "Giá cả tốt nhất", Text = "Giá cả tốt nhất" });
            lstTags.Add(new SelectListItem() { Value = "Siêu phẩm hàng đầu", Text = "Siêu phẩm hàng đầu" });
            ViewBag.ddlTags = new MultiSelectList(lstTags,"Value","Text");
        }
        [HttpGet]
        public ActionResult AddSanPham()
        {
            HienThiChuDe();
            HienThiHangSanXuat();
            HienThiLoaiSanPham();
            HienThiXuatXu();
            HienThiDanhSachaMauSacSanPham();
            HienThiDanhSachKichCoSanPham();
            HienThiDanhSachTagsSanPham();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSanPham(SanPham objSanPham, HttpPostedFileBase fAnh)
        {
            HienThiChuDe();
            HienThiHangSanXuat();
            HienThiLoaiSanPham();
            HienThiXuatXu();
            HienThiDanhSachaMauSacSanPham();
            HienThiDanhSachKichCoSanPham();
            HienThiDanhSachTagsSanPham();
            if (objSanPham != null)
            {
                //Bắt lỗi các trường bắt buộc phải nhập sp
                if (string.IsNullOrEmpty(objSanPham.TenSanPham))
                {
                    ViewBag.ErrorTenSanPham += "Tên sản phẩm không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(objSanPham.MoTa))
                {
                    ViewBag.ErrorMoTa += "Mô tả sản phẩm không được trống";
                    return View();
                }                         
                if (string.IsNullOrEmpty(Request["Gia"] + ""))
                {
                    ViewBag.ErrorGia += "Giá sản phẩm không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(Request["KichCoId"]+""))
                {
                    ViewBag.ErrorKichCo += "Kích cỡ sản phẩm không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(objSanPham.SoLuong + ""))
                {
                    ViewBag.ErrorSoLuong += "Số lượng sản phẩm không được trống và là số nguyên";
                    return View();
                }
                if (string.IsNullOrEmpty(Request["MauId"] + ""))
                {
                    ViewBag.ErrorMau += "Màu sản phẩm không được trống";
                    return View();
                }                
                if (string.IsNullOrEmpty(objSanPham.ChuDeId + ""))
                {
                    ViewBag.ErrorChuDeSanPham += "Chủ đề sản phẩm không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(objSanPham.HangId + ""))
                {
                    ViewBag.ErrorHangSanXuat += "Hãng sản xuất không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(objSanPham.LoaiId + ""))
                {
                    ViewBag.ErrorLoaiSanPham += "Loại sản phẩm không được trống";
                    return View();
                }
                if (string.IsNullOrEmpty(objSanPham.XuatXuId))
                {
                    ViewBag.ErrorXuatXu += "Xuất xứ sản phẩm không được trống";
                    return View();
                }
                //Bắt lỗi khi nhập tên sản phẩm đã tồn tại với tên một sản phẩm đã có trong hệ thống
                if (BatLoiTrungTenSanPham(objSanPham.TenSanPham))
                {
                    ViewBag.TrungTenSanPham += "Tên sản phẩm đã tồn tại";
                    return View();
                }
                //Bắt lỗi khi dữ liệu đầu vào không đúng định dạng(số lượng sản phẩm)                    
                int soluong = 0;
                if (int.TryParse(objSanPham.SoLuong + "", out soluong))
                {
                    if (soluong < 0)
                    {
                        ViewBag.ErrorSoLuongSanPham += "Số lượng sản phẩm phải lớn hơn 0";
                        return View();
                    }

                }

                objSanPham.MaSanPham = TaoMaSanPhamTuDong();
                objSanPham.DaDuyet = false;
                objSanPham.TrangThai = (objSanPham.SoLuong > 0) ? 1 : 0;
                objSanPham.NgayTao = DateTime.Now;
                objSanPham.NgayCapNhat = DateTime.Now;
                objSanPham.KichCo = Request["KichCoId"] + "";
                objSanPham.Mau = Request["MauId"] + "";
                objSanPham.Tags = Request["TagsId"] + "";
                objSanPham.Gia = double.Parse(Request["Gia"] + "");
                if (fAnh != null && fAnh.ContentLength > 0)
                {
                    string tenAnh = Path.GetFileName(fAnh.FileName);
                    fAnh.SaveAs(Server.MapPath("~/Content/Images/" + tenAnh));
                    objSanPham.AnhSanPham = tenAnh;
                }
                if (string.IsNullOrEmpty(objSanPham.AnhSanPham + ""))
                {
                    ViewBag.ErrorAnhSanPham = "Ảnh sản phẩm không được trống";
                    return View();
                }
                DataProvider.ShopEntities.SanPhams.Add(objSanPham);
                DataProvider.ShopEntities.SaveChanges();
                return RedirectToAction("DanhSachSanPham");
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateSanPham(int Id)
        {
            HienThiChuDe();
            HienThiHangSanXuat();
            HienThiLoaiSanPham();
            HienThiXuatXu();
            HienThiDanhSachaMauSacSanPham();
            HienThiDanhSachKichCoSanPham();
            HienThiDanhSachTagsSanPham();
            SanPham objSanPham = DataProvider.ShopEntities.SanPhams.Find(Id);

            List<SelectListItem> lstHand = new List<SelectListItem>();

            SelectListItem item = new SelectListItem() { Value = "Đen", Text = "Đen" };
            lstHand.Add(item);            
            SelectListItem item1 = new SelectListItem() { Value = "Đỏ", Text = "Đỏ" };
            lstHand.Add(item1);          
            SelectListItem item2 = new SelectListItem() { Value = "Xanh", Text = "Xanh" };
            lstHand.Add(item2);        
               
            string[] a = objSanPham.Mau.Split(',');
            List<SelectListItem> lstSizeChose = new List<SelectListItem>();
            for(int i=0;i<a.Length;i++)
            {
                SelectListItem mau1 = new SelectListItem() { Value = a[i], Text = a[i] };
                lstSizeChose.Add(mau1);               
            }           
            ViewBag.ddlMau = new MultiSelectList(lstHand, "Value", "Text", lstSizeChose);
            return View(objSanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSanPham(SanPham objSanPham, HttpPostedFileBase fAnh)
        {
            HienThiChuDe();
            HienThiHangSanXuat();
            HienThiLoaiSanPham();
            HienThiXuatXu();

            //Bắt lỗi các trường bắt buộc phải nhập
            if (string.IsNullOrEmpty(objSanPham.TenSanPham))
            {
                ViewBag.ErrorTenSanPham += "Tên sản phẩm không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.MoTa))
            {
                ViewBag.ErrorMoTa += "Mô tả sản phẩm không được trống";
                return View(objSanPham);
            }                        
            if (string.IsNullOrEmpty(objSanPham.KichCo))
            {
                ViewBag.ErrorKichCo += "Kích cỡ sản phẩm không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.SoLuong + ""))
            {
                ViewBag.ErrorSoLuong += "Số lượng sản phẩm không được trống và là số nguyên";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.Mau + ""))
            {
                ViewBag.ErrorMau += "Màu sản phẩm không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.ChuDeId + ""))
            {
                ViewBag.ErrorChuDeSanPham += "Chủ đề sản phẩm không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.HangId + ""))
            {
                ViewBag.ErrorHangSanXuat += "Hãng sản xuất không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.LoaiId + ""))
            {
                ViewBag.ErrorLoaiSanPham += "Loại sản phẩm không được trống";
                return View(objSanPham);
            }
            if (string.IsNullOrEmpty(objSanPham.XuatXuId))
            {
                ViewBag.ErrorXuatXu += "Xuất xứ sản phẩm không được trống";
                return View(objSanPham);
            }            
            //Bắt lỗi khi dữ liệu đầu vào không đúng định dạng(số lượng sản phẩm)                    
            int soluong = 0;
            if (int.TryParse(objSanPham.SoLuong + "", out soluong))
            {
                if (soluong < 0)
                {
                    ViewBag.ErrorSoLuongSanPham += "Số lượng sản phẩm phải lớn hơn 0";
                    return View(objSanPham);
                }

            }

            objSanPham.MaSanPham = DataProvider.ShopEntities.SanPhams.Find(objSanPham.Id).MaSanPham;

            if (fAnh != null && fAnh.ContentLength > 0)
            {
                string tenAnh = Path.GetFileName(fAnh.FileName);
                fAnh.SaveAs(Server.MapPath("~/Content/Images/" + tenAnh));
                objSanPham.AnhSanPham = tenAnh;
            }
            if (objSanPham.AnhSanPham == null)
            {
                objSanPham.AnhSanPham = "";
            }
            SanPham objSanPhamOld = DataProvider.ShopEntities.SanPhams.Find(objSanPham.Id);
            objSanPham.NgayTao = objSanPhamOld.NgayTao;
            objSanPham.NgayCapNhat = DateTime.Now;
            objSanPham.DaDuyet = objSanPhamOld.DaDuyet;
            objSanPham.TrangThai = objSanPhamOld.TrangThai;
           // objSanPham.KichCo = Request["kichco"] + "";
            if((Request["Gia"] + "")=="")
            {
                ViewBag.ErrorGia = "Giá sản phẩm không được trống";
                return View(objSanPham);
            }
            objSanPham.Gia = double.Parse(Request["Gia"] + "");
            DataProvider.ShopEntities.Entry<SanPham>(objSanPhamOld).CurrentValues.SetValues(objSanPham);
            DataProvider.ShopEntities.SaveChanges();
            return RedirectToAction("DanhSachSanPham");
        }
        public JsonResult XoaSanPham(int id)
        {
            SanPham objSanPham = DataProvider.ShopEntities.SanPhams.Find(id);
            DataProvider.ShopEntities.SanPhams.Remove(objSanPham);
            DataProvider.ShopEntities.SaveChanges();
            return Json(objSanPham, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DuyetSanPham(List<int> lstMa)
        {
            int dem = 0;
            for (int i = 0; i < lstMa.Count; i++)
            {
                Duyet(lstMa[i]);
                dem++;
            }
            return Json(dem, JsonRequestBehavior.AllowGet);
        }
        private void Duyet(int Id)
        {
            SanPham objSanPhamOld = DataProvider.ShopEntities.SanPhams.Find(Id);

            SanPham objSanPhamMoi = DataProvider.ShopEntities.SanPhams.Find(Id);
            objSanPhamMoi.DaDuyet = true;
            objSanPhamMoi.NgayDuyet = DateTime.Now;
            DataProvider.ShopEntities.Entry<SanPham>(objSanPhamOld).CurrentValues.SetValues(objSanPhamMoi);
            DataProvider.ShopEntities.SaveChanges();
        }
        private string TaoMaSanPhamTuDong()
        {
            List<SanPham> lstSanPham = DataProvider.ShopEntities.SanPhams.ToList();
            if (lstSanPham.Count == 0)
            {
                return "SP000001";
            }
            string MaSPTruoc = lstSanPham[lstSanPham.Count - 1].MaSanPham + "";

            int SoTiepTheo = int.Parse(MaSPTruoc.Substring(2)) + 1;
            string lap = "";
            for (int i = 0; i < (MaSPTruoc.Length - 2 - SoTiepTheo.ToString().Length); i++)
            {
                lap += "0";
            }
            return "SP" + lap + SoTiepTheo;
        }

        public JsonResult BoDuyetSanPham(List<int> lstMa)
        {
            int dem = 0;
            for (int i = 0; i < lstMa.Count; i++)
            {
                BoDuyet(lstMa[i]);
                dem++;
            }
            return Json(dem, JsonRequestBehavior.AllowGet);
        }
        private void BoDuyet(int Id)
        {
            SanPham objSanPhamOld = DataProvider.ShopEntities.SanPhams.Find(Id);

            SanPham objSanPhamMoi = DataProvider.ShopEntities.SanPhams.Find(Id);
            objSanPhamMoi.DaDuyet = false;

            DataProvider.ShopEntities.Entry<SanPham>(objSanPhamOld).CurrentValues.SetValues(objSanPhamMoi);
            DataProvider.ShopEntities.SaveChanges();
        }
        /// <summary>
        /// Hàm bắt lỗi trùng tên sản phẩm
        /// </summary>
        /// <param name="TenSP"></param>
        /// <returns>Trả về true nếu tên sản phẩm bị trùng và false nếu tên sản phẩm chưa tồn tại trong danh sách</returns>
        private bool BatLoiTrungTenSanPham(string TenSP)
        {
            bool KetQua = false;
            List<SanPham> lstSanPham = DataProvider.ShopEntities.SanPhams.ToList();
            for (int i = 0; i < lstSanPham.Count; i++)
            {
                if (lstSanPham[i].TenSanPham.Equals(TenSP))
                {
                    KetQua = true;
                    break;
                }
            }
            return KetQua;
        }
        /// <summary>
        /// Hàm lấy thông tin chi tiết một sản phẩm với Id truyền vào
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Các thông số sản phẩm dưới dạng mảng object</returns>
        public JsonResult ThongTinChiTietSanPham(int Id)
        {
            SanPham objSanPham = DataProvider.ShopEntities.SanPhams.Find(Id);
            object[] arr = new object[10];
            arr[0] = objSanPham.MaSanPham;
            arr[1] = objSanPham.TenSanPham;
            arr[2] = objSanPham.MoTa;
            arr[3] = objSanPham.NoiDung;
            arr[4] = objSanPham.AnhSanPham;
            arr[5] = objSanPham.Gia.Value.ToString("###,###")+" VNĐ";
            arr[6] = objSanPham.KichCo;
            arr[7] = objSanPham.Mau;
            arr[8] = (objSanPham.HangId.HasValue) ? objSanPham.Hang.TenHang : "";
            arr[9] = (objSanPham.XuatXuId.Length>0&& objSanPham.XuatXuId!=null) ? objSanPham.QuocGia.TenQuocGia : "";
            return Json(arr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UploadNhieuAnh(IEnumerable<HttpPostedFileBase> fUploadAnh)
        {
           // AnhSanPham objAnhSanPham = null;
            string DanhMucAnh="";
            foreach(HttpPostedFileBase item in fUploadAnh)
            {
                if(item!=null&&item.ContentLength>0)
                {
                    string TenAnh = Path.GetFileName(item.FileName);
                    item.SaveAs(Server.MapPath("~/Content/Images/"+item.FileName));
                    DanhMucAnh += TenAnh + " ";
                }
            }
            return Json(DanhMucAnh.Substring(0,DanhMucAnh.Length-1),JsonRequestBehavior.AllowGet);
        }
        //Hàm xử lý thêm nhiều ảnh cho  1 sản phẩm
        public JsonResult ThemNhieuAnhChoSanPham(int SanPhamId,string ArrAnh)
        {
            string[] Anh = ArrAnh.Split(' ');
            AnhSanPham objAnhSanPham = null;
            for(int i=0;i<Anh.Length;i++)
            {
                objAnhSanPham = new AnhSanPham();
                objAnhSanPham.TenAnh = Anh[i];
                objAnhSanPham.SanPhamId = SanPhamId;
                DataProvider.ShopEntities.AnhSanPhams.Add(objAnhSanPham);
                DataProvider.ShopEntities.SaveChanges();
            }
            return Json(Anh.Length,JsonRequestBehavior.AllowGet);
        }
    }
}