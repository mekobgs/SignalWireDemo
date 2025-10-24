using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Helper: Write the answer XML to the response
static async Task WriteXml(HttpContext ctx, XElement xml)
{
    ctx.Response.ContentType = "application/xml";
    await ctx.Response.WriteAsync(xml.ToString(SaveOptions.DisableFormatting));
}

// Helper: Create a dynamic menu
static XElement CreateGatherMenu(string message, string actionUrl)
{
    return new XElement("Response",
        new XElement("Say", message),
        new XElement("Gather",
            new XAttribute("numDigits", "1"),
            new XAttribute("action", actionUrl),
            new XAttribute("method", "POST"))
    );
}

// ========== MAIN MENU ==========
app.MapPost("/voice/incoming", async ctx =>
{
    var response = CreateGatherMenu(
        "Welcome to the main menu. Press 1 for Sales, 2 for Support, or 3 for Billing.",
        "/voice/handle-main"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/handle-main", async ctx =>
{
    var form = await ctx.Request.ReadFormAsync();
    var digit = form["Digits"].ToString();

    string redirect = digit switch
    {
        "1" => "/voice/menu1",
        "2" => "/voice/menu2",
        "3" => "/voice/menu3",
        _ => "/voice/incoming"
    };

    var response = new XElement("Response", new XElement("Redirect", redirect));
    await WriteXml(ctx, response);
});

// ========== MENU 1 ==========
app.MapPost("/voice/menu1", async ctx =>
{
    var response = CreateGatherMenu(
        "You are in the Sales menu. Press 1, 2, or 3 for options. Press 9 to go back.",
        "/voice/handle-menu1"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/handle-menu1", async ctx =>
{
    var form = await ctx.Request.ReadFormAsync();
    var digit = form["Digits"].ToString();

    string redirect = digit switch
    {
        "9" => "/voice/incoming", // go back
        "1" or "2" or "3" => "/voice/action1",
        _ => "/voice/menu1"
    };

    var response = new XElement("Response", new XElement("Redirect", redirect));
    await WriteXml(ctx, response);
});

// ========== MENU 2 ==========
app.MapPost("/voice/menu2", async ctx =>
{
    var response = CreateGatherMenu(
        "You are in the Support menu. Press 1, 2, or 3 for options. Press 9 to go back.",
        "/voice/handle-menu2"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/handle-menu2", async ctx =>
{
    var form = await ctx.Request.ReadFormAsync();
    var digit = form["Digits"].ToString();

    string redirect = digit switch
    {
        "9" => "/voice/incoming",
        "1" or "2" or "3" => "/voice/action2",
        _ => "/voice/menu2"
    };

    var response = new XElement("Response", new XElement("Redirect", redirect));
    await WriteXml(ctx, response);
});

// ========== MENU 3 ==========
app.MapPost("/voice/menu3", async ctx =>
{
    var response = CreateGatherMenu(
        "You are in the Billing menu. Press 1, 2, or 3 for options. Press 9 to go back.",
        "/voice/handle-menu3"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/handle-menu3", async ctx =>
{
    var form = await ctx.Request.ReadFormAsync();
    var digit = form["Digits"].ToString();

    string redirect = digit switch
    {
        "9" => "/voice/incoming",
        "1" or "2" or "3" => "/voice/action3",
        _ => "/voice/menu3"
    };

    var response = new XElement("Response", new XElement("Redirect", redirect));
    await WriteXml(ctx, response);
});

// ========== ACTIONS ==========
app.MapPost("/voice/action1", async ctx =>
{
    var response = CreateGatherMenu(
        "You selected an option from the Sales menu. Press 9 to go back.",
        "/voice/menu1"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/action2", async ctx =>
{
    var response = CreateGatherMenu(
        "You selected an option from the Support menu. Press 9 to go back.",
        "/voice/menu2"
    );
    await WriteXml(ctx, response);
});

app.MapPost("/voice/action3", async ctx =>
{
    var response = CreateGatherMenu(
        "You selected an option from the Billing menu. Press 9 to go back.",
        "/voice/menu3"
    );
    await WriteXml(ctx, response);
});

app.Run();
