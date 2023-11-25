namespace ikem23_wapi.Models
{
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.Collections.Generic;

    public class ConceptMap
    {
        public string ResourceType { get; set; }
        public string Id { get; set; }
        
        public string Status = "active";
        public string Name { get; set; }
        public List<Group> Groups { get; set; }
    }

    public class Group
    {
        public List<Element> Elements { get; set; }
        public Unmapped Unmapped { get; set; }
    }

    public class Element
    {
        public string Code { get; set; }  //Mapovani CEHO (slopec v excelu)
        public List<Target> Targets { get; set; }
    }

    public class Target
    {
        public string Code { get; set; }   // Mapovani na co (jmeno FIHR atributu? )
        public string Equivalence = "equivalent";
    }

    public class Unmapped
    {
        public string Mode = "fixed";
        public string Code = "temp";
    }
}
