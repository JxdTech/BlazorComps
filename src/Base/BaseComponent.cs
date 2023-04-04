using Microsoft.AspNetCore.Components;

namespace BlazorComps;

public abstract class BaseComponent : BaseComponentBase
{
    #region Properties

    [CascadingParameter] 
    protected IViewport Viewport { get; set; }

    #endregion
}