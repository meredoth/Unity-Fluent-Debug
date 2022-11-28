using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityFluentDebug
{
public sealed class FluentStatements : IFluentStatements
{
   private readonly IFluentDebug _fluentDebug;

   internal FluentStatements(IFluentDebug fluentDebug) => _fluentDebug = fluentDebug;

   #region DebugOverloads
   
   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Assert(bool condition) => _fluentDebug.Assert(condition);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Assert(bool condition, Object context) => _fluentDebug.Assert(condition, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Assert(bool condition, object message) => _fluentDebug.Assert(condition, message);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Assert(bool condition, object message, Object context) => _fluentDebug.Assert(condition, message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertFormat(bool condition, string format, params object[] args) => _fluentDebug.AssertFormat(condition, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertFormat(bool condition, Object context, string format, params object[] args) => _fluentDebug.AssertFormat(condition, context, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions ClearDeveloperConsole() => _fluentDebug.ClearDeveloperConsole();

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion

   public IFluentConditions Break() => _fluentDebug.Break();
   
   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions DrawLine(Vector3 start, Vector3 end, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true) =>
      _fluentDebug.DrawLine(start, end, nullableColor, duration, depthTest);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions DrawRay(Vector3 start, Vector3 dir, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true) =>
      _fluentDebug.DrawRay(start, dir, nullableColor, duration, depthTest);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Log(object message) => _fluentDebug.Log(message);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Log(object message, Object context) => _fluentDebug.Log(message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertion(object message) => _fluentDebug.LogAssertion(message);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertion(object message, Object context) => _fluentDebug.LogAssertion(message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertionFormat(string format, params object[] args) => _fluentDebug.LogAssertionFormat(format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertionFormat(Object context, string format, params object[] args) => _fluentDebug.LogAssertionFormat(context, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogError(object message) => _fluentDebug.LogError(message);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogError(object message, Object context) => _fluentDebug.LogError(message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogErrorFormat(string format, params object[] args) => _fluentDebug.LogErrorFormat(format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogErrorFormat(Object context, string format, params object[] args) => _fluentDebug.LogErrorFormat(context, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogException(Exception exception) => _fluentDebug.LogException(exception);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogException(Exception exception, Object context) => _fluentDebug.LogException(exception, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(string format, params object[] args) => _fluentDebug.LogFormat(format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(Object context, string format, params object[] args) => _fluentDebug.LogFormat(context, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(LogType logType, LogOption logOptions, Object context, string format, 
      params object[] args) =>
      _fluentDebug.LogFormat(logType, logOptions, context, format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarning(object message) => _fluentDebug.LogWarning(message);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarning(object message, Object context) => _fluentDebug.LogWarning(message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarningFormat(string format, params object[] args) => _fluentDebug.LogWarningFormat(format, args);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarningFormat(Object context, string format, params object[] args) => _fluentDebug.LogWarningFormat(context, format, args);

   #endregion DebugOverloads
   
   #region DebugExtensions

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertNotNull(object obj, object message) => _fluentDebug.AssertNotNull(obj, message);
   
   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertNotNull(object obj, object message, Object context) => _fluentDebug.AssertNotNull(obj, message, context);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Say(string msg, bool clearPreviousMessages = false) =>
      _fluentDebug.Say(msg, clearPreviousMessages);

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Execute(Action method) => _fluentDebug.Execute(method);

   public IFluentConditions Enable() => _fluentDebug.Enable();

   public IFluentConditions Disable() => _fluentDebug.Disable();

   #endregion DebugExtensions
}
}
