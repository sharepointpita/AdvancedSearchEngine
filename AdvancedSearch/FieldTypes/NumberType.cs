using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedSearch
{
    public class NumberType : SimpleTypeBase
    {
        public NumberType(): base(Enums.FieldTypeName.Number)
        {
 
        }

        public override List<Operator> Operators
        {
            get
            {
                return new List<Operator>()
                {
                    new Operator("<", "< {0}"),
                    new Operator(">", "> {0}"),
                    new Operator("=", "= {0}"),
                    new Operator("!=", "!= {0}")
                };
            }
        }

        public override Operator DefaultOperator
        {
            get
            {
                return new Operator("=", "= {0}");
            }
        }

    }
}