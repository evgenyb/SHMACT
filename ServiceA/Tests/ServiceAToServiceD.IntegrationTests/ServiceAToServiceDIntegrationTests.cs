using System.Configuration;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using ServiceD.Contracts;
using ServiceD.Contracts.ServiceDependencies.ServiceA;

namespace ServiceAToServiceD.IntegrationTests
{
    [TestFixture]
    public class ServiceAToServiceDIntegrationTests
    {
        IWindsorContainer _container;
        private IServiceDForServiceA _serviceD;
        [OneTimeSetUp]
        public void Init()
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            var httpBaseUrl = ConfigurationManager.AppSettings["Global.WcfServices.HttpBaseUrl"];
            _container.Register(Component.For<IServiceDForServiceA>()
                .AsWcfClient(WcfEndpoint
                    .BoundTo(new BasicHttpBinding())
                    .At(httpBaseUrl + "/ServiceD.SF/ServiceD.svc")));

            _serviceD = _container.Resolve<IServiceDForServiceA>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _container.Dispose();
        }

        [Test]
        public void IServiceDForServiceA_M1()
        {
            var response = _serviceD.M1();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<D1>());
        }
    }
}