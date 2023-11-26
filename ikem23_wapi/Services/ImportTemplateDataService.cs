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

        public async Task<List<FhirImportTemplate>> Get()
        {
            string query = "";

            BundleDto<ConceptMap> bundle = await _httpClient.GetFromJsonAsync<BundleDto<ConceptMap>>(Globals.FHIRServerUri + "/ConceptMap" + query);

            List<FhirImportTemplate> templates = new();

            foreach (var cm in bundle.Entry)
            {
                templates.Add(MapConceptMapToImportTemplate(cm.Resource));
            }

            return templates;
        }

        public async Task<FhirImportTemplate> Get(int id)
        {
            ConceptMap cm  = await _httpClient.GetFromJsonAsync<ConceptMap>(Globals.FHIRServerUri + $"/ConceptMap/{id}");
            return MapConceptMapToImportTemplate(cm);
        }

        public async Task Post(FhirImportTemplate importTemplate)
        {
            ConceptMap cm = MapImportTemplateToConceptMap(importTemplate);

            var response = await _httpClient.PostAsJsonAsync(Globals.FHIRServerUri + "/ConceptMap", cm);
            string err = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync(Globals.FHIRServerUri + $"/ConceptMap/{id}");
        }

        public ConceptMap MapImportTemplateToConceptMap(FhirImportTemplate it)
        {
            ConceptMap cm = new ConceptMap();
            cm.Name = it.Name;
            cm.Id = it.Id.ToString();

            List<Group> groups = new List<Group>();
            cm.Group = groups;
            List<Element> elements = new List<Element>();
            Group g = new Group();
            groups.Add(g);
            g.Element = elements;

            foreach (var cd in it.ColumnMapping)
            {
                Element e = new Element();
                elements.Add(e);
                e.Code = cd.Id;
                
                List<Target> targets = new List<Target>();
                Target t = new Target();
                targets.Add(t);
                e.Target = targets;
                t.Code = cd.ExcelColumnLetter;
            }

            return cm;
        }


        public FhirImportTemplate MapConceptMapToImportTemplate(ConceptMap cm)
        {
            FhirImportTemplate it = new FhirImportTemplate();
            it.Name = cm.Name;
            it.Id = int.Parse(cm.Id);

            List<ColumnDefinition> ColumnMapping = new List<ColumnDefinition>();
            it.ColumnMapping = ColumnMapping;
            Group group = cm.Group?.FirstOrDefault();


            if (group != null)
            {
                foreach (var e in group.Element)
                {
                    Target t = e.Target[0];
                    ColumnDefinition cd = new ColumnDefinition { Id = e.Code.ToString(), ExcelColumnLetter = t.Code.ToString() };
                    ColumnMapping.Add(cd);
                }
            }

            return it;
        }
    }
}
