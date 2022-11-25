// REQUIRES WindowsTTS.dll
// Compile WindowsTTS.dll from the WindowsTTS folder
// Usable ONLY IN WINDOWS x64 SYSTEMS
// original contributor for code in WindowsTTS.dll: Chad Weisshaar
// source code in WindowsTTS.dll is originated from: https://chadweisshaar.com/blog/2015/07/02/microsoft-speech-for-unity/
// contributor: Jinki Jung
// source code originated from https://github.com/VirtualityForSafety/UnityWindowsTTS
// current contributor Giannis Akritidis
// fixed bug in dllmain.cpp after mutex lock needed checking for empty list again.
// got rid of warning C4267 in dllmain.cpp
// Changed exposed method names of WindowsTTS.dll to PascalCase 
// changed way to get string length in dllmain.cpp with int len = MultiByteToWideChar(CP_UTF8, 0, text, -1, NULL, 0);
// removed some unused methods from dllmain.cpp
// added initialization of currentVoiceIndex and TargetVoiceIndex to zero in InitSpeech function of dllmain.cpp

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityFluentDebug.MyPInvokes;

namespace UnityFluentDebug
{
internal class SpeechEngine
{
    public bool SpeechEngineInitialized { get; private set; }

    private static SpeechEngine _instance;
    public static SpeechEngine Instance => _instance ??= new SpeechEngine();
    
    public void Init()
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64
        if (SpeechEngineInitialized) return;
        
        try
        {
            InitSpeech();
            ClearSpeechQueue();
            SpeechEngineInitialized = true;
        }
        catch(Exception ex)
        {
            Debug.LogAssertion($"Cannot initialize speech engine. {Environment.NewLine} {ex}");
        }
#endif
    }
    
    public void Say(string msg, bool clearPreviousMessages = false)
    {
        if(SpeechEngineInitialized)
            Speak(msg, clearPreviousMessages);
        else
            Debug.LogWarning($"Speech engine isn't initialized! {Environment.NewLine} {GetMethodName()} will not run!");
    }
    
    public bool IsSpeaking()
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64
        var isSpeaking = false;
        if (SpeechEngineInitialized)
        {
            try
            {
                isSpeaking = MyPInvokes.IsSpeaking();
            }
            catch (Exception ex)
            {
                Debug.LogAssertion($"Could not retrieve speech engine in use information. {Environment.NewLine} {ex}");
                isSpeaking = false;
            }
        }
        else
            Debug.LogWarning($"Speech engine isn't initialized! {Environment.NewLine} {GetMethodName()} will not run!");

        return isSpeaking;
#endif
    }

    public void CleanUp()
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64
        if (SpeechEngineInitialized)
        {
            try
            {
                DestroySpeech();
                SpeechEngineInitialized = false;
            }
            catch(Exception ex)
            {
                Debug.LogAssertion($"Could not unload speech engine. {Environment.NewLine} {ex}");
            }
        }
        else
            Debug.LogWarning($"Speech engine isn't initialized! {Environment.NewLine} {GetMethodName()} will not run!");
#endif
    }
    
    public void SelectVoice(int idx)
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64
        if (SpeechEngineInitialized)
        {
            try
            {
                ChangeVoice(idx);
            }
            catch (Exception ex)
            {
                Debug.LogAssertion($"Could not execute change of voice in speech engine. {Environment.NewLine} {ex}");
            }
        }
        else
            Debug.LogWarning($"Speech engine isn't initialized! {Environment.NewLine} {GetMethodName()} will not run!");
#endif
    }
    
    [AOT.MonoPInvokeCallback(typeof(SpeechEngine))]
    private void Speak(string msg, bool clearPreviousMessages)
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64
        try
        {
            if (clearPreviousMessages)
                ClearSpeechQueue();

            AddToSpeechQueue(msg);
        }
        catch (Exception ex)
        {
            Debug.LogAssertion($"Could not use speech engine for speech. {Environment.NewLine} {ex}");
        }
#endif
    }
    
    private string GetMethodName([CallerMemberName] string methodName = null) => methodName;
}
}
