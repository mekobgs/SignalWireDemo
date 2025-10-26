namespace IVRDemo.Interfaces;

public interface IXmlResponseBuilder
{
    string Gather(string message, string actionUrl);
    string Redirect(string targetUrl);
}
