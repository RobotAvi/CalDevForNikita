namespace CalDavServer.Utils
{
    public static class IcsHelper
    {
        public static string GenerateIcs() => "BEGIN:VCALENDAR\nEND:VCALENDAR";
        public static void ParseIcs(string icsData) { /* TODO: Реализовать парсинг */ }
    }
}