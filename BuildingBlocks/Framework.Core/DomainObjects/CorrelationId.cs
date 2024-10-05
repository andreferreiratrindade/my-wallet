using MongoDB.Driver.Core.Operations;
using Newtonsoft.Json;

namespace Framework.Core.DomainObjects
{
    public class CorrelationId{
        public Guid Value {get;}

        [JsonConstructor]
        public CorrelationId(Guid value){
            Value = value;
        }
        public static CorrelationId Create(){
            return new CorrelationId(Guid.NewGuid());
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
