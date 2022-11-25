// original contributor: Chad Weisshaar
// source code is originated from: https://chadweisshaar.com/blog/2015/07/02/microsoft-speech-for-unity/
// contributor: Jinki Jung
// source code originated from https://github.com/VirtualityForSafety/UnityWindowsTTS
// current contributor Giannis Akritidis
// fixed bug in dllmain.cpp after mutex lock needed checking for empty list again.
// got rid of warning C4267 in dllmain.cpp
// changed exposed methods names of WindowsTTS.dll to PascalCase 

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

#include <mutex>
#include <list>
#include <thread>

namespace WindowsTTS {

  extern "C" {
    DLL_API void __cdecl InitSpeech();
    DLL_API void __cdecl AddToSpeechQueue(const char* text);
    DLL_API void __cdecl ClearSpeechQueue();
    DLL_API void __cdecl DestroySpeech();
	DLL_API void __cdecl ChangeVoice(int vIdx);
	DLL_API bool __cdecl IsSpeaking();
    DLL_API void __cdecl StatusMessage(char* msg, int msgLen);
  }

  ULONG currentVoiceIndex = 0;
  ULONG targetVoiceIndex = 0;
  bool speaking = false;
  std::mutex theMutex;
  std::list<wchar_t*> theSpeechQueue;
  std::thread* theSpeechThread = nullptr;
  bool shouldTerminate = false;
  wchar_t* condition;
  std::wstring theStatusMessage;
}