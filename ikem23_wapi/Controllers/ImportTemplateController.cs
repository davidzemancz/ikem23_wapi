using ikem23_wapi.Models;
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
        public async Task<IEnumerable<ImportTemplate>> Get()
        {
            return await _dataService.Get();
        }

        [HttpPost]
        public async Task Post([FromBody] ImportTemplate value)
        {
            await _dataService.Post(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _dataService.Delete(id);
        }

        [HttpGet("columns")]
        public async Task<List<ColumnDefinition>> GetColumnsDefinition()
        {
            var list = new List<ColumnDefinition>();

            var pr = new PatientRecord();
            foreach(var prop in pr.GetType().GetProperties())
            {
                list.Add(new ColumnDefinition(prop.Name, "", ""));
            }

            return list;
        }
    }
}
