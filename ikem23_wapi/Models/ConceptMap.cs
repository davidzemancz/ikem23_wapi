namespace ikem23_wapi.Models
{
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.Collections.Generic;

    public class ConceptMap
    {
        public string ResourceType { get; set; } = nameof(ConceptMap);
        public string Id { get; set; }
        public string Status { get; set; } = "active";
        public string Name { get; set; }
        public List<Group> Group { get; set; }
    }

    public class Group
    {
        public List<Element> Element { get; set; } = new();
    }

    public class Element
    {
        public string Code { get; set; }  //Mapovani CEHO (slopec v excelu)
        public List<Target> Target { get; set; }
    }

    public class Target
    {
        public string Code { get; set; }   // Mapovani na co (jmeno FIHR atributu? )
        public string Equivalence { get; set; } = "equivalent";
    }
}
