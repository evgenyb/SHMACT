using System.ServiceModel;

namespace ProviderA.Contracts.ServiceDependencies.CA
{
    [ServiceContract(Name = "IProviderA", Namespace = Constants.OperationsNamespace)]
    public interface IProviderAForCA
    {
        [OperationContract]
        D1 M1();
    }
}