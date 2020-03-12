using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Layer;
using ViewModels.Layer;

namespace StoredVehicles.ApiControllers
{
    public class AccountController : ApiController
    {
        readonly IKorisniciService ks;

        public AccountController(IKorisniciService ks)
        {
            this.ks = ks;
        }

        public string Get(string Email)
        {
           if( this.ks.GetUsersByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found;";
            }
        }
    }
}
