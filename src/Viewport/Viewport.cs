using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComps;

public interface IViewport
{
    bool IsLoaded { get; }
    bool IsRendered { get; }
    Breakpoint Breakpoint { get; }
    event EventHandler OnBreakpointChanged;
}

public class Viewport : BaseComponentBase, IViewport
{
    private EventHandler? _onBreakpointChanged { get; set; }
    public event EventHandler OnBreakpointChanged
    {
        add => _onBreakpointChanged += value;
        remove => _onBreakpointChanged -= value;
    }
    public Breakpoint Breakpoint { get; private set; }
    public bool IsRendered { get; private set; }
    public bool IsLoaded { get; private set; }
    
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override void OnBuildClass(ClassBuilder classBuilder)
    {
        classBuilder.Append(ClassProvider.Viewport);
        base.OnBuildClass(classBuilder);
    }

    protected override void OnAddToRenderTree(int sequence, RenderTreeBuilder builder)
    {
        builder.AddAttribute(sequence++, "data-breakpoint", Breakpoint.ToString().ToLower());
        builder.AddAttribute(sequence++, "data-rendered", IsRendered.ToString().ToLower());
        builder.AddAttribute(sequence++, "data-loaded", IsLoaded.ToString().ToLower());
        builder.OpenComponent<CascadingValue<IViewport>>(sequence++);
        builder.AddAttribute(sequence++, "Value", this);
        builder.AddAttribute(sequence++, "ChildContent", ChildContent);
        builder.CloseComponent();
    }
}
