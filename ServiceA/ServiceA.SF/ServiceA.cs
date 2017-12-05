using ServiceA.Contracts;
using ServiceA.Contracts.ServiceDependencies.ServiceB;

namespace ServiceA.SF
{
    public class ServiceA : IServiceA, IServiceAForServiceB
    {
        public Foo Foo()
        {
            return new Foo();
        }

        public Bar Bar()
        {
            return new Bar();
        }
    }
}
