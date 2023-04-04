namespace BlazorComps;

public interface ISidebar
{
    bool Expanded { get; }
    string? Id { get; }
    ValueTask ToggleExpandAsync();
}