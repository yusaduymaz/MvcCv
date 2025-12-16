using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;
using MvcCv.Repositories;

namespace MvcCv.Controllers
{
    public class IletisimController : Controller
    {
        IletisimRepository repo = new IletisimRepository();
        // GET: Iletisim
        public ActionResult Index()
        {
            var iletisim = repo.Listele();
            return View(iletisim);
        }
    }
}   