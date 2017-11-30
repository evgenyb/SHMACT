using System.Runtime.InteropServices;
using ProviderA.Contracts;
using ProviderA.Contracts.ServiceDependencies.CA;
using ProviderA.Contracts.ServiceDependencies.CB;
using ProviderA.Contracts.ServiceDependencies.CC;

namespace ProviderA
{
    public class ProviderA : IProviderA, IProviderAForCA, IProviderAForCB, IProviderAForCC
    {
        public D1 M1()
        {
            return new D1();
        }

        public D2 M2()
        {
            return new D2();
        }

        public D3 M3()
        {
            return new D3();
        }
    }
}