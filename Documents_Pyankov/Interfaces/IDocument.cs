using Documents_Pyankov.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Pyankov.Interfaces
{
    public interface IDocument
    {
        void Save(bool update = false);
        List<DocumentContext> AllDocuments();
        void Delete();
    }
}
