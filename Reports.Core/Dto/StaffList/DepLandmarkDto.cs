using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class DepLandmarkDto
    {
        public int Id { get; set; }
        public int LandmarkId { get; set; }
        public string LandmarkName { get; set; }
        public string Description { get; set; }
    }
}
