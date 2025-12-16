using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;
using MvcCv.Repositories;

namespace MvcCv.Controllers
{
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        HakkimdaRepository repo = new HakkimdaRepository();
        public ActionResult Index()
        {
            var Hakkimda = repo.Listele();
            return View(Hakkimda);
        }
        public ActionResult HakkimdaGetir(int? id)
        {
            if (id == null)
            {
                // ID gelmediyse ne yapacağını buraya yazarsın (örneğin listeye geri dön)
                return RedirectToAction("Index");
            }
            TblHakkimda t = repo.Find(x => x.ID == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult HakkimdaGetir(TblHakkimda p)
        {
            if (!ModelState.IsValid)
            {
                return View("EgitimGetir");
            }
            TblHakkimda t = repo.Find(x => x.ID == p.ID);
            t.Ad = p.Ad;
            t.Soyad = p.Soyad;
            t.Adres = p.Adres;
            t.Telefon = p.Telefon;
            t.Mail = p.Mail;
            t.Aciklama = p.Aciklama;
            t.Resim = p.Resim;
            repo.Tupdate(t);
            return RedirectToAction("Index");
        }
    }
}