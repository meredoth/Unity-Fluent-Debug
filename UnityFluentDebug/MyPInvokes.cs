using System.Runtime.InteropServices;

namespace UnityFluentDebug
{
public static class MyPInvokes
{
   [DllImport("WindowsTTS")]
   public static extern void InitSpeech();

   [DllImport("WindowsTTS")]
   public static extern void DestroySpeech();

   [DllImport("WindowsTTS")]
   public static extern void AddToSpeechQueue(string s);

   [DllImport("WindowsTTS")]
   public static extern void ClearSpeechQueue();

   [DllImport("WindowsTTS")]
   public static extern void ChangeVoice(int vIdx);
        
   [DllImport("WindowsTTS")]
   public static extern bool IsSpeaking();
}
}