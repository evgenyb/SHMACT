using ServiceD.Contracts;
using ServiceD.Contracts.ServiceDependencies.ServiceA;
using ServiceD.Contracts.ServiceDependencies.ServiceB;

namespace ServiceD.SF
{
    public class ServiceD : IServiceD, IServiceDForServiceA, IServiceDForServiceB
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