using System.Runtime.Serialization;
using System.ServiceModel;

namespace ServiceA.Contracts
{
    [ServiceContract(Name = "IServiceA", Namespace = Constants.OperationsNamespace)]
    public interface IServiceA
    {

        [OperationContract]
        Foo Foo();

        [OperationContract]
        Bar Bar();

    }

    [DataContract]
    public class Foo
    {
        [DataMember]
        public string P11 { get; set; }
        [DataMember]
        public string P12 { get; set; }
        [DataMember]
        public int P13 { get; set; }
    }

    [DataContract]
    public class Bar
    {
        [DataMember]
        public string P21 { get; set; }
        [DataMember]
        public string P22 { get; set; }
        [DataMember]
        public int P23 { get; set; }
    }

}
