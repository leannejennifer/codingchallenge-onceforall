using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Models
{
    public class Color
    {
        public Guid Id { get; }

        public string Name { get; }

        private Color(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static readonly Color Red = new (Guid.NewGuid(), "Red");
        public static readonly Color Blue = new (Guid.NewGuid(), "Blue");
        public static readonly Color Yellow = new (Guid.NewGuid(), "Yellow");
        public static readonly Color White = new (Guid.NewGuid(), "White");
        public static readonly Color Black = new (Guid.NewGuid(), "Black");

        public static List<Color> All =
            [
                Red,
                Blue,
                Yellow,
                White,
                Black
            ];
    }
}