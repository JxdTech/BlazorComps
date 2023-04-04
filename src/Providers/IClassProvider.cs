namespace BlazorComps;

public interface IClassProvider
{
    string Expand { get; }
    string Show { get; }
    string Sidebar { get; }
    string SidebarToggler { get; }
    string Viewport { get; }
}