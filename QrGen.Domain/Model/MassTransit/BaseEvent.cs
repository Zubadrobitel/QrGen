namespace QrGen.Domain.Model.MassTransit
{
    public class BaseEvent<T> : BaseModel<Guid>
    {
        T? Data { get; set; }
        public string Source { get; set; } = "QrGen";
        public int? Method { get; set; }
        public BaseEvent(){}
    }
}
