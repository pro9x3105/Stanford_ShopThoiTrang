using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        public ActionResult GioHang()
        {
            if (Session["carts"] != null)
            {
                List<Cart> lstCart = (List<Cart>)Session["carts"];
                return View(lstCart);
            }
            else
            {
                return RedirectToAction("NoCarts", "Cart");
            }
        }

        public ActionResult ThanhToan()
        {
            if (Session["carts"] != null)
            {
                List<Cart> lstCart = (List<Cart>)Session["carts"];

                //Lấy login khách hàng , nếu ko có thì là khách vãng lai
                if (Session["CTKhachHang"] != null)
                {
                    KhachHang lstKhachHang = (KhachHang)Session["CTKhachHang"];
                    ViewBag.HoTen = lstKhachHang.HoTen;
                    ViewBag.DienThoai = lstKhachHang.DienThoai;
                    ViewBag.Email = lstKhachHang.Email;
                    ViewBag.DiaChi = lstKhachHang.DiaChi;
                    ViewBag.LoaiKH = lstKhachHang.LoaiKhachHang.TenLoai;
                }
                return View(lstCart);
            }
            else
            {
                return RedirectToAction("NoCarts", "Cart");
            }

        }
        //Xoá toàn bộ giỏ hàng
        public JsonResult XoaToanBoGioHangJson()
        {
            Session.Remove("carts");
            Session.Remove("TongTien");
            Session.Remove("SoSP");
            return Json("Xoá toàn bộ thành công");
        }
        public ActionResult FinishCart()
        {
            return View();
        }

        public ActionResult NoCarts()
        {
            return View();
        }


        //Thêm mới
        public JsonResult ThemJson(string hoten, string dienthoai, string email, string diachi, string mota)
        {
            //Khai báo 1 đối tượng
            KhachHang objKhachHang = new KhachHang();
            //Gán giá trị
            objKhachHang.Id = 0;
            objKhachHang.HoTen = hoten;
            objKhachHang.DienThoai = dienthoai;
            objKhachHang.Email = email;
            objKhachHang.DiaChi = diachi;
            objKhachHang.LoaiKhachId = 12; //id=12 là khách vãng lai

            //Kiểm tra dữ liệu trùng thì lấy id cũ , do chưa làm login cho Khách hàng , sau còn phát triển thêm v1.0
            if (Session["CTKhachHang"] != null)
            {
                KhachHang objKHLogin = (KhachHang)Session["CTKhachHang"];
                if (objKHLogin.HoTen == hoten && objKHLogin.DienThoai == dienthoai && objKHLogin.Email == email && objKHLogin.DiaChi == diachi)
                {
                    objKhachHang.Id = objKHLogin.Id;
                    objKhachHang.LoaiKhachId = objKHLogin.LoaiKhachId;
                }
            }

            //Kiểm tra id khách hàng mới hay cũ , nếu mới thì tạo khách hàng mới trước đã
            if (objKhachHang.Id == 0)
            {
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.KhachHangs.Add(objKhachHang);
                DataProvider.ShopEntities.SaveChanges();
            }
            //Kiểm tra trùng tên hoá đơn ở objHoaDonBan
            var tenHDB = 1;
            for (int i = 0; i < DataProvider.ShopEntities.HoaDonBans.ToList().Count; i++)
            {
                var check = false;
                foreach (HoaDonBan x in DataProvider.ShopEntities.HoaDonBans.ToList())
                {
                    string s = x.TenHoaDon.Substring(3);
                    while (s.StartsWith("0") == true)
                    {
                        string z = s.Substring(1);
                        s = z;
                    }
                    if (tenHDB == Int32.Parse(s))
                    {
                        tenHDB++;
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    break;
                }
            }

            var stringHDB = "";
            if (tenHDB > 0 && tenHDB < 9)
            {
                stringHDB = "HDB" + "00000" + tenHDB;
            }
            else if (tenHDB > 10 && tenHDB < 99)
            {
                stringHDB = "HDB" + "0000" + tenHDB;
            }
            else if (tenHDB > 100 && tenHDB < 999)
            {
                stringHDB = "HDB" + "000" + tenHDB;
            }
            else if (tenHDB > 1000 && tenHDB < 9999)
            {
                stringHDB = "HDB" + "00" + tenHDB;
            }
            else if (tenHDB > 10000 && tenHDB < 99999)
            {
                stringHDB = "HDB" + "0" + tenHDB;
            }
            else if (tenHDB > 100000 && tenHDB < 999999)
            {
                stringHDB = "HDB" + "" + tenHDB;
            }
            else
            {
                return Json("Vượt quá giá trị cho phép", JsonRequestBehavior.DenyGet);
            }

            //Tạo mới thông tin HoaDonBan
            HoaDonBan objHoaDonBan = new HoaDonBan();
            objHoaDonBan.Id = 0;
            objHoaDonBan.TenHoaDon = stringHDB;
            objHoaDonBan.MoTa = mota;
            objHoaDonBan.NgayBan = DateTime.Now;
            objHoaDonBan.TrangThai = 0;
            objHoaDonBan.DaThanhToan = false;
            objHoaDonBan.KhachHangId = objKhachHang.Id;
            //Lưu
            DataProvider.ShopEntities.HoaDonBans.Add(objHoaDonBan);
            DataProvider.ShopEntities.SaveChanges();

            //Tìm tenHDB để kiếm id liên kết bảng
            HoaDonBan objHDB = DataProvider.ShopEntities.HoaDonBans.Where(p=>p.TenHoaDon == stringHDB).First();
            int IdHDB = objHDB.Id;

            //Thêm đơn đặt hàng bằng Session
            List<Cart> lstSanPham = (List<Cart>)Session["carts"];
            for (int i = 0; i < lstSanPham.Count; i++)
            {
                SanPham sanPham = DataProvider.ShopEntities.SanPhams.Find(lstSanPham[i].Id);
                HoaDonChiTiet objHoaDonCT = new HoaDonChiTiet();
                objHoaDonCT.Id = 0;
                objHoaDonCT.SanPhamId = sanPham.Id;
                objHoaDonCT.HoaDonId = IdHDB;
                objHoaDonCT.SoLuong = lstSanPham[i].SoLuong;
                objHoaDonCT.Gia = lstSanPham[i].Gia;
                //Lưu
                DataProvider.ShopEntities.HoaDonChiTiets.Add(objHoaDonCT);
                DataProvider.ShopEntities.SaveChanges();
                if (i == lstSanPham.Count-1)
                {
                    //Reset session
                    Session.Remove("carts");
                    Session.Remove("TongTien");
                    Session.Remove("SoSP");
                    return Json(new { Url = "/Cart/FinishCart" });
                }
            }
            return Json("Đặt hàng không thành công", JsonRequestBehavior.DenyGet);
        }

        //Thêm giỏ hàng bằng session
        public JsonResult ThemGioHangJson(int id)
        {
            bool check = false;
            List<Cart> lstCart;
            SanPham sanpham = DataProvider.ShopEntities.SanPhams.Where(p => p.Id == id).First<SanPham>();
            //Khai báo 1 đối tượng , nếu chưa có thì tạo mới , nếu có rồi thì lấy giỏ hàng cũ
            if (Session["carts"] == null)
            {
                lstCart = new List<Cart>();
            }
            else
            {
                lstCart = (List<Cart>)Session["carts"];
            }

            
            //Nếu tồn tại sản phẩm này rồi thì +1 số lượng
            for (int i = 0; i < lstCart.Count; i++)
            {
                if(id == lstCart[i].Id)
                {
                    lstCart[i].SoLuong = lstCart[i].SoLuong + 1;
                    check = true;
                    break;
                }
            }
            //Thêm vào danh sách list , nếu ko có trùng thì thêm
            if (check == false)
            {
                Cart objCart = new Cart();

                //Gán giá trị
                objCart.Id = id;
                objCart.TenSP = sanpham.TenSanPham;
                objCart.MoTa = sanpham.MoTa;
                objCart.Anh = sanpham.AnhSanPham;
                objCart.Gia = (double)sanpham.Gia;
                objCart.SoLuong = 1;
                lstCart.Add(objCart);
            }
            //Add lại Session
            Session["carts"] = lstCart;
            Session["SoSP"] = lstCart.Count();
            double TongTien = 0;
            for (int i = 0; i < lstCart.Count; i++)
            {
                TongTien += lstCart[i].Gia * (double)lstCart[i].SoLuong;
            }
            Session["TongTien"] = TongTien;
            return Json("Thêm vào giỏ hàng thành công", JsonRequestBehavior.AllowGet);
        }

        //Xoá giỏ hàng Json
        public JsonResult XoaGioHangJson(int id)
        {
            List<Cart> lstCart = (List<Cart>)Session["carts"];
            for (int i = 0; i < lstCart.Count; i++)
            {
                if (id == lstCart[i].Id)
                {
                    lstCart.RemoveAt(i);
                    break;
                }
            }
            //Add lại Session
            
            
            Session["SoSP"] = lstCart.Count();
            double TongTien = 0;
            for (int i = 0; i < lstCart.Count; i++)
            {
                TongTien += lstCart[i].Gia * (double)lstCart[i].SoLuong;
            }
            Session["TongTien"] = TongTien;
            if (lstCart.Count==0)
            {
                Session.Remove("carts");

            }
            else
            {
                Session["carts"] = lstCart;
            }
            return Json("Xoá thành công", JsonRequestBehavior.AllowGet);
        }

        //Thêm bớt số lượng Json
        public JsonResult ThemBotSoLuongJson(int id,int soluong)
        {
            List<Cart> lstCart = (List<Cart>)Session["carts"];
            for (int i = 0; i < lstCart.Count; i++)
            {
                if (id == lstCart[i].Id)
                {
                    lstCart[i].SoLuong += soluong;
                    if (lstCart[i].SoLuong == 0)
                    {
                        lstCart.RemoveAt(i);
                    }
                    break;
                }
            }
            //Add lại Session


            Session["SoSP"] = lstCart.Count();
            double TongTien = 0;
            for (int i = 0; i < lstCart.Count; i++)
            {
                TongTien += lstCart[i].Gia * (double)lstCart[i].SoLuong;
            }
            Session["TongTien"] = TongTien;
            if (lstCart.Count == 0)
            {
                Session.Remove("carts");

            }
            else
            {
                Session["carts"] = lstCart;
            }
            return Json("Xoá thành công", JsonRequestBehavior.AllowGet);
        }

    }
}