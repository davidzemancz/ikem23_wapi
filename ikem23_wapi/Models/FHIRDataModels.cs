using DocumentFormat.OpenXml.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace ikem23_wapi.Models
{
    public class FHIRDataModels
    {
        public int Id { get; set; }
        public List<PatientName> Name { get; set; }
    }

    public class PatientName
    {
        public string Family { get; set; }
        public List<string> Given { get; set; }
    }

    public class Coverage
    {
        Identifier identifier { get; set; } = new Identifier();
    }

    public class Condition
    {
        public int Id { get; set; }

        public Code code { get; set; } = new Code();
    }

    public class DiagnosticReport
    {
        public string ResourceType { get; set; } = nameof(DiagnosticReport);
        public List<ObjReference> Observation { get; set; }
        public ObjReference Subject { get; set; }
        
        /// <summary>
        /// IdBiopise
        /// </summary>
        public Code Code { get; set; }
        public DateTime EffectiveDateTime { get; set; }  
    }

    public class ObjReference
    {
        public string Reference { get; set; }
    }

    public class Observation
    {
        public string ResourceType { get; set; } = nameof(Observation);
        public string Status { get; set; } = "unknown";
        public Code Code { get; set; } = new Code();
        //public List<Component> Component { get; set; } = new List<Component> { };
        public List<ObjReference> DerivedFrom { get; set; } = new();

        public ObjReference Specimen { get; set; }

        public List<Dictionary<string, string>> Extension { get; set; } = new List<Dictionary<string, string>> { new Dictionary<string, string> { { "url", "Test" }, { "valueString", "TestVal" } } };
    }

    public class Code
    {
        public string Text { get; set; } = "TEST";
    }

    public class Specimen
    {
        public string Id { get; set; } = "0";
        public string ResourceType { get; set; } = nameof(Specimen);

        public Code Type { get; set; } = new Code { Text = "unknown" };

        public List<Dictionary<string, string>> Extension { get; set; } = new List<Dictionary<string, string>>();

        public Collection Collection { get; set; } = new Collection { collectedDateTime = "1999" };

        public List<Processing> Processing { get; set; } = new List<Processing> { new Processing {  TimeDateTime = "1999"  } };

    }

    public class Identifier
    {
        public string Use { get; set; }

        public FhirCodeableConcept type { get; set; }

        public string System { get; set; }

        public string Value { get; set; }

        public FhirPeriod Period { get; set; }

    }

    public class FhirPeriod
    {
        public string Start { get; set; }

        public string End { get; set; }
    }

    public class FhirCodeableConcept
{
    // You may need to adjust this based on the actual structure of CodeableConcept
    public Code Coding { get; set; }

    public string Text { get; set; }
}

public class Collection
    {
        public string collectedDateTime { get; set; }
    }

    public class Collected
    {
        public string CollectedDateTime { get; set; }
    }

    public class Processing
    {
        public string TimeDateTime { get; set; }
    }


    public class Time
    {
        public string TimeDateTime { get; set; }
    }

    public class Component
    {
        public Code Code { get; set; }
        public string ValueString { get; set; }
    }


    public class MolecularSequence
    {
        public string Id { get; set; } = "0";
        public string ResourceType { get; set; } = nameof(MolecularSequence);
        public int CoordinateSystem { get; set; }

        public ReferenceSeq ReferenceSeq { get; set; } = new ReferenceSeq();

        public ObjReference Patient { get; set; }
        public List<Variant> Variant { get; set; } = new List<Variant>();
        public string ObservedSeq { get; set; } = "A";
        public List<Quality> Quality { get; set; }
        public int ReadCoverage { get; set; }
    }

    public class ReferenceSeq
    {
        public Code Chromosome { get; set; } = new Code { Text = "placeholder" };

        public string Orientation { get; set; } = "sense";

    }

    public class Variant
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string ObservedAllele { get; set; } = "A";
        public string ReferenceAllele { get; set; } = "A";
    }

    public class Quality
    {
        public string Type { get; set; } = "snp";
        public Score Score { get; set; } = new Score();
    }

    public class Score
    {
        public int Value { get; set; }
    }
}
