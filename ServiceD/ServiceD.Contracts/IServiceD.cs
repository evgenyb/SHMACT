using System.Runtime.Serialization;
using System.ServiceModel;

namespace ServiceD.Contracts
{
    [ServiceContract(Name = "IServiceD", Namespace = Constants.OperationsNamespace)]
    public interface IServiceD
    {

        [OperationContract]
        D1 M1();

        [OperationContract]
        D2 M2();

        [OperationContract]
        D3 M3();
    }

    [DataContract]
    public class D1
    {
        [DataMember]
        public string P11 { get; set; }
        [DataMember]
        public string P12 { get; set; }
        [DataMember]
        public int P13 { get; set; }
    }

    [DataContract]
    public class D2
    {
        [DataMember]
        public string P21 { get; set; }
        [DataMember]
        public bool P22 { get; set; }
        [DataMember]
        public int P23 { get; set; }
    }

    [DataContract]
    public class D3
    {
        [DataMember]
        public string P31 { get; set; }
        [DataMember]
        public bool P32 { get; set; }
        [DataMember]
        public int P33 { get; set; }
    }
}
