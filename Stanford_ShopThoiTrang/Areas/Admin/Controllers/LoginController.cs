using Stanford_ShopThoiTrang.Areas.Admin.Models;
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    //NQA - 11/7/2020 - Chỉnh sửa login phân biệt tài khoản và mật khẩu , thêm check trùng lặp tài khoản và đăng ký
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Login()
        {
            if (Session["HoTen"] != null)
            {
                try
                {
                    return RedirectToAction("DanhSach", "NguoiDung");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NguoiDung objNguoiDungLogin)
        {
            if (ModelState.IsValid)
            {
                var passwordMD5 = Encryptor.MD5Hash(objNguoiDungLogin.MatKhau);
                var obj = DataProvider.ShopEntities.NguoiDungs.Where(p => p.TaiKhoan.Equals(objNguoiDungLogin.TaiKhoan)).FirstOrDefault();
                if (obj != null)
                {
                    if(obj.MatKhau == passwordMD5)
                    {
                        Session["HoTen"] = obj.HoTen.ToString();
                        Session["VaiTroId"] = obj.VaiTroId.ToString();
                        Session["userid"] = obj.Id.ToString();
                        //Luu ca objNguoiDung
                        Session["useronline"] = obj;

                        //Lưu thông tin vào biến
                        Common.UserInfo = obj;
                        Common.UserId = obj.Id;

                        //Ghi log
                        NhatKyCommon.Them("Đăng nhập hệ thống thành công", "Đăng nhập");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Sai mật khẩu";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Không tồn tại tài khoản này trong hệ thống";
                    return View();
                }
            }
            return View(objNguoiDungLogin);
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        //Đăng ký
        public JsonResult ThemJson(string taiKhoan, string matKhau, string hoTen, string dienThoai, string email, string diaChi)
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
            objnguoiDung.VaiTroId = 5;      // 5 là mã id của khách hàng
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
            return Json("Đăng ký thành công", JsonRequestBehavior.AllowGet);
        }
    }
}