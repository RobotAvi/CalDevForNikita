using System.Xml.Linq;
using System.Collections.Generic;

namespace CalDavServer.Utils
{
    public static class WebDavHelper
    {
        public static string BuildPropFindResponse(IEnumerable<string> resourceNames)
        {
            var ns = XNamespace.Get("DAV:");
            var multistatus = new XElement(ns + "multistatus");
            foreach (var name in resourceNames)
            {
                var response = new XElement(ns + "response",
                    new XElement(ns + "href", name),
                    new XElement(ns + "propstat",
                        new XElement(ns + "prop",
                            new XElement(ns + "displayname", name)
                        ),
                        new XElement(ns + "status", "HTTP/1.1 200 OK")
                    )
                );
                multistatus.Add(response);
            }
            var doc = new XDocument(multistatus);
            return doc.ToString();
        }

        public static List<string> ParsePropFindRequest(string xml)
        {
            var result = new List<string>();
            var doc = XDocument.Parse(xml);
            XNamespace ns = "DAV:";
            foreach (var prop in doc.Descendants(ns + "prop"))
            {
                foreach (var el in prop.Elements())
                {
                    result.Add(el.Name.LocalName);
                }
            }
            return result;
        }
    }
}