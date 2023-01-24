namespace DebeziumPoc.Api.Infrastructure.Configurations
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
