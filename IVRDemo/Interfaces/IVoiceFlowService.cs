namespace IVRDemo.Interfaces;

public interface IVoiceFlowService
{
    string MainMenu(string origin);
    string HandleMainSelection(string origin, string? digit);
    string SectionMenu(string origin, string sectionKey, string? prev);
    string HandleSectionSelection(string origin, string sectionKey, string? prev, string? digit);
    string ActionLeaf(string origin, string sectionKey, string choice, string? prev);
    string HandleActionLeaf(string origin, string? prev, string? digit);
}
