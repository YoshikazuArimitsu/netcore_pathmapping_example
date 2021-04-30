using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathMappingExp.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DebugController : ControllerBase {
        readonly IConfiguration Config;

        public DebugController(IConfiguration configuration) {
            Config = configuration;
        }

        [HttpGet]
        public string Get() {
            return Config["Value"];
        }
    }
}
