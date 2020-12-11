using Stanford_ShopThoiTrang.Areas.Admin.Models;
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class CheckPhanQuyenLogin:AuthorizeAttribute
    {
        //Khai bao quyen va chuc nang tuong ung
        public string tenChucNang { get; set; }
        NguoiDung objNguoiDung;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Lay thong tin luu vao session
           objNguoiDung = (NguoiDung)HttpContext.Current.Session["useronline"];

            if(objNguoiDung != null)
            {
                //Lay thong tin vai tro
                int vaiTroId = objNguoiDung.VaiTroId;
                ChucNang objChucNang = DataProvider.ShopEntities.ChucNangs.Where(p => p.TenForm.Equals(tenChucNang)).First();
                if (objChucNang != null)
                {
                    PhanQuyen objPhanQuyen = null;
                    try
                    {
                        objPhanQuyen = DataProvider.ShopEntities.PhanQuyens.Where(p => p.VaiTroId != null && p.VaiTroId == vaiTroId && p.ChucNangId == objChucNang.Id).First<PhanQuyen>();
                        if (objPhanQuyen != null && objPhanQuyen.Xem.HasValue)
                        {
                            if (objPhanQuyen.Xem.Value)
                            {
                                return true;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (objNguoiDung == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login", Area = "Admin" }));
            }
            else
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Areas/Admin/Views/Login/NoPermisson.cshtml"
                };
            }
            
        }

    }
}