namespace BlazorComps;

public interface IBreakpointInterop
{
    Func<ValueTask>? OnInitialized { get; set; }
    Func<ValueTask>? OnBreakpointChanged { get; set; }
    bool Initialized { get; }
    Breakpoint Breakpoint { get; }
    ValueTask InitializeAsync();
}