using BlazorComps.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComps;

public class Sidebar : BaseComponent, ISidebar, IDisposable
{
    [Inject] private ISidebarService SidebarService { get; set; } = default!;
    [Parameter] public new string? Id { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public BreakpointOptions<bool?>? Show { get; set; }
    [Parameter] public BreakpointOptions<bool?>? Expand { get; set; }
    public bool Expanded { get; private set; }

    protected override Task OnInitializedAsync()
    {
        SidebarService.AddSidebar(this);
        return base.OnInitializedAsync();
    }

    public async ValueTask ToggleExpandAsync()
    {
        Expanded = !Expanded;
        await InvokeAsync(StateHasChanged);
    }

    protected override void OnBuildClass(ClassBuilder builder)
    {
        builder.Append(ClassProvider.Sidebar);
        if (Show != null)
            builder.Append(ClassProvider.Show, Show.Value(Viewport.Breakpoint));
        if (Expanded)
            builder.Append(ClassProvider.Expand);
        else if(Expand != null)
            builder.Append(ClassProvider.Expand, Expand.Value(Viewport.Breakpoint));
        base.OnBuildClass(builder);
    }

    protected override void OnAddToRenderTree(int sequence, RenderTreeBuilder builder)
    {
        // will add header, body, footer
        builder.AddAttribute(sequence++, "id", Id);
        builder.AddContent(sequence++, ChildContent);
        base.OnAddToRenderTree(sequence, builder);
    }

    public void Dispose()
    {
        SidebarService.RemoveSidebar(this);
    }
}