using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedSearch
{
    public class BooleanType : SimpleTypeBase
    {
        public BooleanType(): base(Enums.FieldTypeName.Boolean)
        {
 
        }

        public override List<Operator> Operators
        {
            get
            {
                return new List<Operator>()
                {
                    new Operator("is true", "= 1"),
                    new Operator("is false", "= 0")
                }; 
            }
        }

        public override Operator DefaultOperator
        {
            get
            {
                return new Operator("is true", "= 1");
            }
        }

    }
}