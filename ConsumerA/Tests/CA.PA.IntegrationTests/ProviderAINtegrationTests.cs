using System;
using System.Configuration;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using ProviderA.Contracts;
using ProviderA.Contracts.ServiceDependencies.CA;

namespace CA.PA.IntegrationTests
{
    [TestFixture]
    public class ProviderAIntegrationTests
    {
        IWindsorContainer _container;
        private IProviderAForCA _providera;
        [OneTimeSetUp]
        public void Init()
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            var httpBaseUrl = ConfigurationManager.AppSettings["Global.WcfServices.HttpBaseUrl"];
            _container.Register(Component.For<IProviderAForCA>()
                .AsWcfClient(WcfEndpoint
                    .BoundTo(new BasicHttpBinding())
                    .At(httpBaseUrl + "/ProviderA/ProviderA.svc")));

            _providera = _container.Resolve<IProviderAForCA>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _container.Dispose();
        }

        [Test]
        public void IProviderAForCA_M1()
        {
            var response = _providera.M1();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<D1>());
        }
    }
}