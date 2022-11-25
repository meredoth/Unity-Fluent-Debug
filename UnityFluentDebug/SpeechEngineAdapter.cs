namespace UnityFluentDebug
{
public class SpeechEngineAdapter : ISpeechEngine
{
   public bool SpeechEngineInitialized => SpeechEngine.Instance.SpeechEngineInitialized;

   public void Init() => SpeechEngine.Instance.Init();

   public void Say(string msg, bool clearPreviousMessages = false) =>
      SpeechEngine.Instance.Say(msg, clearPreviousMessages);
   
   public bool IsSpeaking() => SpeechEngine.Instance.IsSpeaking();

   public void CleanUp() => SpeechEngine.Instance.CleanUp();

   public void SelectVoice(int idx) => SpeechEngine.Instance.SelectVoice(idx);
}
}
