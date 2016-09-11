using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api
{
    public class MapOwnerToPropertiesDto
    {
        public string OwnerId { get; set; }
        public string[] PropertyIds { get; set; }
    }
}
