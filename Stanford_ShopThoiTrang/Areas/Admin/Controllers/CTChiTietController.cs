
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class CTChiTietController : BaseController
    {
        // GET: Admin/CTChiTiet
        // GET: Admin/CTKhuyenMai
        public ActionResult DanhSach()
        {
            var lstChiTiet = DataProvider.ShopEntities.ChuongTrinhChiTiets.ToList();
            ViewBag.sanpham = DataProvider.ShopEntities.SanPhams.ToList();
            ViewBag.chuongtrinh = DataProvider.ShopEntities.ChuongTrinhUuDais.ToList();
            return View(lstChiTiet);

        }
        public JsonResult ThemChuongTrinhJson()
        {

            var data = Request.Form;
            //Khai báo 1 đối tượng
            ChuongTrinhChiTiet objChiTiet = new ChuongTrinhChiTiet();
            // Gán giá trị
            objChiTiet.ChuongTrinhId = Convert.ToInt32(data["IdChuongtrinh"]);
            objChiTiet.SanPhamId = Convert.ToInt32(data["IdSanPham"]);
            objChiTiet.GiamGia = Convert.ToDouble(data["GiamGia"]);
            var id = Convert.ToInt32(data["id"]);

            if (id > 0)//TH sửa
            {
                objChiTiet.Id = id;
                // Lấy đối tượng cũ
                ChuongTrinhChiTiet objChuongTrinhld = DataProvider.ShopEntities.ChuongTrinhChiTiets.Where(p => p.Id == id).First();
                DataProvider.ShopEntities.Entry(objChuongTrinhld).CurrentValues.SetValues(objChiTiet);
                DataProvider.ShopEntities.SaveChanges();

                // nhật ký sửa 
                // 1.Thêm mới 2 Sửa 3.Xóa
                NhatKyCommon.Them("Người dùng thực hiện chức năng Sửa Chương trình khuyến mãi chi tiết","Sửa");
            }
            else
            {
                // Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.ChuongTrinhChiTiets.Add(objChiTiet);
                DataProvider.ShopEntities.SaveChanges();
                // nhật ký thêm mới 
                // 1.Thêm mới 2 Sửa 3.Xóa
                NhatKyCommon.Them("Người dùng thực hiện chức năng Thêm Mới Chương trình khuyến mãi chi tiết","Thêm Mới");
            }

           
            return Json(objChiTiet, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ChiTietChuongTrinhJson(int id)
        {
            ChuongTrinhChiTiet objChiTiet = DataProvider.ShopEntities.ChuongTrinhChiTiets.Where(p => p.Id == id).First();
            ChuongTrinhChiTietViewModel objChiTietView = new ChuongTrinhChiTietViewModel();
            if (objChiTiet != null)
            {
                objChiTietView.Id = objChiTiet.Id;
                objChiTietView.ChuongTrinhId = objChiTiet.ChuongTrinhId;
                objChiTietView.SanPhamId = objChiTiet.SanPhamId;
                objChiTietView.GiamGia = objChiTiet.GiamGia;

            }

            return Json(objChiTietView, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Xoa(int id)
        {
            ChuongTrinhChiTiet objChiTiet = DataProvider.ShopEntities.ChuongTrinhChiTiets.Where(p => p.Id == id).First();

            if (objChiTiet != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.ChuongTrinhChiTiets.Remove(objChiTiet);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            // Nhật ký xóa
            // 1.Thêm mới 2 Sửa 3.Xóa
            NhatKyCommon.Them("Người dùng thực hiện chức năng Xóa Chương trình khuyến mãi chi tiết", "Xóa");
            return Json(objChiTiet, JsonRequestBehavior.AllowGet);
        }

    }
}