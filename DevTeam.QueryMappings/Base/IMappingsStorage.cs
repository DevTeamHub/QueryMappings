﻿
namespace DevTeam.QueryMappings.Base
{
    /// <summary>
    /// Helps to find mappings in solution. All mappings should be described in implementations of <see cref="IMappingsStorage"/>. 
    /// All <see cref="IMappingsStorage"/> instances will be analyzed at the start of application and mappings will be loaded into memory.
    /// Should have default constructor.
    /// </summary>
    public interface IMappingsStorage
    {
        /// <summary>
        /// Method, where all mappings that belong to current Storage, should be described.
        /// </summary>
        void Setup();
    }
}
