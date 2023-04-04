namespace BlazorComps;

public class BreakpointOptions<T>
{
    public T? Xs { get; set; }
    public T? Sm { get; set; }
    public T? Md { get; set; }
    public T? Lg { get; set; }
    public T? Xl { get; set; }
    public T? Xxl { get; set; }

    public T? Value(Breakpoint breakpoint)
    {
        return breakpoint switch
        {
            Breakpoint.Xs => Xs,
            Breakpoint.Sm => Sm ?? Xs,
            Breakpoint.Md => Md ?? Sm ?? Xs,
            Breakpoint.Lg => Lg ?? Md ?? Sm ?? Xs,
            Breakpoint.Xl => Xl ?? Lg ?? Md ?? Sm ?? Xs,
            Breakpoint.Xxl => Xxl ?? Xl ?? Lg ?? Md ?? Sm ?? Xs,
            _ => throw new ArgumentOutOfRangeException(nameof(breakpoint), breakpoint, null)
        };
    }
}