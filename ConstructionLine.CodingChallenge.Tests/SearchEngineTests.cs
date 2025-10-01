using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Models;
using ConstructionLine.CodingChallenge.Services;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new (Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new (Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new (Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = [Color.Red],
                Sizes = [Size.Small]
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
    }
}
