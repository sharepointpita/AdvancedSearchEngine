using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using enums = AdvancedSearch.Enums;

namespace AdvancedSearch
{
    public abstract class FieldTypeBase
    {
        private enums.FieldTypeName _type;

        public enums.FieldTypeName Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public virtual Operator DefaultOperator
        {
            get
            {
                return new Operator("=", "= '{0}'");
            }
        }

        public abstract List<Operator> Operators 
        { 
            get; 
        }

        public FieldTypeBase(enums.FieldTypeName type)
        {
            _type = type;
        }

        public override string ToString()
        {
            if (_type != null)
                return _type.ToString();
            else
                return base.ToString();
        }

  
        
    }
}