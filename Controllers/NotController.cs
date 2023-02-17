using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NotUygulamasıUnited.Models;

namespace NotUygulamasıUnited.Controllers
{
    public class NotController : Controller
    {
        private NotContext db = new NotContext();

        // GET: Not
        public ActionResult Index()
        {
            // Tüm notları veritabanından alır ve view'a gönderir
            return View(db.Notlar.ToList());
        }

        // GET: Not/Yeni
        public ActionResult Yeni()
        {
            // Yeni bir not oluşturma view'ını açar
            return View();
        }

        // POST: Not/Yeni
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni([Bind(Include = "NotId,Baslik,Icerik,UstNotId")] Not not)
        {
            if (ModelState.IsValid)
            {
                // Yeni notu veritabanına ekler ve değişiklikleri kaydeder
                db.Notlar.Add(not);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Eğer model doğrulama başarısız olursa, aynı view'u tekrar açar
            return View(not);
        }

        // GET: Not/Duzenle/5
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Düzenlenecek notu veritabanından bulur ve view'a gönderir
            Not not = db.Notlar.Find(id);

            if (not == null)
            {
                return HttpNotFound();
            }

            return View(not);
        }

        // POST: Not/Duzenle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "NotId,Baslik,Icerik,UstNotId")] Not not)
        {
            if (ModelState.IsValid)
            {
                // Düzenlenen notu veritabanında günceller ve değişiklikleri kaydeder
                db.Entry(not).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Eğer model doğrulama başarısız olursa, aynı view'u tekrar açar
            return View(not);
        }

        // GET: Not/Sil/5
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Silinecek notu veritabanından bulur ve view'a gönderir
            Not not = db.Notlar.Find(id);

            if (not == null)
            {
                return HttpNotFound();
            }

            return View(not);
        }
        // eğer notu veritabanından silmek istemezsek durum tanılayıp true dan false çeviririz ön yüzde gözükmez ama veritabanında gözükecek şekilde olur ( deger.Durum = false;)
        // aşağıda direkt silme işlemi uygulanmıştır.
        // POST: Not/Sil
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult SilConfirmed(int id)
        {
            Not not = db.Notlar.Find(id);

            if (not == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Notlar.Remove(not);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Silme işlemi sırasında bir hata oluştu: " + ex.Message;
                return View("Sil", not);
            }

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}