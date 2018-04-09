using System;

namespace DevTeam.QueryMappings.Base
{
    [Flags]
    public enum MappingType: byte
    {
        Expression = 1,
        Parameterized = 2,
        Query = 4,
        ParemeterizedQuery = 6
    }

    public abstract class Mapping
    {
        public Type From { get; protected set; }
        public Type To { get; protected set; }
        public MappingType MappingType { get; protected set; }

        public string Name { get; set; }

        protected Mapping(Type from, Type to, MappingType mappingType, string name = null)
        {
            From = from;
            To = to;
            Name = name;
            MappingType = mappingType;
        }

        public bool Is<TFirst, TSecond>(string name = null)
        {
            return Is(typeof(TFirst), typeof(TSecond), name);
        }

        public bool Is(Type from, Type to, string name = null)
        {
            if (!string.IsNullOrEmpty(name) && Name != name)
                return false;

            return from == From && to == To;
        }
    }
}
