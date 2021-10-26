using Microsoft.AspNetCore.Mvc;
using Energy.Contracts.Requests;
using Energy.Handlers;

namespace Energy.Controllers
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
