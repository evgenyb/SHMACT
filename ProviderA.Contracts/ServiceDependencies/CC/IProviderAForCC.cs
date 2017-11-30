using System.ServiceModel;

namespace ProviderA.Contracts.ServiceDependencies.CC
{
    public interface IProviderAForCC
    {
        [OperationContract]
        D1 M1();

        [OperationContract]
        D2 M2();
    }
}