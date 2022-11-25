// original contributor: Chad Weisshaar
// source code is originated from: https://chadweisshaar.com/blog/2015/07/02/microsoft-speech-for-unity/
// contributor: Jinki Jung
// source code originated from https://github.com/VirtualityForSafety/UnityWindowsTTS
// current contributor Giannis Akritidis
// fixed bug in dllmain.cpp after mutex lock needed checking for empty list again.
// got rid of warning C4267 in dllmain.cpp
// changed way to get string length with int len = MultiByteToWideChar(CP_UTF8, 0, text, -1, NULL, 0);
// removed some unused code
// added initialization of currentVoiceIndex and TargetVoiceIndex to zero in Init function

#pragma warning(disable: 4996)

#include "pch.h"
#include <atlbase.h>
#include "WindowsTTS.h"

#include <sapi.h>
#include <sphelper.h>  

namespace WindowsTTS 
{
	void changeVoiceByIndex(CComPtr<ISpObjectToken> cpVoiceToken, CComPtr<IEnumSpObjectTokens> cpEnum, CComPtr<ISpVoice> cpVoice, ULONG ulCount) 
    {
		if (targetVoiceIndex != currentVoiceIndex) 
        {
			// Enumerate the available voices.
			HRESULT hr = SpEnumTokens(SPCAT_VOICES, NULL, NULL, &cpEnum);

			if (SUCCEEDED(hr))
			{
				// Get the number of voices.
				hr = cpEnum->GetCount(&ulCount);
			}

			targetVoiceIndex = targetVoiceIndex % ulCount;
			currentVoiceIndex = ulCount;
			// Obtain a list of available voice tokens, set
			// the voice to the token, and call Speak.
			while (SUCCEEDED(hr) && targetVoiceIndex != currentVoiceIndex)
			{
				cpVoiceToken.Release();

				if (SUCCEEDED(hr))
				{
					hr = cpEnum->Next(1, &cpVoiceToken, NULL);
				}

				if (SUCCEEDED(hr))
				{
					hr = cpVoice->SetVoice(cpVoiceToken);
				}

				currentVoiceIndex = (currentVoiceIndex - 1) % ulCount;
			}
		}
	}

  void speechThreadFunc()
  {
	  // Declare local identifiers:
	  HRESULT                        hr = S_OK;
	  CComPtr<ISpObjectToken>        cpVoiceToken;
	  CComPtr<IEnumSpObjectTokens>   cpEnum;
	  CComPtr<ISpVoice>              cpVoice;
	  ULONG                          ulCount = 0;
	  speaking = false;

	  if (FAILED(::CoInitializeEx(NULL, COINITBASE_MULTITHREADED)))
	  {
		  theStatusMessage = L"Failed to initialize COM for Voice.";
		  return;
	  }

	  // Create the SAPI voice.
	  hr = cpVoice.CoCreateInstance(CLSID_SpVoice);

	  if (!SUCCEEDED(hr))
	  {
		  LPSTR pText = 0;

		  ::FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			  NULL, hr, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), pText, 0, NULL);
		  LocalFree(pText);
		  theStatusMessage = L"Failed to create Voice instance.";
		  return;
	  }
    
    theStatusMessage = L"Speech ready.";

    SPVOICESTATUS voiceStatus;
    wchar_t* priorText = nullptr;
    while (!shouldTerminate)
    {
		changeVoiceByIndex(cpVoiceToken, cpEnum, cpVoice, ulCount);
		
	cpVoice->GetStatus(&voiceStatus, NULL);
      if (voiceStatus.dwRunningState == SPRS_IS_SPEAKING)
      {
		speaking = true;
        if (priorText == nullptr)
          theStatusMessage = L"Error: SPRS_IS_SPEAKING but text is NULL";
        else
        {
          theStatusMessage = L"Speaking: ";
          theStatusMessage.append(priorText);
          if (!theSpeechQueue.empty())
          {
            theMutex.lock();
                if (!theSpeechQueue.empty() && lstrcmpW(theSpeechQueue.front(), priorText) == 0)
                {
                    delete[] theSpeechQueue.front();
                    theSpeechQueue.pop_front();
                }
            theMutex.unlock();
          }
        }
      }
      else
      {
		speaking = false;
        theStatusMessage = L"Waiting.";
        if (priorText != NULL)
        {
          delete[] priorText;
          priorText = NULL;
        }
        if (!theSpeechQueue.empty())
        {
          theMutex.lock();
          if (!theSpeechQueue.empty())
          {
              priorText = theSpeechQueue.front();
              theSpeechQueue.pop_front();
          }
          theMutex.unlock();
		  cpVoice->Speak(priorText, SPF_IS_XML | SPF_ASYNC, NULL);
        }
      }
      Sleep(50);
    }
	cpVoice->Pause();

    theStatusMessage = L"Speech thread terminated.";
  }

  bool IsSpeaking() 
  {
	  return speaking;
  }

  void AddToSpeechQueue(const char* text)
  {
    if (text)
    { 
      int len = MultiByteToWideChar(CP_UTF8, 0, text, -1, NULL, 0);
      wchar_t* wText = new wchar_t[len];
 
      ::MultiByteToWideChar(CP_UTF8, 0, text, -1, wText, len);

      theMutex.lock();
      theSpeechQueue.push_back(wText);
      theMutex.unlock(); 
    }
  }

  void ClearSpeechQueue()
  {
    theMutex.lock();
    theSpeechQueue.clear();
    theMutex.unlock();
  }

  void ChangeVoice(int vIdx) 
  {
	  targetVoiceIndex = vIdx;
  }

  void InitSpeech()
  { 
    currentVoiceIndex = 0;
    targetVoiceIndex = 0;
    shouldTerminate = false;
    if (theSpeechThread != nullptr)
    {
      theStatusMessage = L"Windows Voice thread already started.";
      return;
    }
    theStatusMessage = L"Starting Windows Voice.";
    theSpeechThread = new std::thread(WindowsTTS::speechThreadFunc);
  }

  void DestroySpeech()
  {
    if (theSpeechThread == nullptr)
    {
      theStatusMessage = L"Speach thread already destroyed or not started.";
      return;
    }
    theStatusMessage = L"Destroying speech.";
    shouldTerminate = true;
    theSpeechThread->join();
    theSpeechQueue.clear();
    delete theSpeechThread;
    theSpeechThread = nullptr;
    CoUninitialize();
    theStatusMessage = L"Speech destroyed.";
  }

  void StatusMessage(char* msg, int msgLen)
  {
    size_t count;
    wcstombs_s(&count, msg, msgLen, theStatusMessage.c_str(), msgLen);
  }
}


BOOL APIENTRY DllMain(HMODULE, DWORD ul_reason_for_call, LPVOID)
{
  switch (ul_reason_for_call)
  {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
     break;
  }
  
  return TRUE;
}