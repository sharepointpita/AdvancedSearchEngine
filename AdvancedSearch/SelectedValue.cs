using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedSearch
{
    // Needed by Field class.. Contains Operator & Value combination
    public class SelectedValue
    {
        public Operator Operator { get; set; }
        public string Value { get; set; }
    }
}
