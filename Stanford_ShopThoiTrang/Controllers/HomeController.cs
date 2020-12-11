using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Stanford_ShopThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TrangChu()
        {
            List<ChuDe> lstChuDe = DataProvider.ShopEntities.ChuDes.ToList();           
            return View(lstChuDe);
        }
        [HttpGet]
        public ActionResult SanPham(int? Id,string txtTuKhoa)
        {            
            IQueryable<SanPham> lstSanPham = DataProvider.ShopEntities.SanPhams.Where(x=>x.DaDuyet==true);
            if(Id.HasValue)
            {
                lstSanPham = lstSanPham.Where(x => x.ChuDeId == Id.Value);
            }
            if (!string.IsNullOrEmpty(txtTuKhoa))
            {
                lstSanPham = lstSanPham.Where(x => x.TenSanPham.ToLower().Contains(txtTuKhoa.ToLower()));
            }                    
            return View(lstSanPham.ToList());
        }    
        [HttpGet]
        public ActionResult ChiTietSanPham(int? Id)
        {
            int id = 0;
            if(Id.HasValue)
            {
                id = Id.Value;
            }
            SanPham objSanPham = DataProvider.ShopEntities.SanPhams.Find(id);
            List<SelectListItem> lstMauSP = new List<SelectListItem>();
            List<SelectListItem> lstKichCoSP = new List<SelectListItem>();
            string CacMau = objSanPham.Mau;
            string KichCo = objSanPham.KichCo;
            string[] ArrMau = CacMau.Split(',');
            string[] ArrKichCo = KichCo.Split(',');
            for(int i=0;i< ArrMau.Length;i++)
            {
                lstMauSP.Add(new SelectListItem() {Value= ArrMau[i],Text= ArrMau[i] });
            }
            for(int i=0;i<ArrKichCo.Length;i++)
            {
                lstKichCoSP.Add(new SelectListItem() { Value = ArrKichCo[i], Text = ArrKichCo[i] });
            }
            ViewBag.MauSanPham = new SelectList(lstMauSP,"Value","Text");
            ViewBag.KichCoSanPham = new SelectList(lstKichCoSP, "Value", "Text");
            return View(objSanPham);
        }       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult AutoComplete()
        {
            List<string> lstAuto = new List<string>();
            foreach (var item in DataProvider.ShopEntities.SanPhams)
            {
                lstAuto.Add(item.TenSanPham);
            }
            return Json(lstAuto, JsonRequestBehavior.AllowGet);
        }
    }
}