using System.Collections.Generic;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities.CompareObjects
{
    public interface IComparator
    {
        bool AreEqual(object expected, object actual);

        bool AreEqual(object expected, object actual, out IEnumerable<IDifference> differences);
    }
}