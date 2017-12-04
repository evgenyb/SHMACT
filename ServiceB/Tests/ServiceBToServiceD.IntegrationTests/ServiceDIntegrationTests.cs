using System.Configuration;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using ProviderA.Contracts;
using ProviderA.Contracts.ServiceDependencies.CB;

namespace ServiceBToServiceD.IntegrationTests
{
    [TestFixture]
    public class ServiceDIntegrationTests
    {
        IWindsorContainer _container;
        private IProviderAForCB _serviceD;
        [OneTimeSetUp]
        public void Init()
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            var httpBaseUrl = ConfigurationManager.AppSettings["Global.WcfServices.HttpBaseUrl"];
            _container.Register(Component.For<IProviderAForCB>()
                .AsWcfClient(WcfEndpoint
                    .BoundTo(new BasicHttpBinding())
                    .At(httpBaseUrl + "/ServiceD.SF/ServiceD.svc")));

            _serviceD = _container.Resolve<IProviderAForCB>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _container.Dispose();
        }

        [Test]
        public void IProviderAForCB_M1()
        {
            var response = _serviceD.M2();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<D2>());
        }
    }
}