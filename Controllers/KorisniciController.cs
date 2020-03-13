using DomainModels.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoredVehicles.Controllers
{
    public class KorisniciController : Controller
    {
        // GET: Korisnici
        public ActionResult Index()
        {
            StoredVehiclesDatabaseDbContext dbcontext = new StoredVehiclesDatabaseDbContext();
            List<Korisnik> korisnici = dbcontext.Korisnici.ToList();
            return View(korisnici);
        }
    }
}