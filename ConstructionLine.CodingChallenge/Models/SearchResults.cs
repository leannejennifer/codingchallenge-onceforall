using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Models
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; }

        public List<SizeCount> SizeCounts { get; set; }

        public List<ColorCount> ColorCounts { get; set; }
    }
}