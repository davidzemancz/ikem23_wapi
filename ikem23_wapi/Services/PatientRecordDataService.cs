using ikem23_wapi.Models;

namespace ikem23_wapi.Services
{
    public class PatientRecordDataService
    {
        public static List<PatientRecord> Data { get; set; }

        public async Task<List<PatientRecord>> Get()
        {
            return Data;
        }

        public async Task Post(PatientRecord importTemplate)
        {
            int index = Data.FindIndex(0, c => c.Id == importTemplate.Id);
            if (index == -1)
            {
                Data.Add(importTemplate);
            }
            else
            {
                Data[index] = importTemplate;
            }
        }

        public async Task Delete(int id)
        {
            Data.RemoveAll(c => c.Id == id);
        }
    }
}
