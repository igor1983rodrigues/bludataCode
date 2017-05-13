using bludata.Models.Dao;
using bludata.Models.IDao;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Bludata
{
    internal static class StartupExtensao
    {
        public static void Configurar(this Container container)
        {
            container.Register<IPessoaDao, PessoaDao>();
            container.Register<IUfDao, UfDao>();
            container.Register<IClienteDao, ClienteDao>();
            container.Register<ITipoTelefoneDao, TipoTelefoneDao>();
            container.Register<IRgDao, RgDao>();
            container.Register<ITelefoneDao, TelefoneDao>();
        }
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var container = new Container();

            container.Configurar();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjector.Integration.WebApi.SimpleInjectorWebApiDependencyResolver(container)
            };

            //-- Habilita Cores
            app.UseCors(CorsOptions.AllowAll);

            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}
