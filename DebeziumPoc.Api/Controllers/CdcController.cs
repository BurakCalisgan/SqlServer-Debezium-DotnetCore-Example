using DebeziumPoc.Api.Application.Service.Abstractions;
using DebeziumPoc.Api.Domain.Entities;
using DebeziumPoc.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DebeziumPoc.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdcController : ControllerBase
    {
        private readonly IServiceProvider _collection;
        private readonly ILogger<CdcController> _logger;

        public CdcController(ILogger<CdcController> logger, IServiceProvider collection)
        {
            _logger = logger;
            _collection = collection;
        }

        [HttpGet(Name = "GetCdcList")]
        public IEnumerable<Product> GetAll()
        {
            return _collection.GetService<IProductService>().GetAll();
        }
    }
}