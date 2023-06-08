using System;
using System.Collections.Generic;

namespace Modules.Core.Domain.Entities
{
    public abstract class Identity : ValueObject
    {
        protected long Id { get; }

        public virtual bool IsNull => Id == 0;

        protected Identity(long id = 0)
        {
            if (id < 0) throw new ArgumentException("id must be greater than 0", nameof(id));
            Id = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static explicit operator long(Identity id) => id.Id;
    }

    public abstract class Identity<T> : Identity
    {
        protected new T Id { get; }

        public override bool IsNull => Id.Equals(default(T));

        protected Identity(T value)
        {
            Id = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static explicit operator T(Identity<T> id) => id.Id;
    }
}