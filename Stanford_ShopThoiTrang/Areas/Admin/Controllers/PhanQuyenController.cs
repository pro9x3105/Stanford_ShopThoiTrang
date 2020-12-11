using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    [CheckPhanQuyenLogin(tenChucNang = "PhanQuyen")]
    public class PhanQuyenController : Controller
    {
        // GET: Admin/PhanQuyen
        public ActionResult DanhSach()
        {
            //Khai báo
            var lstPhanQuyen = DataProvider.ShopEntities.PhanQuyens.ToList();
            var lstChucNang = DataProvider.ShopEntities.ChucNangs.ToList();
            ViewBag.VaiTro = new SelectList(DataProvider.ShopEntities.VaiTroes.ToList(), "Id", "TenVaiTro");

            //Lọc Chức năng chưa cấp
            var ChucNangIdChuaCap = lstChucNang.AsEnumerable().Select(r => r.Id).Except(lstPhanQuyen.AsEnumerable().Select(r =>(int)r.ChucNangId));
            var lstChuaCap = (from p in lstChucNang.AsEnumerable()
                          join Id in ChucNangIdChuaCap on p.Id equals Id
                          select p);
            ViewBag.ChuaCap = new SelectList(lstChuaCap,"Id","TenChucNang");

            //Lọc chức năng đã cấp
            var lstDaCap = (from p in lstPhanQuyen
                            join c in lstChucNang on p.ChucNangId equals c.Id
                            select new {
                                p.Id,
                                p.ChucNangId,
                                c.TenChucNang
                            }).ToList();

            //Lọc chức năng đã cấp test 2

            ViewBag.DaCap = new SelectList(lstDaCap, "Id", "TenChucNang");

            return View();
        }

        //Chi tiết vai trò
        public JsonResult ChiTietVaiTroChuaCapJson(int vaiTroId)
        {
            //Khai báo
            var lstPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId == vaiTroId).ToList();
            var lstChucNang = DataProvider.ShopEntities.ChucNangs.ToList();

            //Lọc Chức năng chưa cấp
            var ChucNangIdChuaCap = lstChucNang.AsEnumerable().Select(r => r.Id).Except(lstPhanQuyen.AsEnumerable().Select(r => (int)r.ChucNangId));
            var lstChuaCap = (from p in lstChucNang.AsEnumerable()
                              join Id in ChucNangIdChuaCap on p.Id equals Id
                              select new
                              {
                                  p.Id,
                                  p.TenChucNang
                              }).ToList();


            return Json(lstChuaCap, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChiTietVaiTroDaCapJson(int vaiTroId)
        {
            //Khai báo
            var lstPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId == vaiTroId).ToList();
            var lstChucNang = DataProvider.ShopEntities.ChucNangs.ToList();

            //Lọc chức năng đã cấp
            var lstDaCap = (from p in lstPhanQuyen
                            join c in lstChucNang on p.ChucNangId equals c.Id
                            select new
                            {
                                p.ChucNangId,
                                c.TenChucNang
                            }).ToList();

            return Json(lstDaCap, JsonRequestBehavior.AllowGet);
        }

        //Thêm mới
        public JsonResult ThemJson(int vaiTroId, int chucNangChuaCapId, bool xem, bool them, bool sua, bool xoa)
        {
            //Khai báo 1 đối tượng
            PhanQuyen objPhanQuyen = new PhanQuyen();
            //Gán giá trị
            objPhanQuyen.VaiTroId = vaiTroId;
            objPhanQuyen.ChucNangId = chucNangChuaCapId;
            objPhanQuyen.Xem = xem;
            objPhanQuyen.Them = them;
            objPhanQuyen.Sua = sua;
            objPhanQuyen.Xoa = xoa;

            foreach (PhanQuyen x in DataProvider.ShopEntities.PhanQuyens.ToList())
            {
                if (chucNangChuaCapId == x.ChucNangId && vaiTroId == x.VaiTroId)
                {
                    return Json(1, JsonRequestBehavior.DenyGet);
                }
            }
            //Thực hiện thêm và lưu sự thay đổi
            DataProvider.ShopEntities.PhanQuyens.Add(objPhanQuyen);
            DataProvider.ShopEntities.SaveChanges();
            return Json("Thêm thành công", JsonRequestBehavior.AllowGet);
        }

        //Sửa

        public JsonResult ChiTietJson(int id,int idVaiTro)
        {
            PhanQuyen objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.ChucNangId == id && p.VaiTroId == idVaiTro).First();
            PhanQuyenViewModel objPhanQuyenView = new PhanQuyenViewModel();
            if (objPhanQuyen != null)
            {
                objPhanQuyenView.Id = objPhanQuyen.Id;
                objPhanQuyenView.ChucNangId = objPhanQuyen.ChucNangId;
                objPhanQuyenView.VaiTroId = objPhanQuyen.VaiTroId;
                objPhanQuyenView.Xem = objPhanQuyen.Xem;
                objPhanQuyenView.Them = objPhanQuyen.Them;
                objPhanQuyenView.Sua = objPhanQuyen.Sua;
                objPhanQuyenView.Xoa = objPhanQuyen.Xoa;
            }
            return Json(objPhanQuyenView, JsonRequestBehavior.AllowGet);
        }

        //Lưu
        public JsonResult LuuJson(int id, bool xem, bool them, bool sua, bool xoa,int idVaiTro)
        {
            //Lấy đối tượng cũ
            PhanQuyen objPhanQuyenOld = DataProvider.ShopEntities.PhanQuyens.Where(p => p.ChucNangId == id && p.VaiTroId== idVaiTro).First();

            PhanQuyenViewModel objPhanQuyenView = new PhanQuyenViewModel();
            objPhanQuyenView.VaiTroId = objPhanQuyenOld.VaiTroId;
            objPhanQuyenView.ChucNangId = objPhanQuyenOld.ChucNangId;
            objPhanQuyenView.Xem = xem;
            objPhanQuyenView.Them = them;
            objPhanQuyenView.Sua = sua;
            objPhanQuyenView.Xoa = xoa;
            objPhanQuyenView.Id = id;
            
            DataProvider.ShopEntities.Entry(objPhanQuyenOld).CurrentValues.SetValues(objPhanQuyenView);
            DataProvider.ShopEntities.SaveChanges();
            return Json("Sửa thành công", JsonRequestBehavior.AllowGet);
        }

        //Xoá

        public JsonResult XoaJson(int id,int idVaiTro)
        {
            PhanQuyen objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.ChucNangId == id && p.VaiTroId == idVaiTro).First();
            if (objPhanQuyen != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.PhanQuyens.Remove(objPhanQuyen);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(objPhanQuyen, JsonRequestBehavior.AllowGet);
        }
    }
}