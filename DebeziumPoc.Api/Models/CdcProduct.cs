namespace DebeziumPoc.Api.Models
{
    public class CdcProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockCode { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
