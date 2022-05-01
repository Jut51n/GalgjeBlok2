using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Word
    {
        public int Id { get; set; }
        public string Woord { get; set; }

        public Word(string word)
        {
            Woord = word;
        }

        public Word()
        {
        }
    }
}
