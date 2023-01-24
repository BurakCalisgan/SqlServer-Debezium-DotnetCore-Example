using DebeziumPoc.Api.Domain.Entities;
using DebeziumPoc.Api.Domain.Repositories;
using DebeziumPoc.Api.Infrastructure.Context;

namespace DebeziumPoc.Api.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
