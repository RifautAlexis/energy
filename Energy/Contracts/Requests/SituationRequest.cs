using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using popo.Contracts.DTOs;

namespace popo.Contracts.Requests
{
    public class SituationRequest
    {
        [FromBody]
        public int Load { get; set; }
        [FromBody]
        public FuelsDTO Fuels { get; set; }
        [FromBody]
        public List<PowerPlantDTO> PowerPlants { get; set; }
    }
}
