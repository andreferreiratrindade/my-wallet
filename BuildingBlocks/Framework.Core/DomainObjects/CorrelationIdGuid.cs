using MongoDB.Driver.Core.Operations;

namespace Framework.Core.DomainObjects
{
    public class CorrelationIdGuid{
        public Guid Id {get;}
        private CorrelationIdGuid(Guid id){
            Id = id;
        }
        public static CorrelationIdGuid Create(){
            return new CorrelationIdGuid(Guid.NewGuid());
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
