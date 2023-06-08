using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;

namespace Modules.Core.Domain.Preferences
{
    public abstract class Preference : ValueObject
    {
        // Getters
        public string Key { get; }
        public abstract object ValueAsObject { get; }

        // Constructors
        protected Preference(string key)
        {
            Key = key;
        }
    }

    public class Preference<T> : Preference
    {
        // Getters
        public override object ValueAsObject => Value;
        public T Value { get; }

        // Interfaces
        protected Preference<T> SetValue(T value)
        {
            Contracts.Require(value != null, "Preference value cannot be null");
            return new Preference<T>(Key, value);
        }

        // Constructors
        protected Preference(string key, T value) : base(key)
        {
            Value = value;
        }

        // Factories
        public static Preference<T> Create(string key, T value)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference key cannot be null or empty");
            Contracts.Require(value != null, "Preference value cannot be null");
            return new Preference<T>(key, value);
        }

        // Operators
        public static implicit operator T(Preference<T> preference) => preference.Value;

        // Overrides
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
            yield return Value;
        }
    }
}