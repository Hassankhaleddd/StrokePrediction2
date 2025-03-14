﻿using Microsoft.AspNetCore.Authorization;

namespace StrokePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StrokePredictionController : ControllerBase
    {
        [HttpPost]
        public IActionResult PredictStroke([FromBody] StrokeInput Requst,
           [FromServices] IValidator<StrokeInput> validator)
        {

            var validationresult = validator.Validate(Requst);
            if (!validationresult.IsValid)
            {
                var modelstate = new ModelStateDictionary();

                foreach (var error in validationresult.Errors)
                {
                    modelstate.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelstate);
            }
            
            return Ok();
        }
    }
}
