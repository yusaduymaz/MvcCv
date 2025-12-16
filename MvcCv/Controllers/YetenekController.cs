using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;
using MvcCv.Repositories;

namespace MvcCv.Controllers
{
    public class YetenekController : Controller
    {
        // GET: Yetenek
        DbCvEntities db = new DbCvEntities();
        YeteneklerRepository repo = new YeteneklerRepository();
        public ActionResult Index()
        {
            var yetenekler = repo.Listele();
            return View(yetenekler);
        }
        [HttpGet]
        public ActionResult YetenekEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YetenekEkle(TblYeteneklerim p)
        {
            repo.Tadd(p);
            return RedirectToAction("Index");
        }
        public ActionResult YetenekSil(int id)
        {
            TblYeteneklerim t = repo.Find(x => x.ID == id);
            repo.Tdel(t);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult YetenekGetir(int? id)
        {

            if (id == null)
            {
                // ID gelmediyse ne yapacağını buraya yazarsın (örneğin listeye geri dön)
                return RedirectToAction("Index");
            }
            TblYeteneklerim t = repo.Find(x => x.ID == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult YetenekGetir(TblYeteneklerim p)
        {
            TblYeteneklerim t = repo.Find(x => x.ID == p.ID);
            t.Yetenek = p.Yetenek;
            t.Oran = p.Oran;
            repo.Tupdate(t);
            return RedirectToAction("Index");
        }
    }
}