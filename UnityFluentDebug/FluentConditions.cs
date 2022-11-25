using System;

namespace UnityFluentDebug
{
public sealed class FluentConditions : IFluentConditions
{
    private readonly IFluentDebug _fluentDebug;

    internal FluentConditions(IFluentDebug fluentDebug) => _fluentDebug = fluentDebug;

    public IFluentStatements If(bool condition) => _fluentDebug.If(condition);

    public IFluentStatements If(Func<bool> condition) => _fluentDebug.If(condition);

    public IFluentStatements And() =>
        _fluentDebug.LastCondition != null
            ? If(_fluentDebug.LastCondition.Value)
            : _fluentDebug.FluentStatements;

    public IFluentStatements AndIf(bool condition) =>
        _fluentDebug.LastCondition != null
            ? If(_fluentDebug.LastCondition.Value && condition)
            : If(condition);

    public IFluentStatements AndIf(Func<bool> condition)
    {
        if (_fluentDebug.LastCondition != null)
            return _fluentDebug.LastCondition.Value ? If(condition) : If(false);
        
        return If(condition);
    }
}
}
