using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Modules.Core.Domain.Preferences
{
    public class ListPreference<T> : Preference<IReadOnlyList<T>>, IListPreference
    {
        // Constructors
        protected ListPreference(string key, IReadOnlyList<T> value) : base(key, value)
        {
        }

        // Interfaces
        public List<T> ToList() => (List<T>)Value;

        public List<object> ToObjectList() => Value.Select(ConvertElementToObject).ToList();

        protected ListPreference<T> AddElement(T element)
        {
            Contracts.Require(element != null, "New preference element cannot be null");
            var newList = new List<T>(Value) { element };
            return new ListPreference<T>(Key, newList);
        }

        protected ListPreference<T> RemoveElement(T element)
        {
            Contracts.Require(element != null, "Preference element cannot be null");
            var newList = Value.Where(value => !EqualityComparer<T>.Default.Equals(value , element)).ToList();
            return new ListPreference<T>(Key, newList);
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
        public new static ListPreference<T> Create(string key, IReadOnlyList<T> values)
        {
            Contracts.Require(!string.IsNullOrEmpty(key), "Preference key cannot be null or empty");
            Contracts.Require(values != null, "Preference value list cannot be null");
            return new ListPreference<T>(key, values);
        }

        // Operators
        public static implicit operator List<T>(ListPreference<T> preference) => preference.ToList();

        public static explicit operator List<object>(ListPreference<T> preference) => preference.ToObjectList();

        // Overrides
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
            foreach (var value in Value)
                yield return value;
        }
    }
}