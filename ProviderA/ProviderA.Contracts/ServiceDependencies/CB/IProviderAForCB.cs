using System.ServiceModel;

namespace ProviderA.Contracts.ServiceDependencies.CB
{
    [ServiceContract(Name = "IProviderA", Namespace = Constants.OperationsNamespace)]
    public interface IProviderAForCB
    {
        [OperationContract]
        D2 M2();
    }
}