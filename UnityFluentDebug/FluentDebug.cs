using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityFluentDebug
{
public sealed class FluentDebug : IFluentDebug
{

#if DISABLE_FLUENT_DEBUG
   public bool Enabled
   {
      get => false;
      set { } // cannot set if DISABLE_FLUENT_DEBUG, empty setter for compatibility.
   } 
#else
   public bool Enabled { get; set; } = true;
#endif
   
   public bool IsSpeechEngineInitialized => _speechEngine is {SpeechEngineInitialized: true};

   public static bool isDebugBuild => Debug.isDebugBuild;
   public static ILogger unityLogger => Debug.unityLogger;
   public static bool developerConsoleVisible
   {
      get => Debug.developerConsoleVisible;
      set => Debug.developerConsoleVisible = value;
   }
   public IFluentConditions FluentConditions { get; }
   public IFluentStatements FluentStatements { get; }
   public bool? LastCondition { get; private set; }
   
   private readonly ISpeechEngine _speechEngine;
   private bool _disposedValue;
   private bool _condition = true;

   internal FluentDebug(ISpeechEngine speechEngine)
   {
      FluentConditions = new FluentConditions(this);
      FluentStatements = new FluentStatements(this);
      _speechEngine = speechEngine;
   }

   #region DebugOverloads

     #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
     #endregion
   public IFluentConditions Assert(bool condition)
   {
      if(Enabled && _condition)
         Debug.Assert(condition);

      _condition = true;
      return FluentConditions;
   }

     #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
     #endregion
   public IFluentConditions Assert(bool condition, Object context)
   {
      if(Enabled && _condition)
         Debug.Assert(condition, context);

      _condition = true;
      return FluentConditions;
   }

     #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
     #endregion
   public IFluentConditions Assert(bool condition, object message)
   {
      if(Enabled && _condition)
         Debug.Assert(condition, message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Assert(bool condition, object message, Object context)
   {
      if(Enabled && _condition)
         Debug.Assert(condition, message, context);
      
      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertFormat(bool condition, string format, params object[] args)
   {
      if(Enabled && _condition)
         Debug.AssertFormat(condition, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertFormat(bool condition, Object context, string format, params object[] args)
   {
      if(Enabled && _condition)
         Debug.AssertFormat(condition, context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Break()
   {
      if(Enabled && _condition)
         Debug.Break();

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions ClearDeveloperConsole()
   {
      if (Enabled && _condition)
         Debug.ClearDeveloperConsole();

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions DrawLine(Vector3 start, Vector3 end, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true)
   {
      if (Enabled && _condition)
      {
         var color = nullableColor ?? Color.white;
         Debug.DrawLine(start, end, color, duration, depthTest);
      }

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions DrawRay(Vector3 start, Vector3 dir, Color? nullableColor,
      float duration = 0.0f, bool depthTest = true)
   {
      if (Enabled && _condition)
      {
         var color = nullableColor ?? Color.white;
         Debug.DrawRay(start, dir, color, duration, depthTest);
      }

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Log(object message)
   {
      if (Enabled && _condition)
         Debug.Log(message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Log(object message, Object context)
   {
      if (Enabled && _condition)
         Debug.Log(message, context);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertion(object message)
   {
      if (Enabled && _condition)
         Debug.LogAssertion(message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertion(object message, Object context)
   {
      if (Enabled && _condition)
         Debug.LogAssertion(message, context);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertionFormat(string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogAssertionFormat(format, args);
      
      _condition = true;
      return FluentConditions;
   }
   
   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogAssertionFormat(Object context, string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogAssertionFormat(context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogError(object message)
   {
      if (Enabled && _condition)
         Debug.LogError(message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogError(object message, Object context)
   {
      if (Enabled && _condition)
         Debug.LogError(message, context);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogErrorFormat(string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogErrorFormat(format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogErrorFormat(Object context, string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogErrorFormat(context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogException(Exception exception)
   {
      if (Enabled && _condition)
         Debug.LogException(exception);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogException(Exception exception, Object context)
   {
      if (Enabled && _condition)
         Debug.LogException(exception, context);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogFormat(format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(Object context, string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogFormat(context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogFormat(LogType logType, LogOption logOptions, Object context, string format, 
      params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogFormat(logType, logOptions, context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarning(object message)
   {
      if (Enabled && _condition)
         Debug.LogWarning(message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarning(object message, Object context)
   {
      if (Enabled && _condition)
         Debug.LogWarning(message, context);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarningFormat(string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogWarningFormat(format, args);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions LogWarningFormat(Object context, string format, params object[] args)
   {
      if (Enabled && _condition)
         Debug.LogWarningFormat(context, format, args);

      _condition = true;
      return FluentConditions;
   }

   #endregion DebugOverloads
   
   #region DebugExtensions

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertNotNull(object obj, object message)
   {
      if(Enabled && _condition)
         // UnityObjects overload the == operator
         if(!(obj is null || obj == null))
            Debug.LogAssertion(message);

      _condition = true;
      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions AssertNotNull(object obj, object message, Object context)
   {
      if(Enabled && _condition)
         // UnityObjects overload the == operator
         if(!(obj is null || obj == null))
            Debug.LogAssertion(message, context);

      _condition = true;
      return FluentConditions;
   }
   
   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Say(string msg, bool clearPreviousMessages = false)
   {
      if(Enabled && _condition)
         _speechEngine?.Say(msg, clearPreviousMessages);

      return FluentConditions;
   }

   #region
#if UNITY_2022_2_OR_NEWER
   [HideInCallstack]
#endif
   #endregion
   public IFluentConditions Execute(Action method)
   {
      if (Enabled && _condition)
         method?.Invoke();

      return FluentConditions;
   }

   public IFluentConditions Enable()
   {
      if(_condition)
         Enabled = true;
      
      return FluentConditions;
   }
   
   public IFluentConditions Disable()
   {
      if(_condition)
         Enabled = false;
      
      return FluentConditions;
   }

   public IFluentConditions PlayAudioClip(AudioClip clip, Vector3 position)
   {
      if(_condition)
         AudioSource.PlayClipAtPoint(clip, position);
      
      return FluentConditions;
   }
   
   #endregion DebugExtensions

   #region DebugContitionals
   
   public IFluentStatements If(bool condition)
   {
      if (Enabled)
      {
         _condition = condition;
         LastCondition = condition;
      }

      return FluentStatements;
   }
   
   public IFluentStatements If(Func<bool> condition)
   {
      if (Enabled)
      {
         _condition = condition();
         LastCondition = _condition;
      }

      return FluentStatements;
   }

   #endregion DebugConditionals
}
}