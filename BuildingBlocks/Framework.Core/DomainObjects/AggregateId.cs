public abstract class AggregateId {
     public Guid Id {get;}
     protected AggregateId (){}
        public AggregateId(Guid id){
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
}
