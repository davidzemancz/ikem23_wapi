using ikem23_wapi.Models;

namespace ikem23_wapi.Services
{
    public class PatientDataService
    {
        public static List<Patient> Data { get; set; }

        public async Task<List<Patient>> Get()
        {
            return Data;
        }
    }
}
