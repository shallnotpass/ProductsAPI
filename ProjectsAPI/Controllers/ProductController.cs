using ProductsAPI.Logic.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpPost("Add")]
        public async Task<ActionResult> Add(ulong nomenclature)
        {
            if (nomenclature.ToString().Count() == 10)
            {
                await productService.Add(nomenclature);
                return Ok();
            }
            else
                return BadRequest();

        }
    }
}
