using IVRDemo.Interfaces;

namespace IVRDemo.Services;

public class MenuPromptService : IMenuPromptService
{
    public string MainMenu()
        => "Welcome to the main menu. Press 1 for Sales, 2 for Support, or 3 for Billing.";

    public string SectionPrompt(string section)
    {
        return section switch
        {
            "1" => "You are in the Sales menu. Press 1, 2, or 3. Press 9 to go back.",
            "2" => "You are in the Support menu. Press 1, 2, or 3. Press 9 to go back.",
            "3" => "You are in the Billing menu. Press 1, 2, or 3. Press 9 to go back.",
            _ => "Invalid section. Returning to main menu."
        };
    }
}
