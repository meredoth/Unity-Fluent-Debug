namespace UnityFluentDebug
{
public interface ISpeechEngine
{
   bool SpeechEngineInitialized { get; }
   void Init();
   void Say(string msg, bool clearPreviousMessages = false);
   bool IsSpeaking();
   void CleanUp();
   void SelectVoice(int idx);
}
}