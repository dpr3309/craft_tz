using System.Collections.Generic;

namespace Craft_TZ.Shared.SelectionAlgorithms
{
    public interface ISelector
    {
        IEnumerable<T> SelectItems<T>(IEnumerable<T> controllCollection);
    }
}