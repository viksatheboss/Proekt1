using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Layer;
using ViewModels.Layer;

namespace StoredVehicles.Controllers
{
    public class HomeController : Controller
    {
        
        readonly IMarkaNaVoziloService ms;
        
        

        public HomeController( IMarkaNaVoziloService ms)
        {
            
            this.ms = ms;
            
        }

        
       // public ActionResult Index()
       // {
       //     List<VoziloViewModel> vozila = this.vs.GetVehicles().ToList();
       //     return View(vozila);
       // }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult MarkaNaVozilo()
        {    
            List<MarkaNaVoziloViewModel> vozila = this.ms.GetModels();
            return View(vozila);
        }

    }
}