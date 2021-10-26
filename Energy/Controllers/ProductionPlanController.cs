using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using popo.Contracts.Requests;
using popo.Contracts.Responses;
using popo.Handlers;

namespace popo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult ProductionPlan(SituationRequest request, [FromServices] IHandler<SituationRequest, ObjectResult> handler)
        {
            return handler.Handle(request);
        }
    }
}
