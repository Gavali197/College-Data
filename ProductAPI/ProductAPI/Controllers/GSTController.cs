using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GSTController : ControllerBase
    {
        [HttpGet("CalculateGST")]
        public IActionResult CalculateGst(ProductCategory category, decimal rate)
        {
            decimal gst = category switch
            {
                ProductCategory.Electronic => 20,
                ProductCategory.footwear => 18,
                ProductCategory.clothing => 12,
                _ => 0
            };

            decimal totalGst = rate * gst / 100;
            decimal cgst = totalGst / 2;
            decimal sgst = totalGst / 2;

            return Ok(new
            {
                category = category.ToString(),
                rate,
                gstPercent = gst,
                cgst,
                sgst,
                totalGst
            });
        }
    }
    
}
