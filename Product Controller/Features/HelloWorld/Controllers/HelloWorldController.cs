using Microsoft.AspNetCore.Mvc;

namespace Product_Controller.Features.HelloWorld.Controllers
{
    [Route("hello")]
    
    public class HelloWorldController : Controller
    {
        [HttpGet("")]
        
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("welcome/{name?}/{numTimes:int?}")]
        
        public IActionResult Welcome(string name, int  numTimes = 1)
        {

            ViewData["Message"] = "hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
