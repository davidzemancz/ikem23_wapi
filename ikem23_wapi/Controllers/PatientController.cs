using ikem23_wapi.Models;
using ikem23_wapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientDataService _dataService;
        private readonly HttpClient _httpClient;

        public PatientController(PatientDataService dataService, HttpClient httpClient)
        {
            _dataService = dataService;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<FHIRDataModels>> Get([FromQuery]string name)
        {
            return await _dataService.Get(name);
        }
    }
}
