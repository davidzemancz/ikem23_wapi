using System.Xml.Serialization;


namespace ikem23_wapi
{
    public class ImportObj
    {
        public Dictionary<(int, ColumnDefinition), string> data { get; set; }
    }
}
