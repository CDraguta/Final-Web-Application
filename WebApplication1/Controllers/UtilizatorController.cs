using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Context;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class UtilizatorController : Controller
    {

        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode ")] Utilizator user)
        {
            bool Status = false;
            string message = "";
            //Model Validation
            if (ModelState.IsValid)
            {
                #region //Email is already Exist
                var isExist = IsEmailExist(user.EmailId);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email-ul exista deja");
                    return View(user);
                }
                #endregion

                #region //Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region //Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion

                user.IsEmailVerified = false;

                #region //Save data to Database
                using (BlogContext dc = new BlogContext())
                {
                    dc.Utilizatori.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(user.EmailId, user.ActivationCode.ToString());
                    message ="Link-ul de activare al contului a fost trimis catre contul dumneavoastra de email " + user.EmailId;
                    Status = true;
                }
                #endregion

            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(user);
        }

        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (BlogContext blog = new BlogContext())
            {
                blog.Configuration.ValidateOnSaveEnabled = false; 
                var v = blog.Utilizatori.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    blog.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UtilizatorLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (BlogContext blog = new BlogContext())
            {
                var v = blog.Utilizatori.Where(a => a.EmailId == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; //525600 = 1an 
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            if(login.EmailID == "mdragu27@gmail.com" && login.Password == "123456")
                            {
                                HttpCookie manager = new HttpCookie("manager", "mdragu");
                                Response.Cookies.Add(manager);
                            }

                            HttpCookie userLogat = new HttpCookie("userLogat", "logat");
                            Response.Cookies.Add(userLogat);
                            return RedirectToAction("Index", "Home"); 
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //delete cookie "manager":
            Response.Cookies["manager"].Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(Response.Cookies["manager"]);

            //delete cookie "userLogat" :
            Response.Cookies["userLogat"].Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(Response.Cookies["userLogat"]);

            return RedirectToAction("Login", "Utilizator");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (BlogContext blog = new BlogContext())
            {
                var v = blog.Utilizatori.Where(a => a.EmailId == emailID).FirstOrDefault();
                return v != null;
            }
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Utilizator/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("blogCulinar@gmail.com", "Blog culinar");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "12345Mm.12345"; //Replace with actual password 10*
            string subject = "Contul tau a fost creat cu succes!";

            string body = "<br/><br/> Suntem incantati sa va anuntam ca a fost creat cu succes contul." +
                "Va rog sa apasati pe link-ul de mai jos pentru verificare." +
                "<br/><br/><a href='" + link + "'>" + link + " </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) 

                smtp.Send(message);
        }
    }
}