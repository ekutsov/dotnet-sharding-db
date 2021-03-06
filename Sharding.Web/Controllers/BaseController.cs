using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Web.Controllers
{
    [Route("api/v1")]
    public class BaseController: ControllerBase
    {
        private readonly IOptions<List<ShardSettings>> _shardsSettings;
        public BaseController(IOptions<List<ShardSettings>> shardsSettings) {
            _shardsSettings = shardsSettings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_shardsSettings);
        }
    }
}