using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DpkViewer.Tools
{
    public class SearchTemplate
    {
        public List<int> ListAddresses { get; set; }
        public List<int> ListValues { get; set; }
        public bool ExactMatch { get; set; }

        public SearchTemplate()
        {
            ListAddresses = new List<int>();
            ListValues = new List<int>();
            ExactMatch = true;
        }

        public SearchTemplate(List<int> listAddresses, List<int> listValues, bool exactMatch):this()
        {
            ListAddresses.AddRange(listAddresses);
            ListValues.AddRange(listAddresses);
            ExactMatch = exactMatch;
        }

        public void Clear() { ListValues.Clear(); ListAddresses.Clear(); }
        
        public void AddListAddresses(List<int> listAddresses) { ListAddresses.AddRange(listAddresses); }
        public void AddListValues(List<int> listValues) { ListValues.AddRange(listValues); }
    }

    public class SearchTemplate_2
    {
        public List<int> ListAddresses { get; set; }
        public List<bool> Value { get; set; }
        public List<bool> Check { get; set; }

        public SearchTemplate_2()
        {
            ListAddresses = new List<int>();
            Value = new List<bool>();
            Check = new List<bool>();
        }

        public SearchTemplate_2(List<int> listAddresses, List<bool> value, List<bool> check)
            : this()
        {
            ListAddresses.AddRange(listAddresses);
            Value.AddRange(value);
            Check.AddRange(check);
        }

        public void Clear() { ListAddresses.Clear(); Value.Clear(); Check.Clear(); }

        public void AddListAddresses(List<int> listAddresses) { ListAddresses.AddRange(listAddresses); }
    }
}
