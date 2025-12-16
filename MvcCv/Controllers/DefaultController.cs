using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;

namespace MvcCv.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DbCvEntities db = new DbCvEntities();

        public ActionResult Index()
        {
            var degerler = db.TblHakkimda.ToList();
            return View(degerler);
        }
        public PartialViewResult SosyalMedya()
        {
            var SosyalMedya = db.TblSosyalMedya.ToList();
            return PartialView(SosyalMedya);
        }
        public PartialViewResult Deneyim()
        {
            var degerler = db.TblDeneyimlerim.ToList();
            return PartialView(degerler);
        }
        public PartialViewResult Egitim()
        {
            var degerler = db.TblEgitimlerim.ToList();
            return PartialView(degerler);
        }
        public PartialViewResult Yetenekler()
        {
            var degerler = db.TblYeteneklerim.ToList();
            return PartialView(degerler);
        }
        public PartialViewResult Hobiler()
        {
            var degerler = db.TblHobilerim.ToList();
            return PartialView(degerler);
        }
        public PartialViewResult Sertifikalar()
        {
            var degerler = db.TblSertifikalarim.ToList();
            return PartialView(degerler);
        }
        [HttpGet]
        public PartialViewResult Iletisim()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Iletisim(Tbl_Iletisim t)
        {
            t.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Tbl_Iletisim.Add(t);
            db.SaveChanges();
            return PartialView();
        }
    }
}