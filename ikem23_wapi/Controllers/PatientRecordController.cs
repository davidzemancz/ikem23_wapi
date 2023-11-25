using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using ikem23_wapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRecordController : ControllerBase
    {
        private readonly PatientRecordDataService _dataService;
        private readonly ImportTemplateDataService _importDataService;
        private readonly HttpClient _httpClient;

        public PatientRecordController(PatientRecordDataService dataService, HttpClient httpClient, ImportTemplateDataService importDataService)
        {
            _dataService = dataService;
            _importDataService = importDataService;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<PatientRecord>> Get()
        {
            return await _dataService.Get();
        }

        [HttpPost("create")]
        public async Task Create(IFormCollection data)
        {
            var dto = new PatientRecordCreateDto();

            dto.PacientId = int.Parse(data["pacientId"].First());
            dto.KodPojistovna = int.Parse(data["kodPojistovna"].First());
            dto.Diagnoza = data["diagnoza"].First();
            dto.OnkologickyKod = data["onkologickyKod"].First();
            dto.PomerNadorovychBunek = double.Parse(data["pomerNadorovychBunek"].First());
            dto.IdBiopsie = data["idBiopsie"].First();
            dto.PrijemLMP = DateTime.Parse(data["prijemLMP"].First(), CultureInfo.InvariantCulture);
            dto.UzavreniLMP = DateTime.Parse(data["prijemLMP"].First(), CultureInfo.InvariantCulture);

            int i = 0;
            foreach (string item in data.Keys.Where(k => k.StartsWith("file")))
            {
                int templateId = 8464; //int.Parse(data[item]);

                var template = await _importDataService.Get(templateId);

                dto.Files.Add(new PatientRecordFileDto()
                {
                    File = data.Files[i].OpenReadStream(),
                    Template = template
                });
                i++;
            }

            await _dataService.Post(dto);

        }


    }
}
