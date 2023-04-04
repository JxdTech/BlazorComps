using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace BlazorComps;

public abstract class BaseComponentBase : ComponentBase
{
    #region Members

    private string? _class;
    private readonly ClassBuilder _classBuilder;

    #endregion

    #region Constructors

    protected BaseComponentBase()
    {
        _classBuilder = new ClassBuilder(OnBuildClass);
    }

    #endregion

    #region Mehods

    protected virtual void OnBuildClass(ClassBuilder classBuilder)
    {
        classBuilder.Append(_class);
    }

    protected override Task OnParametersSetAsync()
    {
        if (AdditionalAttributes == null)
            return base.OnParametersSetAsync();

        if (string.IsNullOrWhiteSpace(Id) && AdditionalAttributes.TryGetValue("id", out var idValue))
        {
            Id = Convert.ToString(idValue, CultureInfo.CurrentCulture);
        }

        if (AdditionalAttributes.TryGetValue("class", out var classValue))
        {
            _class = Convert.ToString(classValue, CultureInfo.CurrentCulture);
        }

        return base.OnParametersSetAsync();
    }

    #endregion

    #region Properties

    public string? Id { get; private set; }

    public string? Classes => _classBuilder.ToString();

    [Inject] 
    protected IClassProvider ClassProvider { get; set; } = default!;

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    #endregion
}