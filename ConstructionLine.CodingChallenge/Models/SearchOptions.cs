using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Models
{
    public class SearchOptions
    {
        public List<Size> Sizes { get; set; } = [];

        public List<Color> Colors { get; set; } = [];
    }
}