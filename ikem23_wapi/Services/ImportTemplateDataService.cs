using ikem23_wapi.Models;

namespace ikem23_wapi.Services
{
    public class ImportTemplateDataService
    {
        public static List<ImportTemplate> Data { get; set; }

        public async Task<List<ImportTemplate>> Get()
        {
            return Data;
        } 

        public async Task Post(ImportTemplate importTemplate)
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
