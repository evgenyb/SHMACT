using System.ServiceModel;

namespace ServiceD.Contracts.ServiceDependencies.ServiceB
{
    [ServiceContract(Name = "IServiceD", Namespace = Constants.OperationsNamespace)]
    public interface IServiceDForServiceB
    {
        [OperationContract]
        D2 M2();
    }
}