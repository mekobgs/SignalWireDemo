using Microsoft.AspNetCore.Mvc;
using IVRDemo.Interfaces;

namespace IVRDemo.Controllers;

[ApiController]
[Route("voice")]
public class VoiceController : ControllerBase
{
    private readonly IVoiceFlowService _flowService;

    public VoiceController(IVoiceFlowService flowService)
    {
        _flowService = flowService;
    }

    [HttpPost("incoming")]
    public IActionResult IncomingCall()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.MainMenu(baseUrl);
        return Content(xml, "application/xml");
    }

    [HttpPost("handle-main")]
    public async Task<IActionResult> HandleMainMenu()
    {
        var form = await Request.ReadFormAsync();
        var digit = form["Digits"].ToString();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.HandleMainSelection(baseUrl, digit);
        return Content(xml, "application/xml");
    }

    [HttpPost("menu/{section}")]
    public IActionResult SubMenu(string section, [FromQuery] string? prev)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.SectionMenu(baseUrl, section, prev);
        return Content(xml, "application/xml");
    }

    [HttpPost("handle-menu/{section}")]
    public async Task<IActionResult> HandleSubMenu(string section, [FromQuery] string? prev)
    {
        var form = await Request.ReadFormAsync();
        var digit = form["Digits"].ToString();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.HandleSectionSelection(baseUrl, section, prev, digit);
        return Content(xml, "application/xml");
    }

    [HttpPost("action/{section}/{choice}")]
    public IActionResult ActionLevel(string section, string choice, [FromQuery] string? prev)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.ActionLeaf(baseUrl, section, choice, prev);
        return Content(xml, "application/xml");
    }

    [HttpPost("handle-action/{section}/{choice}")]
    public async Task<IActionResult> HandleActionLevel(string section, string choice, [FromQuery] string? prev)
    {
        var form = await Request.ReadFormAsync();
        var digit = form["Digits"].ToString();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xml = _flowService.HandleActionLeaf(baseUrl, prev, digit);
        return Content(xml, "application/xml");
    }

}
