using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using enums = AdvancedSearch.Enums;

namespace AdvancedSearch
{
    public abstract class ComplexTypeBase : FieldTypeBase
    {

        public ComplexTypeBase(enums.FieldTypeName type)
            : base(type)
        {
 
        }


        public override abstract List<Operator> Operators
        {
            get;
        }



        /// <summary>
        /// Used for Lazy Load constructions (E.g. properties which contains data fetch from database).
        /// </summary>
        public virtual void InitializeAdditionalMembers() { }
        


    }
}
