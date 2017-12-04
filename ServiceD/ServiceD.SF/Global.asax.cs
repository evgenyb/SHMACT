using System;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ServiceD.Contracts;

namespace ServiceD.SF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var container = new WindsorContainer();

            container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero)
                .Register(Component.For<IServiceD>()
                    .ImplementedBy<ServiceD>()
                    .AsWcfService(new DefaultServiceModel().Hosted()
                    .AddEndpoints(WcfEndpoint.ForContract<IServiceD>().BoundTo(new BasicHttpBinding()))));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}