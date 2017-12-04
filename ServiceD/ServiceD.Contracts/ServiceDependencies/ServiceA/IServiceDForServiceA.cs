using System.ServiceModel;

namespace ServiceD.Contracts.ServiceDependencies.ServiceA
{
    [ServiceContract(Name = "IServiceD", Namespace = Constants.OperationsNamespace)]
    public interface IServiceDForServiceA
    {
        [OperationContract]
        D1 M1();
    }
}