using System.ServiceModel;

namespace ProviderA.Contracts.ServiceDependencies.CB
{
    public interface IProviderAForCB
    {
        [OperationContract]
        D2 M2();
    }
}