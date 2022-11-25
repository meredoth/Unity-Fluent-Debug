using UnityEditor;

namespace UnityFluentDebug
{
public static class FluentDebugFactory
{
  public static bool IsSpeechEngineInitialized
  {
    get
    {
#if !DISABLE_FLUENT_DEBUG
      return _speechEngine.SpeechEngineInitialized;
#else
      return false;
#endif
    }
  }

  public static bool IsSpeaking
  {
    get
    {
#if !DISABLE_FLUENT_DEBUG
      return _speechEngine.IsSpeaking();
#else
      return false;
#endif
    }
  }

#if !DISABLE_FLUENT_DEBUG
  private static readonly ISpeechEngine _speechEngine = new SpeechEngineAdapter();
#else
  private static readonly ISpeechEngine _speechEngine = null;
#endif
  
  public static FluentDebug Create() => new(_speechEngine);

  public static void InitializeSpeechEngine()
  {
#if !DISABLE_FLUENT_DEBUG
    _speechEngine.Init();

#if UNITY_EDITOR
    EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
#endif
  }
   
  public static void SelectVoice(int idx)
  {
#if !DISABLE_FLUENT_DEBUG
     _speechEngine.SelectVoice(idx);
#endif
  }

  public static void UnloadSpeechEngine()
  {
#if !DISABLE_FLUENT_DEBUG
     _speechEngine.CleanUp();
#endif
  }
  
#if UNITY_EDITOR
  private static void OnPlayModeStateChanged(PlayModeStateChange state)
  {
#if !DISABLE_FLUENT_DEBUG
    if (state == PlayModeStateChange.EnteredEditMode)
      _speechEngine.CleanUp();
#endif
  }
#endif
}
}
