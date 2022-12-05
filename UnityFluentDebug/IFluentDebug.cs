using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityFluentDebug
{
public interface IFluentDebug 
{
   public bool Enabled { get; set; }
   public bool? LastCondition { get; }
   IFluentConditions FluentConditions { get; }
   IFluentStatements FluentStatements { get; }

   #region DebugOverloads
   
   IFluentConditions Assert(bool condition);
   IFluentConditions Assert(bool condition, Object context);
   IFluentConditions Assert(bool condition, object message);
   IFluentConditions Assert(bool condition, object message, Object context);
   IFluentConditions AssertFormat(bool condition, string format, params object[] args);
   IFluentConditions AssertFormat(bool condition, Object context, string format, params object[] args);
   IFluentConditions Break();
   IFluentConditions ClearDeveloperConsole();
   IFluentConditions DrawLine(Vector3 start, Vector3 end, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true);
   IFluentConditions DrawRay(Vector3 start, Vector3 dir, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true);
   IFluentConditions Log(object message);
   IFluentConditions Log(object message, Object context);
   IFluentConditions LogAssertion(object message);
   IFluentConditions LogAssertion(object message, Object context);
   IFluentConditions LogAssertionFormat(string format, params object[] args);
   IFluentConditions LogAssertionFormat(Object context, string format, params object[] args);
   IFluentConditions LogError(object message);
   IFluentConditions LogError(object message, Object context);
   IFluentConditions LogErrorFormat(string format, params object[] args);
   IFluentConditions LogErrorFormat(Object context, string format, params object[] args);
   IFluentConditions LogException(Exception exception);
   IFluentConditions LogException(Exception exception, Object context);
   IFluentConditions LogFormat(string format, params object[] args);
   IFluentConditions LogFormat(Object context, string format, params object[] args);
   IFluentConditions LogFormat(LogType logType, LogOption logOptions, Object context, string format, 
      params object[] args);
   IFluentConditions LogWarning(object message);
   IFluentConditions LogWarning(object message, Object context);
   IFluentConditions LogWarningFormat(string format, params object[] args);
   IFluentConditions LogWarningFormat(Object context, string format, params object[] args);
   
   #endregion
   
   #region DebugExtensions
   
   IFluentConditions AssertNotNull(object obj, object message);
   IFluentConditions AssertNotNull(object obj, object message, Object context);
   IFluentConditions Say(string msg, bool clearPreviousMessages = false);
   IFluentConditions Execute(Action method);
   IFluentConditions Enable();
   IFluentConditions Disable();
   IFluentConditions PlayAudioClip(AudioClip clip, Vector3 position, float volume);
   
   #endregion

   #region DebugConditionals
   
   IFluentStatements If(bool condition);
   IFluentStatements If(Func<bool> condition);
   
   #endregion
}
}
