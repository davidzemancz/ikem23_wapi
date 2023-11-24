﻿namespace ikem23_wapi
{
    public class ImportTemplate
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int sheetNum { get; set; }

        public List<ColumnDefinition> ColumnMapping { get;} = new List<ColumnDefinition>();
    }

    public class ColumnDefinition
    {
        public string PropertyName { get; set; }

        public string ExcelColumn { get; set; }
    }
}
