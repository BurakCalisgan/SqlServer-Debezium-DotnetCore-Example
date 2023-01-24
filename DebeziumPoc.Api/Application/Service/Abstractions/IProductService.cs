using DebeziumPoc.Api.Domain.Entities;
using DebeziumPoc.Api.Models;

namespace DebeziumPoc.Api.Application.Service.Abstractions
{
    public interface IProductService
    {
        bool Add(CdcModel request);

        IEnumerable<Product> GetAll();
    }
}
