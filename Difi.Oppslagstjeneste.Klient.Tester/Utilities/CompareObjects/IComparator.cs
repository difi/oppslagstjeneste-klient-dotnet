using System.Collections.Generic;

namespace Digipost.Signature.Api.Client.Core.Tests.Utilities.CompareObjects
{
    public interface IComparator
    {
        bool AreEqual(object expected, object actual);

        bool AreEqual(object expected, object actual, out IEnumerable<IDifference> differences);
    }
}