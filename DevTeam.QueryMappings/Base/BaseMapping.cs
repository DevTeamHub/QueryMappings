using System;

namespace DevTeam.QueryMappings.Base
{
    /// <summary>
    /// Describes one of 4 types of mappings: Expression Mapping, Parameterized Mapping, Query Mapping or Parameterized Query Mapping.
    /// Parameterized Query Mapping is just a combination of Parameterized and Query Mappings, so it's described with Flag enum.
    /// </summary>
    [Flags]
    public enum MappingType: byte
    {
        /// <summary>
        /// Expression Mapping: <see cref="Mappings.ExpressionMapping{TFrom, TTo}"/>
        /// </summary>
        Expression = 1,
        
        /// <summary>
        /// Parameterized Mapping: <see cref="Mappings.ParameterizedMapping{TFrom, TTo, TArgs}"/>
        /// </summary>
        Parameterized = 2,

        /// <summary>
        /// Query Mapping: <see cref="Mappings.QueryMapping{TFrom, TTo, TContext}"/>
        /// </summary>
        Query = 4,

        /// <summary>
        /// Parameterized Query Mapping: <see cref="Mappings.ParameterizedQueryMapping{TFrom, TTo, TArgs, TContext}"/>
        /// </summary>
        ParemeterizedQuery = 6
    }

    /// <summary>
    /// Base class of every mapping. Contains information about direction of mapping (From -> To), mapping type and mapping name (for Named Mappings).
    /// </summary>
    public abstract class Mapping
    {
        /// <summary>
        /// Source type of mapping.
        /// </summary>
        public Type From { get; protected set; }

        /// <summary>
        /// Destination type of mapping.
        /// </summary>
        public Type To { get; protected set; }

        /// <summary>
        /// Type of mapping. Described with help of <see cref="MappingType"/> enum.
        /// </summary>
        public MappingType MappingType { get; protected set; }

        /// <summary>
        /// Name of mapping. Can be used when it's needed to have more than one mapping from type A to type B.
        /// Name will be identifier of mapping in this case.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor of mapping class. Allows to set up direction of mapping.
        /// </summary>
        /// <param name="from">Source type of mapping.</param>
        /// <param name="to">Destination type of mapping.</param>
        /// <param name="mappingType">Type of mapping. Described with help of <see cref="MappingType"/> enum.</param>
        /// <param name="name">Name of mapping. Used as identifier for mappings with the same direction (From -> To).</param>
        protected Mapping(Type from, Type to, MappingType mappingType, string name = null)
        {
            From = from;
            To = to;
            Name = name;
            MappingType = mappingType;
        }

        /// <summary>
        /// Checks if current mapping has described direction.
        /// </summary>
        /// <typeparam name="TFirst">Source type of mapping.</typeparam>
        /// <typeparam name="TSecond">Destination type of mapping.</typeparam>
        /// <param name="name">Name of mapping. Used as identifier for mappings with the same direction (From -> To).</param>
        /// <returns>Result of comparison that signalizes if current mapping has the same direction.</returns>
        public bool Is<TFirst, TSecond>(string name = null)
        {
            return Is(typeof(TFirst), typeof(TSecond), name);
        }

        /// <summary>
        /// Checks if current mapping has described direction.
        /// </summary>
        /// <param name="from">Source type of mapping.</param>
        /// <param name="to">Destination type of mapping.</param>
        /// <param name="name">Name of mapping. Used as identifier for mappings with the same direction (From -> To).</param>
        /// <returns>Result of comparison that signalizes if current mapping has the same direction.</returns>
        public bool Is(Type from, Type to, string name = null)
        {
            if (!string.IsNullOrEmpty(name) && Name != name)
                return false;

            return from == From && to == To;
        }
    }
}
