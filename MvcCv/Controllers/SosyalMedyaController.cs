using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;
using MvcCv.Repositories;

namespace MvcCv.Controllers
{
    public class SosyalMedyaController : Controller
    {
        // GET: SosyalMedya
        SosyalMedyaRepository repo = new SosyalMedyaRepository();
        public ActionResult Index()
        {
            var sosyalmedya = repo.Listele();
            return View(sosyalmedya);
        }
        [HttpGet]
        public ActionResult SosyalMedyaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SosyalMedyaEkle(TblSosyalMedya p)
        {
            if (!ModelState.IsValid)
            {
                return View("SosyalMedyaEkle");
            }
            repo.Tadd(p);
            return RedirectToAction("Index");
        }
        public ActionResult SosyalMedyaSil(int id)
        {
            TblSosyalMedya t = repo.Find(x => x.ID == id);
            repo.Tdel(t);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SosyalMedyaGetir(int? id)
        {
            if (id == null)
            {
                // ID gelmediyse ne yapacağını buraya yazarsın (örneğin listeye geri dön)
                return RedirectToAction("Index");
            }
            TblSosyalMedya t = repo.Find(x => x.ID == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult SosyalMedyaGetir(TblSosyalMedya p)
        {
            if (!ModelState.IsValid)
            {
                return View("SosyalMedyaGetir");
            }
            TblSosyalMedya t = repo.Find(x => x.ID == p.ID);
            t.Ad = p.Ad;
            t.Link = p.Link;
            t.Icon = p.Icon;
            repo.Tupdate(t);
            return RedirectToAction("Index");
        }
    }   
}