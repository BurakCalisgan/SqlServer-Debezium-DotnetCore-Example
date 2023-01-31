using DebeziumPoc.Api.Application.Service.Abstractions;
using DebeziumPoc.Api.Domain.Entities;
using DebeziumPoc.Api.Domain.Repositories;
using DebeziumPoc.Api.Models;

namespace DebeziumPoc.Api.Application.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IServiceProvider _collection;
        public ProductService(IServiceProvider collection)
        {
            _collection = collection;
        }
        public bool Add(CdcModel request)
        {
            Product entity = new Product();

            if (request != null && request.Payload != null)
            {
                entity.CreateDate = DateTime.Now;
                entity.Op = request.Payload.op;
                if (request.Payload.Before != null)
                {
                    entity.Before = new Before
                    {
                        ProductId = request.Payload.Before.Id,
                        Name = request.Payload.Before.Name,
                        StockCode = request.Payload.Before.StockCode,
                        Price = request.Payload.Before.Price,
                        StockQuantity = request.Payload.Before.StockQuantity
                    };
                }

                if (request.Payload.After != null)
                {
                    entity.After = new After
                    {
                        ProductId = request.Payload.After.Id,
                        Name = request.Payload.After.Name,
                        StockCode = request.Payload.After.StockCode,
                        Price = request.Payload.After.Price,
                        StockQuantity = request.Payload.After.StockQuantity
                    };
                }

            }
            var product = _collection.GetService<IProductRepository>().Add(entity);

            return product != null ? true : false;
        }

        public IEnumerable<Product> GetAll()
        {
            return _collection.GetService<IProductRepository>().GetAll();
        }
    }
}
