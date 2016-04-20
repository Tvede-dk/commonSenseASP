using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using commonSenseASP.security;

namespace commonSenseASP.baseClasses {
    [HttpsRemote]
    public  class BaseController : Controller {
        public readonly ILogger<BaseController> Logger;
        public BaseController(ILogger<BaseController> logger) {
            this.Logger = logger;
        }


    }
}
