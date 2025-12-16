using MvcCv.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcCv.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
       
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TblAdmin p)
        {
            if (!ModelState.IsValid)
            {
                return View("Index","Login");
            }
            DbCvEntities db = new DbCvEntities();
            var bilgi=db.TblAdmin.FirstOrDefault(x=>x.KullaniciAdi==p.KullaniciAdi && x.Sifre==p.Sifre);

            if(bilgi!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.KullaniciAdi, false);
                return RedirectToAction("Index", "Hakkimda");
            }
            else
                {
                var mesaj = "Hatalı Kullanıcı Adı veya Şifre";
                ViewBag.Mesaj = mesaj;
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut(); // Çerezleri temizler (Bileti yırtar)
            Session.Abandon(); // Varsa session'ı sonlandırır
            return RedirectToAction("Index", "Login"); // Login sayfasına geri gönderir
        }

        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(TblAdmin p)
        {
            // Eğer gizli alan doluysa, kesinlikle bottur!
            if (!string.IsNullOrEmpty("WebsiteUrl"))
            {
                // Hiçbir şey yapma, sanki kaydetmiş gibi Login'e at (Böylece bot başardığını sanar, sistemi zorlamaz)
                return RedirectToAction("Index", "Login");
            }
            // 1. IP Adresini al
            string ipAdresi = Request.UserHostAddress;
            string cacheKey = "SonKayit_" + ipAdresi;

            // 2. Cache kontrolü: Cezası var mı?
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                // Cache'teki bitiş zamanını çekiyoruz
                DateTime cezaBitisSuresi = (DateTime)HttpRuntime.Cache[cacheKey];

                // Şu anki zaman ile bitiş zamanı arasındaki farkı buluyoruz
                TimeSpan kalanSure = cezaBitisSuresi - DateTime.Now;

                // Kullanıcıya gösterilecek mesajı hazırlıyoruz
                // Örn: "Lütfen 1 dakika 30 saniye sonra tekrar deneyin."
                ViewBag.Uyari = string.Format("Çok sık işlem yaptınız. Lütfen {0} dakika {1} saniye sonra tekrar deneyin.",
                                              kalanSure.Minutes,
                                              kalanSure.Seconds);

                // İşlemi yapmadan sayfayı geri döndürüyoruz
                return View();
            }

            // --- Model Kontrolü ve Kayıt İşlemleri ---
            if (!ModelState.IsValid)
            {
                return View("KayitOl");
            }

            DbCvEntities db = new DbCvEntities();
            db.TblAdmin.Add(p);
            db.SaveChanges();
            // ----------------------------------------

            // 3. İşlem başarılı, şimdi IP'yi 2 dakikalığına banlıyoruz (Cache'e atıyoruz)
            // ÖNEMLİ: Değer (Value) kısmına bitiş zamanını kaydediyoruz ki yukarıda okuyabilelim.
            DateTime bitisZamani = DateTime.Now.AddMinutes(10);

            HttpRuntime.Cache.Insert(
                cacheKey,
                bitisZamani, // <-- Buraya "true" yerine "bitisZamani" yazıyoruz
                null,
                bitisZamani,
                System.Web.Caching.Cache.NoSlidingExpiration
            );

            return RedirectToAction("Index", "Login");
        }
    }
}