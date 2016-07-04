using Microsoft.AspNet.Mvc;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadProperty(string id)
        {
            var propertyModel = await this.propertyService.ReadPropertyAsync(id);
            return Ok(propertyModel);
        }
    }
}
