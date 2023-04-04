namespace BlazorComps.Services;

public class SidebarService : ISidebarService
{
    private List<ISidebar> _sidebars = new();

    public bool SidebarExpanded(string sidebarId)
    {
        var sidebar = _sidebars.Find(e => e.Id == sidebarId);
        if(sidebar == null)
            throw new InvalidOperationException($"no sidebar exists with the id: {nameof(sidebarId)}");
        return sidebar.Expanded;
    }

    public async ValueTask ToggleSidebarExpandAsync(string sidebarId)
    {
        var sidebar = _sidebars.Find(e => e.Id == sidebarId);
        if(sidebar == null)
            throw new InvalidOperationException($"no sidebar exists with the id: {nameof(sidebarId)}");
        await sidebar.ToggleExpandAsync();
    }

    public void AddSidebar(ISidebar sidebar)
    {
        if (_sidebars.Contains(sidebar))
            throw new InvalidOperationException($"{nameof(sidebar)} has already been added");
        _sidebars.Add(sidebar);
    }
    
    public void RemoveSidebar(ISidebar sidebar)
    {
        if (!_sidebars.Contains(sidebar))
            throw new InvalidOperationException($"{nameof(sidebar)} has already been removed");
        _sidebars.Remove(sidebar);
    }
}