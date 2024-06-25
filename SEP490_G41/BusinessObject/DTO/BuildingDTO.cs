using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class BuildingDTO
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
    }
    public class BuildingAddDTO
    {
        public string BuildingName { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FacilityId { get; set; }
    }

    public class BuildingUpdateDTO
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FacilityId { get; set; }
    }
}
