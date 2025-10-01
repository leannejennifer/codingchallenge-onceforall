using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Models
{
    public class Size
    {
        public Guid Id { get; }

        public string Name { get; }

        private Size(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static readonly Size Small = new (Guid.NewGuid(), "Small");
        public static readonly Size Medium = new (Guid.NewGuid(), "Medium");
        public static readonly Size Large = new (Guid.NewGuid(), "Large");

        public static List<Size> All = 
            [
                Small,
                Medium,
                Large
            ];
    }
}