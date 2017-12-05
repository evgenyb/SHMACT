using System.ServiceModel;

namespace ServiceA.Contracts.ServiceDependencies.ServiceB
{
    [ServiceContract(Name = "IServiceA", Namespace = Constants.OperationsNamespace)]
    public interface IServiceDForServiceA
    {
        [OperationContract]
        Foo Foo();
    }
}