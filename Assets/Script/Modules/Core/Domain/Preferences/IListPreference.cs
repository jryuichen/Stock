using System.Collections.Generic;

namespace Modules.Core.Domain.Preferences
{
    public interface IListPreference
    {
        string Key { get; }
        List<object> ToObjectList();
    }
}