using System.Collections.Generic;

namespace Modules.Core.Domain.Preferences
{
    public interface IDictionaryPreference
    {
        string Key { get; }
        Dictionary<string, object> ToObjectDictionary();
    }
}