using System;

namespace DevTeam.QueryMappings.Base
{
    public abstract class Mapping
    {
        public Type From { get; protected set; }
        public Type To { get; protected set; }

        public string Name { get; set; }

        protected Mapping(Type from, Type to, string name = null)
        {
            From = from;
            To = to;
            Name = name;
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
