using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data.Models; 
using SurveyPortal.Shared;      
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SurveyPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveysApiController : ControllerBase
    {
        private readonly ISurveyRepository _repository;

        public SurveysApiController(ISurveyRepository repository)
        {
            _repository = repository;
        }

        // GET: /api/SurveysApi
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
            return await _repository.Surveys.ToListAsync();
        }

        // GET: /api/SurveysApi/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Survey>> GetSurvey(long id)
        {
            var survey = await _repository.Surveys.FirstOrDefaultAsync(s => s.SurveyID == id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // POST: /api/SurveysApi
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Survey> PostSurvey(Survey survey)
        {
            _repository.SaveSurvey(survey);
            return CreatedAtAction(nameof(GetSurvey), new { id = survey.SurveyID }, survey);
        }

        // PUT: /api/SurveysApi/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutSurvey(long id, Survey survey)
        {
            if (id != survey.SurveyID)
            {
                return BadRequest();
            }

            _repository.SaveSurvey(survey);
            return NoContent();
        }

        // DELETE: /api/SurveysApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSurvey(long id)
        {
            var deletedSurvey = _repository.DeleteSurvey(id);
            if (deletedSurvey == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}