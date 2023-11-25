using ikem23_wapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientRecordDataService _dataService;
        private readonly HttpClient _httpClient;

        public PatientController(PatientRecordDataService dataService, HttpClient httpClient)
        {
            _dataService = dataService;
            _httpClient = httpClient;
        }
    }
}
