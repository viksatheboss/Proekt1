using Service.Layer;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.WebApi;

namespace StoredVehicles
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IVozilaService, VozilaService>();
            container.RegisterType<IKorisniciService, KorisniciService>();
            container.RegisterType<IMarkaNaVoziloService, MarkaNaVoziloService>();
            
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}