using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccessLayer
{
    public class ColumnMetaData
    {
        public string FieldName { get; set; }
        public string HeaderName { get; set; }
        public int ColumnPosition { get; set; }
        public int RowPosition { get; set; }
        public int ResultSetIndex { get; set; }
        public string ControlType { get; set; }
    }

    public class DataPayload
    {
        public List<DataTable> DataSources;
        public List<ColumnMetaData> MetaList;
        public List<string> ColumnNames;
    }

    
}
