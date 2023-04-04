using Microsoft.JSInterop;

namespace BlazorComps;

public class BreakpointInterop : IAsyncDisposable, IBreakpointInterop
{
    public Func<ValueTask>? OnInitialized { get; set; }
    public Func<ValueTask>? OnBreakpointChanged { get; set; }
    
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private IReadOnlyDictionary<Breakpoint, int> _breakpoints = new Dictionary<Breakpoint, int>()
    {
        [Breakpoint.Xs] = 0,
        [Breakpoint.Sm] = 576,
        [Breakpoint.Md] = 768,
        [Breakpoint.Lg] = 1024,
        [Breakpoint.Xl] = 1200,
        [Breakpoint.Xxl] = 1400
    };
    
    public bool Initialized { get; private set; }
    public Breakpoint Breakpoint { get; private set; }

    public BreakpointInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new(
            () => jsRuntime
                .InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/BlazorComps/js/breakpoint-interop.js"
                ).AsTask());
    }

    public async ValueTask InitializeAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("initialize", DotNetObjectReference.Create(this), _breakpoints);
    }

    [JSInvokable]
    public async ValueTask OnInitializedAsync()
    {
        if (Initialized)
            throw new InvalidOperationException($"{GetType().Name} has already been initialize");
        Initialized = true;
        if(OnInitialized == null)
            throw new Exception($"{nameof(OnInitialized)} is null.");
        await OnInitialized.Invoke();
    }

    [JSInvokable]
    public async ValueTask OnBreakpointChangedAsync(Breakpoint breakpoint)
    {
        if (Breakpoint == breakpoint)
            return;
        if(OnBreakpointChanged == null)
            throw new Exception($"{nameof(OnBreakpointChanged)} is null.");
        Breakpoint = breakpoint;
        await OnBreakpointChanged.Invoke();
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}