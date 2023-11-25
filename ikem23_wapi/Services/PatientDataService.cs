using ikem23_wapi.DTOs;
using ikem23_wapi.Models;

namespace ikem23_wapi.Services
{
    public class PatientDataService
    {
        private readonly HttpClient _httpClient;


        public PatientDataService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
            
        }

        public async Task<List<Patient>> Get(string name)
        {
            string query = "";
            if (!string.IsNullOrEmpty(name)) query += $"?name={name}";
            
            BundleDto<Patient> bundle =  await _httpClient.GetFromJsonAsync<BundleDto<Patient>>(Globals.FHIRServerUri + "/Patient" + query);
            return bundle.Entry.Select(e => e.Resource).ToList();
        }
    }
}
