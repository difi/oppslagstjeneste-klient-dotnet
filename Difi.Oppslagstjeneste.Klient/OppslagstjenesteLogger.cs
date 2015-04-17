using System.Diagnostics;

namespace Difi.Oppslagstjeneste.Klient
{
    internal static class OppslagstjenesteLogger
    {
        private static TraceSource s_traceSource = new TraceSource("Difi.Oppslagstjenesten");

        internal static void LogInfo(string message)
        {
            s_traceSource.TraceEvent(TraceEventType.Information, (int)DebugEvent.Soap, message);
        }

        internal enum DebugEvent
        {
            Soap
        }

        internal static void LogError(string p)
        {
            s_traceSource.TraceEvent(TraceEventType.Error, (int)DebugEvent.Soap, p);
        }
    }
}
