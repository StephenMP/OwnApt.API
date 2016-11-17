using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Domain.Service;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CacheController : ApiController
    {
        #region Public Constructors

        public CacheController(IMemoryCacheService cache) : base(cache)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost("invalidate")]
        public async Task<IActionResult> Invalidate()
        {
            await Task.Factory.StartNew(() => this.InvalidateCache());
            return Ok();
        }

        #endregion Public Methods
    }
}
