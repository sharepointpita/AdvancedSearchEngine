using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedSearch
{
    public class TextType : SimpleTypeBase
    {
        public TextType() : base(Enums.FieldTypeName.Text)
        {
 
        }

        public override List<Operator> Operators
        {
            get 
            {
                return new List<Operator>()
                {
                    new Operator("=", "= '{0}'"),
                    new Operator("contains", "LIKE '%{0}%'")
                };
            }
        }

        public override Operator DefaultOperator
        {
            get
            {
                return base.DefaultOperator;
            }
        }

    }
}