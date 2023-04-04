using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComps;

public class Sidebar : BaseComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    protected override void OnBuildClass(ClassBuilder builder)
    {
        builder.Append(ClassProvider.Sidebar);
        base.OnBuildClass(builder);
    }

    protected override void OnAddToRenderTree(int sequence, RenderTreeBuilder builder)
    {
        // will add header, body, footer
        builder.AddContent(sequence++, ChildContent);
        base.OnAddToRenderTree(sequence, builder);
    }
}