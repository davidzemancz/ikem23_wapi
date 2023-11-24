using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportTemplateController : ControllerBase
    {
        // GET: api/<ImportTemplateController>
        [HttpGet]
        public IEnumerable<ImportTemplate> Get()
        {
            return new ImportTemplate[] { "value1", "value2" };
        }

        // GET api/<ImportTemplateController>/5
        [HttpGet("{id}")]
        public ImportTemplate Get(int id)
        {
            return "value";
        }

        // POST api/<ImportTemplateController>
        [HttpPost]
        public void Post([FromBody] ImportTemplate value)
        {
        }

        // PUT api/<ImportTemplateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ImportTemplate value)
        {
        }

        // DELETE api/<ImportTemplateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
