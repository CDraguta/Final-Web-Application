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
    public class ReteteController : Controller
    {

       [HttpGet]
        public ActionResult AddRetete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRetete(Reteta reteta)
        {
            string fileName = Path.GetFileNameWithoutExtension(reteta.ImageFile.FileName);
            string extension = Path.GetExtension(reteta.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reteta.Imagine = "../Imagini/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Imagini/"),fileName);
            reteta.ImageFile.SaveAs(fileName);
            using (BlogContext blog = new BlogContext())
            {
                blog.Retete.Add(reteta);
                blog.SaveChanges();
                TempData["SuccessMessageReteta"] = "A fost adaugata o noua reteta";
            }
            ModelState.Clear();
            return View();
        }

        //AFISARE
     
        public ActionResult Index(string searching)
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(x => x.Nume.Contains(searching) || searching == null).ToList());
            }
        }

        public ActionResult Pranz()
        {
            using(BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a=>a.TipMasa == "Pranz").ToList());
            }
        }

        public ActionResult FelPrincipal()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipMasa == "Fel principal").ToList());
            }
        }

        public ActionResult Desert()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipMasa == "Desert").ToList());
            }
        }

        public ActionResult Aperitiv()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipMasa == "Aperitiv").ToList());
            }
        }

        //tip bucatarie
        public ActionResult Romaneasca()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipBucatarie == "Romaneasca").ToList());
            }
        }

        public ActionResult Asiatica()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipBucatarie == "Asiatica").ToList());
            }
        }

        public ActionResult Germana()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipBucatarie == "Germana").ToList());
            }
        }

        public ActionResult Italiana()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.TipBucatarie == "Italiana").ToList());
            }
        }

        //fel de mancare
        public ActionResult SupeCiorbe()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.FelMancare == "Supe/Ciorbe").ToList());
            }
        }
        public ActionResult PrajituriTorturi()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.FelMancare == "Prajituri/Torturi").ToList());
            }
        }
        public ActionResult Legume()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.FelMancare == "Legume").ToList());
            }
        }
        public ActionResult Peste()
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a => a.FelMancare == "Peste").ToList());
            }
        }

        //EDIT
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (BlogContext blog = new BlogContext())
            {
                return View(blog.Retete.Where(a=> a.Id == id).FirstOrDefault());
                
            }
           
        }

        // POST: Edit
        [HttpPost]
        public ActionResult Edit(Reteta reteta)
        {
                if (ModelState.IsValid) { 
            

                using (BlogContext blog = new BlogContext()) //NU IMI SALVEAZA SI POZA 
                    {
                    //blog.Entry(reteta).State = EntityState.Modified;
                    blog.Set<Reteta>().AddOrUpdate(reteta);
                        blog.SaveChanges();
                    TempData["EditReteta"] = "Reteta a fost editata";
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
                return View(blog.Retete.Where(a => a.Id == id).FirstOrDefault());
            }
        }

        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id, Reteta reteta )
        {
            try
            {
                using (BlogContext blog = new BlogContext())
                {
                    reteta = blog.Retete.Where(a => a.Id == id).FirstOrDefault();
                    blog.Retete.Remove(reteta);
                    blog.SaveChanges();
                    TempData["DeleteReteta"] = "Reteta a fost stearsa";
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