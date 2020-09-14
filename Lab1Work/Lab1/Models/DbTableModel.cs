using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class DbTableModel
    {
        public enum allowedDataType
        {
            intergerNumber,
            floatNumber,
            text,
            boolean
        }

        public class FieldInfo
        {
            public string FieldName { get; set; }
            public allowedDataType FieldType { get; set; }
            public bool IsPrimaryKey { get; set; }

            public FieldInfo(string name, allowedDataType type, bool isPrimaryKey)
            {
                this.FieldName = name;
                this.FieldType = type;
                this.IsPrimaryKey = isPrimaryKey;
            }

            public FieldInfo()
            {

            }
        }

        public List<List<string>> Table { get; set; } = new List<List<string>>();

        public List<FieldInfo> FieldsInfo { get; set; } = new List<FieldInfo>();
    }
}
