using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ProviderA.Contracts;

namespace ProviderA
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var container = new WindsorContainer();

            container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero)
                .Register(Component.For<IProviderA>()
                    .ImplementedBy<ProviderA>()
                    .AsWcfService(new DefaultServiceModel().Hosted()
                    .AddEndpoints(WcfEndpoint.ForContract<IProviderA>().BoundTo(new BasicHttpBinding()))));
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