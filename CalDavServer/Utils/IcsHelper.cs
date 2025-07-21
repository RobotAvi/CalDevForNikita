using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CalDavServer.Models;

namespace CalDavServer.Utils
{
    public static class IcsHelper
    {
        public static string GenerateIcs(IEnumerable<Event> events)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            foreach (var ev in events)
            {
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine($"UID:{ev.Id}");
                sb.AppendLine($"SUMMARY:{ev.Summary}");
                sb.AppendLine($"DESCRIPTION:{ev.Description}");
                sb.AppendLine($"DTSTART:{ev.Start:yyyyMMddTHHmmssZ}");
                sb.AppendLine($"DTEND:{ev.End:yyyyMMddTHHmmssZ}");
                sb.AppendLine("END:VEVENT");
            }
            sb.AppendLine("END:VCALENDAR");
            return sb.ToString();
        }

        public static List<Event> ParseIcs(string icsData)
        {
            var events = new List<Event>();
            if (string.IsNullOrWhiteSpace(icsData)) return events;
            var lines = icsData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Event current = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("BEGIN:VEVENT")) current = new Event();
                else if (line.StartsWith("END:VEVENT") && current != null) { events.Add(current); current = null; }
                else if (current != null)
                {
                    if (line.StartsWith("UID:")) current.Id = Guid.Parse(line.Substring(4));
                    else if (line.StartsWith("SUMMARY:")) current.Summary = line.Substring(8);
                    else if (line.StartsWith("DESCRIPTION:")) current.Description = line.Substring(12);
                    else if (line.StartsWith("DTSTART:")) current.Start = DateTime.ParseExact(line.Substring(8), "yyyyMMdd'T'HHmmss'Z'", null);
                    else if (line.StartsWith("DTEND:")) current.End = DateTime.ParseExact(line.Substring(6), "yyyyMMdd'T'HHmmss'Z'", null);
                }
            }
            return events;
        }

        public static void ExportToFile(IEnumerable<Event> events, string filePath)
        {
            var ics = GenerateIcs(events);
            File.WriteAllText(filePath, ics);
        }

        public static List<Event> ImportFromFile(string filePath)
        {
            var ics = File.ReadAllText(filePath);
            return ParseIcs(ics);
        }
    }
}