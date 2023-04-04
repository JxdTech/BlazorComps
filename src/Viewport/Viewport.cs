using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComps;

public interface IViewport
{
    bool Initialized { get; }
    Breakpoint Breakpoint { get; }
}

public class Viewport : BaseComponentBase, IViewport
{
    public Breakpoint Breakpoint => BreakpointInterop.Breakpoint;
    public bool Initialized => BreakpointInterop.Initialized;

    [Inject] public IBreakpointInterop BreakpointInterop { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override Task OnInitializedAsync()
    {
        BreakpointInterop.OnInitialized = NotifyIfInitialized;
        BreakpointInterop.OnBreakpointChanged = OnBreakpointChanged;
        return base.OnInitializedAsync();
    }

    private async ValueTask NotifyIfInitialized()
    {
        if (Initialized)
            await InvokeAsync(StateHasChanged);
    }
    
    private async ValueTask OnBreakpointChanged()
    {
        // no point in updating view until everything is initialized
        // ....for now at least
        if(!Initialized)
            return;
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await BreakpointInterop.InitializeAsync();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnBuildClass(ClassBuilder classBuilder)
    {
        classBuilder.Append(ClassProvider.Viewport);
        base.OnBuildClass(classBuilder);
    }

    protected override void OnAddToRenderTree(int sequence, RenderTreeBuilder builder)
    {
        builder.AddAttribute(sequence++, "data-breakpoint", Breakpoint.ToString().ToLower());
        builder.AddAttribute(sequence++, "data-initialized", Initialized.ToString().ToLower());
        builder.OpenComponent<CascadingValue<IViewport>>(sequence++);
        builder.AddAttribute(sequence++, "Value", this);
        builder.AddAttribute(sequence++, "ChildContent", ChildContent);
        builder.CloseComponent();
    }
}
