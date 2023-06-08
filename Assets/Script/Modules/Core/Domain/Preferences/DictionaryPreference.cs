using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Modules.Core.Domain.Preferences
{
    public class DictionaryPreference<T> : Preference<IReadOnlyDictionary<string, T>>, IDictionaryPreference
    {
        // Constructors
        protected DictionaryPreference(string key, IReadOnlyDictionary<string, T> value) : base(key, value)
        {
        }

        // Interfaces
        public Dictionary<string, T> ToDictionary() => (Dictionary<string, T>)Value;

        public Dictionary<string, object> ToObjectDictionary() =>
            Value.ToDictionary(pair => pair.Key, pair => ConvertElementToObject(pair.Value));

        protected bool ContainsKey(string key) => Value.ContainsKey(key);

        protected DictionaryPreference<T> AddElement(string key, T element)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference element key cannot be null or empty");
            Contracts.Require(element != null, "New preference element cannot be null");
            var newDictionary = new Dictionary<string, T>(ToDictionary()) { { key, element } };
            return new DictionaryPreference<T>(Key, newDictionary);
        }

        protected DictionaryPreference<T> UpdateElement(string key, T element)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference element key cannot be null or empty");
            Contracts.Require(element != null, "New preference element cannot be null");
            Contracts.Require(Value.ContainsKey(key), $"Preference dictionary does not contain key {key}");
            var newDictionary = new Dictionary<string, T>(ToDictionary()) { [key] = element };
            return new DictionaryPreference<T>(Key, newDictionary);
        }

        protected DictionaryPreference<T> RemoveKey(string key)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference element key cannot be null or empty");
            Contracts.Require(Value.ContainsKey(key), $"Preference dictionary does not contain key {key}");
            var newDictionary = new Dictionary<string, T>(ToDictionary());
            newDictionary.Remove(key);
            return new DictionaryPreference<T>(Key, newDictionary);
        }

        // Methods
        private static object ConvertElementToObject(T element) =>
            element switch
            {
                IListPreference listPreference => listPreference.ToObjectList(),
                IDictionaryPreference dictionaryPreference => dictionaryPreference.ToObjectDictionary(),
                Preference preference => preference.ValueAsObject,
                _ => element
            };

        // Factories
        public new static DictionaryPreference<T> Create(string key, IReadOnlyDictionary<string, T> values)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference key cannot be null or empty");
            Contracts.Require(values != null, "Preference value list cannot be null");
            return new DictionaryPreference<T>(key, values);
        }

        // Operators
        public static implicit operator Dictionary<string, T>(DictionaryPreference<T> preference) => preference.ToDictionary();

        public static explicit operator Dictionary<string, object>(DictionaryPreference<T> preference) =>
            preference.ToObjectDictionary();

        // Overrides
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
            foreach (var value in Value)
            {
                yield return value.Key;
                yield return value.Value;
            }
        }
    }
}