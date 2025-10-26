using IVRDemo.Interfaces;
using IVRDemo.Models;

namespace IVRDemo.Services;

public class VoiceFlowService : IVoiceFlowService
{
    private readonly IXmlResponseBuilder _xml;
    private readonly IMenuPromptService _prompts;

    public VoiceFlowService(IXmlResponseBuilder xml, IMenuPromptService prompts)
    {
        _xml = xml;
        _prompts = prompts;
    }

    public string MainMenu(string origin)
    {
        var action = $"{origin}/voice/handle-main";
        return _xml.Gather(_prompts.MainMenu(), action);
    }

    public string HandleMainSelection(string origin, string? digit)
    {
        var nextUrl = digit switch
        {
            DtmfKeys.One => $"{origin}/voice/menu/1?prev=/voice/incoming",
            DtmfKeys.Two => $"{origin}/voice/menu/2?prev=/voice/incoming",
            DtmfKeys.Three => $"{origin}/voice/menu/3?prev=/voice/incoming",
            _ => $"{origin}/voice/incoming"
        };

        return _xml.Redirect(nextUrl);
    }

    public string SectionMenu(string origin, string sectionKey, string? prev)
    {
        var action = $"{origin}/voice/handle-menu/{sectionKey}?prev={prev}";
        return _xml.Gather(_prompts.SectionPrompt(sectionKey), action);
    }

    public string HandleSectionSelection(string origin, string sectionKey, string? prev, string? digit)
    {
        if (digit == DtmfKeys.Back && prev != null)
            return _xml.Redirect($"{origin}{prev}");

        if (DtmfKeys.IsValid(digit))
        {
            var thisMenuAsPrev = $"/voice/menu/{sectionKey}?prev={prev}";
            var nextUrl = $"{origin}/voice/action/{sectionKey}/{digit}?prev={thisMenuAsPrev}";
            return _xml.Redirect(nextUrl);
        }

        return _xml.Redirect($"{origin}/voice/menu/{sectionKey}?prev={prev}");
    }
    public string ActionLeaf(string origin, string sectionKey, string choice, string? prev)
    {
        var message = $"You selected option {choice} in the {sectionKey} menu. Press 9 to go back.";
        var handleUrl = $"{origin}/voice/handle-action/{sectionKey}/{choice}?prev={prev}";
        return _xml.Gather(message, handleUrl);
    }

    public string HandleActionLeaf(string origin, string? prev, string? digit)
    {
        if (digit == DtmfKeys.Back && prev != null)
            return _xml.Redirect($"{origin}{prev}");

        return _xml.Redirect($"{origin}{prev}");
    }

}
