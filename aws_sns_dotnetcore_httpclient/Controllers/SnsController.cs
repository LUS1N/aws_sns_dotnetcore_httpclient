using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aws_sns_dotnetcore_httpclient.Controllers
{
    [Route("/[controller]")]
    public class SnsController : Controller
    {
        // POST /[controller]
        [HttpPost]
        public string Post()
        {
            return "hi";
        }
    }
}
