using Microsoft.AspNetCore.Components;

namespace BlazorComps;

public abstract class BaseComponent : BaseComponentBase
{
    [CascadingParameter] 
    protected IViewport Viewport { get; set; } = default!;
}