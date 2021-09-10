using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CosController : Controller
    {
        BlogContext blog = new BlogContext();
       
        public Cos GetCos()
        { //daca sterg o carte din lista , ramane in cos 
            Cos cart = Session["Cart"] as Cos;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cos();
                Session["Cart"] = cart;
            }
            return cart;

        }
        public ActionResult AddtoCart(int id)
        {
            var pro = blog.Carti.SingleOrDefault(s => s.Id == id);
            if (pro != null)
            {
                GetCos().Add(pro);
            }

            return RedirectToAction("Index", "Carte");
        }

        public ActionResult ShowToCart()
        {
            Cos cart = Session["Cart"] as Cos;
            return View(cart);
        }

        public ActionResult UpdateCantitate(FormCollection form)
        {
            Cos cart = Session["Cart"] as Cos;
            int id_pro = int.Parse(form["IdCarte"]);
            int cantitate = int.Parse(form["Cantitate"]);
            cart.UpdateCantitate(id_pro, cantitate);
            return RedirectToAction("ShowToCart", "Cos");
        }

        public ActionResult RemoveCos(int id)
        {
            Cos cart = Session["Cart"] as Cos;
            cart.RemoveElement(id);
            return RedirectToAction("ShowToCart", "Cos");
        }

    }
}