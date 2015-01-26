using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedSearch
{
    public class Operator
    {

        private string _displayName;
        public string DisplayName { get { return _displayName; } }

        private string _sqlSyntax;
        public string SqlSyntax { get { return _sqlSyntax; } }

        public Operator(string displayName, string sqlSyntax)
        {
            _displayName = displayName;
            _sqlSyntax = sqlSyntax;
        }


    }
}