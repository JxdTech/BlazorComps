using BlazorComps.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComps;

public class SidebarToggler : BaseComponent
{
    public override string TagName { get; set; } = "button";
    private string Type { get; set; } = "button";
    private bool? SidebarExpanded => string.IsNullOrWhiteSpace(SidebarId) 
        ? null : SidebarService.SidebarExpanded(SidebarId);
    [Inject] private SidebarService SidebarService { get; set; } = default!;
    [Parameter]public string? SidebarId { get; set; }
    [Parameter]public RenderFragment? ChildContent { get; set; }

    protected override void OnBuildClass(ClassBuilder builder)
    {
        builder.Append(ClassProvider.SidebarToggler);
        base.OnBuildClass(builder);
    }

    protected override void OnAddToRenderTree(int sequence, RenderTreeBuilder builder)
    {
        builder.AddAttribute(sequence++, "type", Type);
        builder.AddAttribute(sequence++, "aria-controls", SidebarId);
        builder.AddAttribute(sequence++, "aria-expanded", SidebarExpanded);
        builder.AddContent(sequence++, ChildContent);
        base.OnAddToRenderTree(sequence, builder);
    }
}