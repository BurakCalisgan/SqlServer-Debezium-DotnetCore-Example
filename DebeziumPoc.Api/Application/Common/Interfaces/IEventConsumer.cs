using Confluent.Kafka;

namespace DebeziumPoc.Api.Application.Common.Interfaces
{
    public interface IEventConsumer
    {
        ConsumeResult<string, string> ReadMessage(CancellationToken token);
    }
}
