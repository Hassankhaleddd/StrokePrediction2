using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text;

namespace StrokePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StrokePredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _endPoint;
        public StrokePredictionController(ApplicationDbContext context)
        {
            _httpClient = new HttpClient();
            _endPoint = $"https://0e728509-deda-4405-a493-dd6ed2649073-00-2ypwghvs04nkw.kirk.replit.dev/predict";
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PredictStrokeAsync([FromBody] StrokeInput request,
           [FromServices] IValidator<StrokeInput> validator)
        {

            var validationresult = validator.Validate(request);
            if (!validationresult.IsValid)
            {
                var modelstate = new ModelStateDictionary();

                foreach (var error in validationresult.Errors)
                {
                    modelstate.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelstate);
            }
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var features = new object[]
            {
                0, 
                request.Gender,
                request.Age,
                request.Hypertension,
                request.HasHeartDisease,
                request.EverMarried ? "Yes" : "No",
                request.WorkType,
                request.Residence_type,
                request.avg_glucose_level,
                request.Bmi,
                request.smoking_status
            };
            //requestbody ديه هناخده نعملها سيرياليزيشن نحوله يعنى لjson
            var requestBody = new
            {
                features = features
            };
            // convert it to json
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //here post el req and get the res
            try
            {
                var response = await _httpClient.PostAsync(_endPoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, responseString);
                var result = JsonSerializer.Deserialize<Dictionary<string, int[]>>(responseString);
                int prediction = result["prediction"][0];
                var labtest = new LabTest
                {
                    UserId = userId,
                    Gender = request.Gender,
                    Age = request.Age,
                    Hypertension = request.Hypertension,
                    HasHeartDisease = request.HasHeartDisease,
                    EverMarried = request.EverMarried,
                    WorkType = request.WorkType,
                    Residence_type = request.Residence_type,
                    avg_glucose_level = request.avg_glucose_level,
                    Bmi = request.Bmi,
                    smoking_status = request.smoking_status,
                    StrokeResult = prediction,
                    Date = DateTime.Now
                };
                _context.LabTests.Add(labtest);
                await _context.SaveChangesAsync();
                return Ok(responseString);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
