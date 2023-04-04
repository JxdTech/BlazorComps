using System.Text;

namespace BlazorComps;

public class ClassBuilder
{
    private const char _delimiter = ' ';
    private readonly Action<ClassBuilder> _buildAction;
    private readonly StringBuilder _stringBuilder = new();

    public ClassBuilder(Action<ClassBuilder> buildAction)
    {
        _buildAction = buildAction;
    }

    public void Append(string? value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return;
        _stringBuilder.Append(value).Append(_delimiter);
    }
    public void Append(string? value, bool? condition)
    {
        if(condition is not true)
            return;
        _stringBuilder.Append(value).Append(_delimiter);
    }

    public override string? ToString()
    {
        _stringBuilder.Clear();
        _buildAction.Invoke(this);
        return NullIfEmpty(_stringBuilder.ToString().TrimEnd());
    }

    private string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;
}