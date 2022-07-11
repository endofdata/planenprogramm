using System.Collections.Generic;

namespace Planenprogramm
{
	class TarpCategory
    {
        public int TarpCategoryId { get; set; }
        public string Name { get; set; } 
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Additional { get; set; }
        public int TarpTypeId { get; set; }
        public TarpType TarpType { get; set; }

        public List<Tarp> Tarps { get; set; }
    }
}
