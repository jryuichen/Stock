using System.Collections.Generic;
using ValueObject = CSharpFunctionalExtensions.ValueObject;

namespace Modules.Core.Domain.Preferences
{
    public abstract class Preferences : ValueObject
    {
        // Getters
        public abstract IReadOnlyList<Preference> AllPreferences { get; }
    }
}