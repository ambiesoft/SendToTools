using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeFileName
{
    public class RegexItem
    {
        string _name;
        string _reg;
        string _rep;
        bool _allIsActivating;
        public RegexItem(string name, string reg, string rep, bool bAll)
        {
            _name = string.IsNullOrEmpty(name) ?
                Properties.Resources.SRT_REG_NONAME : name;
            _reg = reg;
            _rep = rep;
            _allIsActivating = bAll;
        }
        public RegexItem(string name, string reg, string rep)
            :             this(name, reg, rep, false) { }

        public bool IsAll { get { return _allIsActivating; } }
        public string Name
        {
            get { return _name; }
        }
        public string RegexString
        {
            get { return _reg; }
        }
        public string Replacement
        {
            get { return _rep; }
        }

        internal void reset(string regexName, string regExString, string regExReplacement)
        {
            _name = regexName;
            _reg = regExString;
            _rep = regExReplacement;
        }
    }
}
