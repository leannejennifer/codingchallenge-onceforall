using System;
using System.Collections.Generic;
using System.Linq;
using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Services
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly List<ColorCount> _colorTotalCounts = [];
        private readonly List<SizeCount> _sizeTotalCounts = [];
        private readonly Dictionary<Guid, List<Shirt>> _shirtsByColor = [];
        private readonly Dictionary<Guid, List<Shirt>> _shirtsBySize = [];
        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts ?? [];

            foreach (var shirt in _shirts)
            {
                // Color counts
                var colorCount = _colorTotalCounts.FirstOrDefault(cc => cc.Color.Id == shirt.Color.Id);
                if (colorCount == null)
                {
                    _colorTotalCounts.Add(new ColorCount { Color = shirt.Color, Count = 1 });
                }
                else
                {
                    colorCount.Count++;
                }

                // Size counts
                var sizeCount = _sizeTotalCounts.FirstOrDefault(sc => sc.Size.Id == shirt.Size.Id);
                if (sizeCount == null)
                {
                    _sizeTotalCounts.Add(new SizeCount { Size = shirt.Size, Count = 1 });
                }
                else
                {
                    sizeCount.Count++;
                }

                // Index by color
                if (!_shirtsByColor.TryGetValue(shirt.Color.Id, out List<Shirt> shirtValue))
                {
                    shirtValue = [];
                    _shirtsByColor[shirt.Color.Id] = shirtValue;
                }

                shirtValue.Add(shirt);

                // Index by size
                if (!_shirtsBySize.TryGetValue(shirt.Size.Id, out List<Shirt> sizeValue))
                {
                    sizeValue = [];
                    _shirtsBySize[shirt.Size.Id] = sizeValue;
                }

                sizeValue.Add(shirt);
            }
        }


        public SearchResults Search(SearchOptions options)
        {
            // Shirts that match the color filter (or all shirts if no color filter)
            var shirtsMatchingColor = options.Colors.Count == 0
                ? _shirts
                : options.Colors.SelectMany(c => _shirtsByColor.TryGetValue(c.Id, out var shirts) ? shirts : Enumerable.Empty<Shirt>());

            // Shirts that match the size filter (or all shirts if no size filter)
            var shirtsMatchingSize = options.Sizes.Count == 0
                ? _shirts
                : options.Sizes.SelectMany(s => _shirtsBySize.TryGetValue(s.Id, out var shirts) ? shirts : Enumerable.Empty<Shirt>());

            // Final matching shirts must satisfy both filters
            var matchingShirts = shirtsMatchingColor.Intersect(shirtsMatchingSize).ToList();

            // Color counts should reflect the number of shirts per color among the size-filtered set
            var finalColorCounts = Color.All
                .Select(color => new ColorCount {
                    Color = color,
                    Count = shirtsMatchingSize.Count(s => s.Color.Id == color.Id)
                })
                .ToList();

            // Size counts should reflect the number of shirts per size among the color-filtered set
            var finalSizeCounts = Size.All
                .Select(size => new SizeCount {
                    Size = size,
                    Count = shirtsMatchingColor.Count(s => s.Size.Id == size.Id)
                })
                .ToList();

            return new SearchResults
            {
                Shirts = matchingShirts,
                ColorCounts = finalColorCounts,
                SizeCounts = finalSizeCounts
            };
        }
    }
}