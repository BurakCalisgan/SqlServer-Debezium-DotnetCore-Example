using DebeziumPoc.Api.Domain.Entities;
using DebeziumPoc.Api.Infrastructure.Attiributes;
using MongoDBExample.Entities.BusinessEntities.Base;

namespace DebeziumPoc.Api.Domain.Entities
{
    [BsonCollection("product_change")]
    public class Product : BaseEntity
    {
        public string Op { get; set; }
        public Before Before { get; set; }
        public After After { get; set; }

    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string StockCode { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
    }
    public class Before : ProductInfo
    {

    }
    public class After : ProductInfo
    {

    }

}
