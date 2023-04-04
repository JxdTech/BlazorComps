namespace BlazorComps.Services;

public interface ISidebarService
{
    bool SidebarExpanded(string sidebarId);
    ValueTask ToggleSidebarExpandAsync(string sidebarId);
    void AddSidebar(ISidebar sidebar);
    void RemoveSidebar(ISidebar sidebar);
}