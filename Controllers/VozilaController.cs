using DomainModels.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoredVehicles.Controllers
{
    public class VozilaController : Controller
    {
        [HttpGet]
        [Route("api/vozila")]
        public ActionResult GetVehicles()
        {
            StoredVehiclesDatabaseDbContext dbcontext = new StoredVehiclesDatabaseDbContext();
            List<Vozilo> vozila = dbcontext.Vozila.ToList();
            return Json(vozila, JsonRequestBehavior.AllowGet);
        }

        
    }
}
