using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public Task Post()
        {
            throw new Exception("123");
        }
        
        [HttpPost("v1")]
        public Task PostV1([FromBody] object o)
        {
            throw new Exception("123");
        }
    }
}