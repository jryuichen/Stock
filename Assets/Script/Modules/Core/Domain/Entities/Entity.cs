namespace Modules.Core.Domain.Entities
{
    public abstract class Entity
    {
        protected Identity Id { get; }

        protected Entity(Identity id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id.IsNull || other.Id.IsNull) return false;
            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public abstract class Entity<T> : Entity
    {
        protected new Identity<T> Id { get; }

        protected Entity(Identity<T> id) : base(id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<T> other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id.IsNull || other.Id.IsNull) return false;
            return Id == other.Id;
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}