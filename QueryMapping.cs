using System;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings
{
    public enum MappingType: byte
    {
        Data,
        Update
    }

    public abstract class QueryMapping
    {
        public MappingType Type { get; protected set; }
        public Type From { get; protected set; }
        public Type To { get; protected set; }
        public bool Is<TFirst, TSecond>(MappingType type)
        {
            return typeof(TFirst) == From
                && typeof(TSecond) == To
                && Type == type;
        }

        public bool IsTo<TTo>()
        {
            return To == typeof (TTo);
        }

        public bool IsFrom<TFrom>()
        {
            return From == typeof (TFrom);
        }
    }

    public class QueryMapping<TFrom, TTo> : QueryMapping
    {
        public QueryMapping(Expression<Func<TFrom, TTo>> mapping)
        {
            From = typeof(TFrom);
            To = typeof(TTo);
            Type = MappingType.Data;
            _mapping = mapping;
        }

        private readonly Expression<Func<TFrom, TTo>> _mapping;
        public Expression<Func<TFrom, TTo>> Get()
        {
            return _mapping;
        }
    }

    public class UpdateMapping<TFrom, TTo> : QueryMapping
    {
        public UpdateMapping(Func<TFrom, TTo, TTo> mapping)
        {
            From = typeof (TFrom);
            To = typeof (TTo);
            Type = MappingType.Update;
            _mapping = mapping;
        }

        private readonly Func<TFrom, TTo, TTo> _mapping;
        public TTo Update(TFrom model, TTo entity)
        {
            return _mapping(model, entity);
        }
    }

    public class QueryMapping<TFirst, TSecond, TTo> : QueryMapping
    {
        public QueryMapping(Expression<Func<TFirst, TSecond, TTo>> mapping)
        {
            From = typeof(TFirst);
            To = typeof(TTo);
            Type = MappingType.Data;
            _mapping = mapping;
        }

        private readonly Expression<Func<TFirst, TSecond, TTo>> _mapping;
        public Expression<Func<TFirst, TSecond, TTo>> Get()
        {
            return _mapping;
        }
    }
}
