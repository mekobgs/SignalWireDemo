using IVRDemo.Interfaces;
using System.Xml.Linq;

namespace IVRDemo.Services;

public class XmlResponseBuilder : IXmlResponseBuilder
{
    public string Gather(string message, string actionUrl)
    {
        var xml = new XElement("Response",
            new XElement("Say", message),
            new XElement("Gather",
                new XAttribute("numDigits", "1"),
                new XAttribute("action", actionUrl),
                new XAttribute("method", "POST"))
        );

        return xml.ToString(SaveOptions.DisableFormatting);
    }

    public string Redirect(string targetUrl)
    {
        var xml = new XElement("Response",
            new XElement("Redirect", targetUrl)
        );

        return xml.ToString(SaveOptions.DisableFormatting);
    }
}
