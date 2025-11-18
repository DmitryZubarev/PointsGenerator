using Domain.Interfaces.Common;


namespace Domain.Interfaces.Kafka
{
    public interface IBaseProducer<in TKey, in TMessage> : IDisposable
        where TMessage : IMessage
    {
        Task ProduceAsync(TMessage message);
    }
}
