using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Domain.Service;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CacheController : ApiController
    {
        public CacheController(IMemoryCacheService cache) : base(cache)
        {
        }

        [HttpPost("invalidate")]
        public async Task<IActionResult> Invalidate()
        {
            await Task.Factory.StartNew(() => this.InvalidateCache());
            return Ok();
        }
    }
}
