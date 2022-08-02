using ProductsAPI.Logic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductctController : Controller
    {
        private readonly IProductService productService;

        public ProductctController(IProductService productService)
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
