using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Layer;
using ViewModels.Layer;

namespace StoredVehicles.Controllers
{
    public class AccountController : Controller
    {
        readonly IKorisniciService ks;

        public AccountController(IKorisniciService ks)
        {
            this.ks = ks;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = this.ks.InsertUser(rvm);
                Session["CurrentuserId"] = uid;
                Session["CurrentuserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
            }
            return View();
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                KorisnikViewModel kvm = this.ks.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if(kvm != null)
                {
                    Session["CurrentUserId"] = kvm.UserId;
                    Session["CurrentUserName"] = kvm.Name;
                    Session["CurrentUserEmail"] = kvm.Email;
                    Session["CurrentUserPassword"] = kvm.Password;
                    Session["CurrentUserIsAdmin"] = kvm.IsAdmin;

                    if (kvm.IsAdmin)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email/Password");
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(lvm);
            }
            return View(lvm);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            KorisnikViewModel kvm = this.ks.GetUsersByUserId(uid);
            EditKorisnikDetailsViewModel ekdvm = new EditKorisnikDetailsViewModel() { Name = kvm.Name, Email = kvm.Email, Mobile = kvm.Mobile, UserId = kvm.UserId };
            return View(ekdvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(EditKorisnikDetailsViewModel ekdvm)
        {
            if (ModelState.IsValid)
            {
                ekdvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.ks.UpdateUserDetails(ekdvm);
                Session["CurrentUserName"] = ekdvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(ekdvm);
            }
        }


        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            KorisnikViewModel kvm = this.ks.GetUsersByUserId(uid);
            EditKorisnikPasswordViewModel ekpvm = new EditKorisnikPasswordViewModel() {Email = kvm.Email, Password ="", ConfirmPassword ="", UserId = kvm.UserId };
            return View(ekpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(EditKorisnikPasswordViewModel ekpvm)
        {
            if (ModelState.IsValid)
            {
                ekpvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.ks.UpdateUserPassword(ekpvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(ekpvm);
            }
        }
    }
}