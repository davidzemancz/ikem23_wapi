namespace ikem23_wapi
{
    public class ImportTemplate
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ColumnDefinition> ColumnMapping { get;}
    }

    public class ColumnDefinition
    {
        public string Id { get; set; }

        public string ExcelColumn { get; set; }
    }
}
