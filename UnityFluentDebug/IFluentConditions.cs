using System;

namespace UnityFluentDebug
{
public interface IFluentConditions
{
   IFluentStatements If(bool condition);
   IFluentStatements If(Func<bool> condition);
   IFluentStatements And();
   IFluentStatements AndIf(bool condition);
   IFluentStatements AndIf(Func<bool> condition);
}
}