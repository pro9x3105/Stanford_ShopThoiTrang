using Stanford_ShopThoiTrang.Areas.Admin.Models;
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    //NQA - 11/7/2020 - Chỉnh sửa thêm check trùng lặp tài khoản và xem chi tiết có thêm dropdownlist
    //NQA - 18/7/2020 - Thêm lại phân quyền , bỏ basecontroller , tuỳ biến các nút thêm sửa xoá

    
    public class NguoiDungController : Controller
    {
        
        [CheckPhanQuyenLogin(tenChucNang = "NguoiDung")]
        public ActionResult DanhSach()
        {
            var lstNguoiDung = DataProvider.ShopEntities.NguoiDungs.ToList();
            ViewBag.VaiTro = new SelectList(DataProvider.ShopEntities.VaiTroes.ToList(), "Id", "TenVaiTro");
            //Lấy VaiTroId và các quyền
            var vaiTroId = Int32.Parse(Session["VaiTroId"].ToString());
            PhanQuyen objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId != null && p.VaiTroId == vaiTroId && p.ChucNangId == 45).First<PhanQuyen>();
            //Gán các nút hiển thị
            ViewBag.Them = objPhanQuyen.Them == true ? "normal" : "none";
            ViewBag.Sua = objPhanQuyen.Sua == true ? "normal" : "none";
            ViewBag.Xoa = objPhanQuyen.Xoa == true ? "normal" : "none";

            return View(lstNguoiDung);
        }

        //Thêm mới
        public JsonResult ThemJson(string taiKhoan, string matKhau, string hoTen, string dienThoai, string email, string diaChi, int vaiTro, int id)
        {
            //Khai báo 1 đối tượng
            NguoiDung objnguoiDung = new NguoiDung();
            //Gán giá trị
            objnguoiDung.TaiKhoan = taiKhoan;
            objnguoiDung.MatKhau = Encryptor.MD5Hash(matKhau);
            objnguoiDung.HoTen = hoTen;
            objnguoiDung.DienThoai = dienThoai;
            objnguoiDung.Email = email;
            objnguoiDung.DiaChi = diaChi;
            objnguoiDung.VaiTroId = vaiTro;
            

            if (id > 0)//TH sửa
            {
                //Kiểm tra trùng lặp tên tài khoản ngoại trừ đang sửa
                foreach (NguoiDung x in DataProvider.ShopEntities.NguoiDungs.Where(p=>p.Id != id).ToList())
                {
                    if (taiKhoan == x.TaiKhoan)
                    {
                        return Json("Trùng lặp tên Tài Khoản", JsonRequestBehavior.DenyGet);
                    }
                }
                objnguoiDung.Id = id;
                //Lấy đối tượng cũ
                NguoiDung objnguoiDungOld = DataProvider.ShopEntities.NguoiDungs.Where(p => p.Id == id).First();
                DataProvider.ShopEntities.Entry(objnguoiDungOld).CurrentValues.SetValues(objnguoiDung);
                DataProvider.ShopEntities.SaveChanges();
                return Json("Sửa thành công", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Kiểm tra trùng lặp tên tài khoản
                foreach (NguoiDung x in DataProvider.ShopEntities.NguoiDungs)
                {
                    if (taiKhoan == x.TaiKhoan)
                    {
                        return Json("Trùng lặp tên Tài Khoản", JsonRequestBehavior.DenyGet);
                    }
                }
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.NguoiDungs.Add(objnguoiDung);
                DataProvider.ShopEntities.SaveChanges();
                return Json("Thêm thành công", JsonRequestBehavior.AllowGet);
            }

        }
        //Sửa
        public JsonResult ChiTietJson(int id)
        {
            NguoiDung objNguoiDung = DataProvider.ShopEntities.NguoiDungs.Where(p => p.Id == id).First();
            NguoiDungViewModel objNguoiDungView = new NguoiDungViewModel();
            if (objNguoiDung != null)
            {
                objNguoiDungView.Id = objNguoiDung.Id;
                objNguoiDungView.TaiKhoan = objNguoiDung.TaiKhoan;
                objNguoiDungView.HoTen = objNguoiDung.HoTen;
                objNguoiDungView.DienThoai = objNguoiDung.DienThoai;
                objNguoiDungView.Email = objNguoiDung.Email;
                objNguoiDungView.DiaChi = objNguoiDung.DiaChi;
                objNguoiDungView.VaiTroId = objNguoiDung.VaiTroId;
            }
            return Json(objNguoiDungView, JsonRequestBehavior.AllowGet);
        }
        //Xoá
        public JsonResult XoaJson(int id)
        {
            NguoiDung objNguoiDung = DataProvider.ShopEntities.NguoiDungs.Where(p => p.Id == id).First();
            if (objNguoiDung != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.NguoiDungs.Remove(objNguoiDung);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(objNguoiDung, JsonRequestBehavior.AllowGet);
        }
    }
}