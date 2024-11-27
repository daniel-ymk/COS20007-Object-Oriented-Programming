using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHouse
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>();
            foreach (string ident in idents)
            {
                _identifiers.Add(ident.ToLower());
            }
        }
    }
}
