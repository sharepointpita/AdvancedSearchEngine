using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using enums = AdvancedSearch.Enums;

namespace AdvancedSearch
{
    public abstract class SimpleTypeBase : FieldTypeBase
    {
        public SimpleTypeBase(enums.FieldTypeName type)
            : base(type)
        {
 
        }

        public abstract override List<Operator> Operators
        {
            get;
        }
    }
}
