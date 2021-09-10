using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;
using WebApplication1.Context;
using System.Net;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WebApplication1.Controllers
{
    public class CarteController : Controller
    {
        // GET: Carte
        [HttpGet]
        public ActionResult AddCarte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCarte(Carte carte)
        {
            string fileName = Path.GetFileNameWithoutExtension(carte.ImageFile.FileName);
            string extension = Path.GetExtension(carte.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            carte.Imagine = "../Imagini/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Imagini/"), fileName);
            carte.ImageFile.SaveAs(fileName);
            using (BlogContext blog = new BlogContext())
            {
                blog.Carti.Add(carte);
                blog.SaveChanges();
                TempData["SuccessMessageCarte"] = "A fost adaugata o carte noua";
            }
            ModelState.Clear();
            return View();
        }

        //AFISARE
        public ActionResult Index()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Carti.ToList());
            }

        }

        //EDITARE
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Carti.Where(a => a.Id == id).FirstOrDefault());
            }

        }

        // POST: Edit
        [HttpPost]
        public ActionResult Edit(Carte carte)
        {
            if (ModelState.IsValid)
            {
              /*  string fileName = Path.GetFileNameWithoutExtension(carte.ImageFile.FileName);
                string extension = Path.GetExtension(carte.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                carte.Imagine = "../Imagini/" + fileName;
                fileName = Path.Combine(Server.MapPath("../Imagini/"), fileName);
                carte.ImageFile.SaveAs(fileName);*/

                using (BlogContext blog = new BlogContext()) //NU IMI SALVEAZA SI POZA 
                {
                    //blog.Entry(reteta).State = EntityState.Modified;
                    blog.Set<Carte>().AddOrUpdate(carte);
                    blog.SaveChanges();
                    TempData["EditCarte"] = "Cartea a fost editata"; //MESAJELE NU MERG LA PRIMA APLICARE
                }

                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Carti.Where(a => a.Id == id).FirstOrDefault());
            }
        }

        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id, Carte carte)
        {
            try
            {
                using (BlogContext blog = new BlogContext())
                {
                    carte = blog.Carti.Where(a => a.Id == id).FirstOrDefault();
                    blog.Carti.Remove(carte);
                    blog.SaveChanges();
                    TempData["DeleteCarte"] = "Cartea a fost stearsa";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}