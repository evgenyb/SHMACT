using System.Configuration;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using ProviderA.Contracts;
using ProviderA.Contracts.ServiceDependencies.CB;

namespace CB.PA.IntegrationTests
{
    [TestFixture]
    public class ProviderAIntegrationTests
    {
        IWindsorContainer _container;
        private IProviderAForCB _providerb;
        [OneTimeSetUp]
        public void Init()
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            var httpBaseUrl = ConfigurationManager.AppSettings["Global.WcfServices.HttpBaseUrl"];
            _container.Register(Component.For<IProviderAForCB>()
                .AsWcfClient(WcfEndpoint
                    .BoundTo(new BasicHttpBinding())
                    .At(httpBaseUrl + "/ProviderA/ProviderA.svc")));

            _providerb = _container.Resolve<IProviderAForCB>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _container.Dispose();
        }

        [Test]
        public void IProviderAForCB_M1()
        {
            var response = _providerb.M2();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<D2>());
        }
    }
}