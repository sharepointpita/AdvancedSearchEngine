using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedSearch.Enums
{
        public enum FieldTypeName
        {
            Text = 1,
            Number = 2,
            Boolean = 3,
            KeyValue = 4 // Containing key, value (E.g. picklist)
        }

        public enum FilterType
        {
            AND,
            OR
        }
}
