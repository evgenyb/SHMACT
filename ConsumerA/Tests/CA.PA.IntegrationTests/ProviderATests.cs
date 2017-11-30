using System;
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
    public class ProviderATests
    {
        IWindsorContainer _container;
        private IProviderAForCA _providera;
        [OneTimeSetUp]
        public void Init()
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            _container.Register(Component.For<IProviderAForCA>()
                .AsWcfClient(WcfEndpoint
                    .BoundTo(new BasicHttpBinding())
                    .At("http://localhost/ProviderA/ProviderA.svc")));

            _providera = _container.Resolve<IProviderAForCA>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _container.Dispose();
        }

        [Test]
        public void Test1()
        {
            var response = _providera.M1();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<D1>());
        }
    }
}