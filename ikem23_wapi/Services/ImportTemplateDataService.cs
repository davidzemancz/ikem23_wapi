using DocumentFormat.OpenXml.Wordprocessing;
using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using System.Net.Http;

namespace ikem23_wapi.Services
{
    public class ImportTemplateDataService
    {
        private readonly HttpClient _httpClient;

        public ImportTemplateDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<ImportTemplate>> Get()
        {
            string query = "";

            BundleDto<ConceptMap> bundle = await _httpClient.GetFromJsonAsync<BundleDto<ConceptMap>>(Globals.FHIRServerUri + "/ConceptMap" + query);

            List<ImportTemplate> templates = new();

            foreach (var cm in bundle.Entry)
            {
                templates.Add(MapConceptMapToImportTemplate(cm.Resource));
            }

            return templates;

        } 

        public async Task Post(ImportTemplate importTemplate)
        {
            ConceptMap cm = new();

            // TODO petr mapovani

            var response = await _httpClient.PostAsJsonAsync(Globals.FHIRServerUri + "/ConceptMap", cm);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
        }


        public ImportTemplate MapConceptMapToImportTemplate(ConceptMap cm)
        {
            ImportTemplate it = new ImportTemplate();
            it.Name = cm.Name;
            it.Id = cm.Id;

            List<ColumnDefinition> ColumnMapping = new List<ColumnDefinition>();
            Group group = cm.Groups[0];

            foreach (var e in group.Elements)
            {
                Target t = e.Targets[0];
                ColumnDefinition cd = new ColumnDefinition { PropertyName = e.Code.ToString(), ExcelColumnLetter = t.Code.ToString() };
            }

            return it;
        }

        public ConceptMap MapImportTemplateToConceptMap(ImportTemplate it)
        {
            ConceptMap cm = new ConceptMap();
            cm.Name = it.Name;
            cm.Id = it.Id;
            
            List<Group> groups = new List<Group>();
            cm.Groups = groups;
            List<Element> elements = new List<Element>();
            Group g = new Group();
            groups.Add(g);
            g.Elements = elements;

            foreach (var cd in it.ColumnMapping)
            {
                Element e = new Element();
                elements.Add(e);
                e.Code = cd.PropertyName;
                List<Target> targets = new List<Target>();
                Target t = new Target();
                targets.Add(t);
                e.Targets = targets;
                t.Code = cd.ExcelColumnLetter;
            }

            return cm;
        }
    }
}
