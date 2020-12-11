using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    //NQA - 11/7/2020 - Chỉnh sửa thêm check trùng lặp tên vai trò
    //NQA 19/7/2020 - Bỏ BaseController và thêm phân quyền
    public class VaiTroController : Controller
    {
        [CheckPhanQuyenLogin(tenChucNang = "VaiTro")]
        public ActionResult DanhSach(string searchVaiTro)
        {
            var lstVaiTro = DataProvider.ShopEntities.VaiTroes.ToList();

            //Lấy VaiTroId và các quyền
            var vaiTroId = Int32.Parse(Session["VaiTroId"].ToString());
            PhanQuyen objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId != null && p.VaiTroId == vaiTroId && p.ChucNangId == 9).First<PhanQuyen>();
            //Gán các nút hiển thị
            ViewBag.Them =objPhanQuyen.Them== true ? "normal" : "none";
            ViewBag.Sua = objPhanQuyen.Sua == true ? "normal" : "none";
            ViewBag.Xoa = objPhanQuyen.Xoa == true ? "normal" : "none";

            return View(lstVaiTro);
        }
        //Thêm mới
        public JsonResult ThemJson(string tenVaiTro, string moTa, int id)
        {
            //Khai báo 1 đối tượng
            VaiTro objVaiTro = new VaiTro();
            //Gán giá trị
            objVaiTro.TenVaiTro = tenVaiTro;
            objVaiTro.MoTa = moTa;

            
            if (id > 0)//TH sửa
            {
                //Kiểm tra trùng lặp tên vai trò ngoại trừ id đang sửa
                foreach (VaiTro x in DataProvider.ShopEntities.VaiTroes.Where(p => p.Id != id).ToList())
                {
                    if (tenVaiTro == x.TenVaiTro)
                    {
                        return Json("Trùng lặp tên vai trò", JsonRequestBehavior.AllowGet);
                    }
                }
                objVaiTro.Id = id;
                //Lấy đối tượng cũ
                VaiTro objVaiTroOld = DataProvider.ShopEntities.VaiTroes.Where(p => p.Id == id).First();

                DataProvider.ShopEntities.Entry(objVaiTroOld).CurrentValues.SetValues(objVaiTro);
                DataProvider.ShopEntities.SaveChanges();
                return Json("Sửa thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Kiểm tra trùng lặp tên vai trò
                foreach (VaiTro x in DataProvider.ShopEntities.VaiTroes)
                {
                    if (tenVaiTro == x.TenVaiTro)
                    {
                        return Json("Trùng lặp tên vai trò", JsonRequestBehavior.AllowGet);
                    }
                }
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.VaiTroes.Add(objVaiTro);
                DataProvider.ShopEntities.SaveChanges();
                return Json("Thêm thành công", JsonRequestBehavior.AllowGet);
            }

        }
        //Sửa
        public JsonResult ChiTietJson(int id)
        {
            VaiTro objVaiTro = DataProvider.ShopEntities.VaiTroes.Where(p => p.Id == id).First();

            VaiTroViewModel objVaiTroView = new VaiTroViewModel();
            if(objVaiTro != null)
            {
                objVaiTroView.Id = objVaiTro.Id;
                objVaiTroView.TenVaiTro = objVaiTro.TenVaiTro;
                objVaiTroView.MoTa = objVaiTro.MoTa;
            }
            return Json(objVaiTroView, JsonRequestBehavior.AllowGet);
        }
        //Xoá
        public JsonResult XoaJson(int id)
        {
            VaiTro objVaiTro = DataProvider.ShopEntities.VaiTroes.Where(p => p.Id == id).First();
            if (objVaiTro != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.VaiTroes.Remove(objVaiTro);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(objVaiTro, JsonRequestBehavior.AllowGet);
        }
    }
}