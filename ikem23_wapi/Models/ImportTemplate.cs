namespace ikem23_wapi.Models
{
    public class ImportTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SheetNumber { get; set; }
        public List<ColumnDefinition> ColumnMapping { get; set; } = new List<ColumnDefinition>();
    }

    public class ColumnDefinition
    {
        public ColumnDefinition() { }
        public ColumnDefinition(string id, string excelColumn, string excelColumnHeader)
        {
            Id = id;
            ExcelColumnLetter = excelColumn;
            ExcelColumnHeader = excelColumnHeader;
        }

        public string Id { get; set; }
        public string ExcelColumnLetter { get; set; }
        public string ExcelColumnHeader{ get; set; }
    }
}
