using MvcCv.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;

namespace MvcCv.Controllers
{
    public class SertifikaController : Controller
    {
        // GET: Sertifika
        SertifikalarRepository repo = new SertifikalarRepository();
        public ActionResult Index()
        {
            var sertifikalar = repo.Listele();
            return View(sertifikalar);
        }
        [HttpGet]
        public ActionResult SertifikaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SertifikaEkle(TblSertifikalarim p)
        {
            if (!ModelState.IsValid)
            {
                return View("SertifikaEkle");
            }
            repo.Tadd(p);
            return RedirectToAction("Index");
        }
        public ActionResult SertifikaSil(int? id)
        {
            TblSertifikalarim t = repo.Find(x => x.ID == id);
            repo.Tdel(t);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult SertifikaGetir(int? id)
        {
            if (id == null)
            {
                // ID gelmediyse ne yapacağını buraya yazarsın (örneğin listeye geri dön)
                return RedirectToAction("Index");
            }
            TblSertifikalarim t =  repo.Find(x => x.ID == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult SertifikaGetir(TblSertifikalarim p)
        {
            if (!ModelState.IsValid)
            {
                return View("SertifikaGetir");
            }
            TblSertifikalarim t = repo.Find(x => x.ID == p.ID);
            t.Tarih = p.Tarih;
            t.Aciklama = p.Aciklama;
            repo.Tupdate(t);
            return RedirectToAction("Index");

        }
    }
}