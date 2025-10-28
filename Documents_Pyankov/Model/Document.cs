using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Pyankov.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public int IdDocument { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public string Direction { get; set; }
    }
}
