using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planenprogramm
{
    class Tarp
    {
        public int TarpId { get; set; }
        public int TarpTypeId { get; set; }
        public int TarpCategoryId { get; set; }
        public string Annotation { get; set; }
        public int Number { get; set; }
        public TarpType TarpType { get; set; }
        public TarpCategory TarpCategory { get; set; }
    }
}
