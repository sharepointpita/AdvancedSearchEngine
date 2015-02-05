using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedSearch.Table
{
    public class DatabaseSchema
    {
        public List<Table> Tables = new List<Table>();

        public string DatabaseName;
    }

    public class Table
    {
        public List<Column> Columns = new List<Column>();

        public string Name;
    }

    public class Column
    {
        public string Name;
    }

}
