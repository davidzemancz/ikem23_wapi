using ikem23_wapi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportTemplateController : ControllerBase
    {
        private readonly ImportTemplateDataService _dataService;
        public ImportTemplateController(ImportTemplateDataService dataService) 
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IEnumerable<ImportTemplate> Get()
        {
            return new ImportTemplate[] { };
        }

        [HttpPost]
        public void Post([FromBody] ImportTemplate value)
        {
        }

        
    }
}
