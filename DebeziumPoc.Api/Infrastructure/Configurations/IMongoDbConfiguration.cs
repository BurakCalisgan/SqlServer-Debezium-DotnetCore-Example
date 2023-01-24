namespace DebeziumPoc.Api.Infrastructure.Configurations
{
    public interface IMongoDbConfiguration
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
