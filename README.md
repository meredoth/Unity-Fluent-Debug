# Unity Fluent Debug

Unity Fluent Debug is a wrapper over theUnity Debug with some additional functionality using fluent syntax.

## Getting Started

Copy UnityFluentDebug folder to you Unity Assets folder.

If you want speech support, build the dll from the WindowsTTS folder and add the WindowsTTS.dll to your Unity Assets folder.

## Simple Usage

Add the namespace with the following using statement

```cs
using UnityFluentDebug;
```

The FluentDebugFactory is your entry point.

Create a new fluent debugger with:

```cs
private readonly FluentDebug FDebug = FluentDebugFactory.Create();
```

then use FDebug as you would use the Debug class in unity:

```cs
FDebug.Log("Hello World!");
```

### Syntax

Unity Fluent Debug supports fluent syntax, you can chain statements and conditions:

```cs
FDebug.If(x > 69).Log("x is grater than 69.");

FDebug.If(x > 4).Log("x is grater than 4.")
      .If(y < 2).LogWarning("y is less than 2.");

FDebug.Log("Waiting at the inn")
      .If(barbarian.IsNear).Log("A barbarian is near.")
      .AndIf(barbarian.AttackMode == AttackMode.Berserk).LogWarning("Watch out.")
      .AndIf(barbarian.XPToNextLevel < 5f).LogError("We are going to die!").And().Say("Run for your lives.");
```

# Advanced usage

Unity fluent debug supports some extra functionality like speech or avoiding the evaluation of conditions when you have disabled the fluentDebugger.


## The FluentDebugFactory static class

The FluentDebugFactory static class is your entry point. You can create different debuggers by calling the ```FluentDebugFactory.Create()``` method:

```cs
FluentDebug myDebug = FluentDebugFactory.Create();
```

if you have compiled the WindowsTTS.dll the FluentDebugFactory is responsible for controlling the speech engine through the following methods:

### (WARNING)

The WindowsTTs.dll provides speech only for the Windows x64 systems, it requires:

* MS language pack: [https://support.microsoft.com/en-us/help/14236/language-packs](https://support.microsoft.com/en-us/help/14236/language-packs)
* MS TTS package: [https://support.office.com/en-us/article/how-to-download-text-to-speech-languages-for-windows-10-d5a6b612-b3ae-423f-afa5-4f6caf1ec5d3](https://support.office.com/en-us/article/how-to-download-text-to-speech-languages-for-windows-10-d5a6b612-b3ae-423f-afa5-4f6caf1ec5d3)

uses modified code from the [The UnityWindowsTTS project](https://github.com/VirtualityForSafety/UnityWindowsTTS).


```cs
FluentDebugFactory.InitializeSpeechEngine();
```

Initializes the speech engine. If you want speech this has to be called before using the ```Say()``` method.


```cs
FluentDebugFactory.UnloadSpeechEngine();
```

Unloads the speech engine. You don't need to call this as it gets automatically called every time you exit the play mode.


```cs
FluentDebugFactory.SelectVoice(int idx);
```

changes the voice of the speech engine, depending on the voices you have installed on your machine.


```cs
bool FluentDebugFactory.IsSpeaking;
```

This property returns true while the speech engine is speaking.


```cs
bool FluentDebugFactory.IsSpeechEngineInitialized;
```
This property returns true if the speech engine has been successfully initialized.

#

## The Debuggers

After you have created a debugger with the ```FluentDebugFactory.Create()``` method you can call all the usual methods from the Unity Debug class including the ```isDebugBuild``` , ```unityLogger``` and ```developerConsoleVisible``` commands that call the same Unity Debug methods.

There is also the ```Enabled``` boolean property which can be used to enable/disable only a specific debugger. For example you may have created two debuggers the ```myAIdebug``` for your AI system debugging and the ```myStatSystemDebug``` for your StatSystem Debugging. The command ```myAIdebug.Enabled = false``` will only disable the ```myAIdebug``` debugger, leaving only the ```myStatSystemDebug``` statements to be printed to the console (or heard, if you use the ```Say()``` statement and the speech engine is initialized).

The Debuggers offer some extra functionality that works well with the fluent syntax.

There are two groups of methods that you can use: statements and conditions. The statements include all the usual methods of the Unity Debug class plus a few extra methods.

You can start with either a statement or a condition, but after that you create a chain that alternates between statements and conditions.

### Extra statements

```cs
AssertNotNull(object obj, object message)
AssertNotNull(object obj, object message, Object context)
```

if the obj is null calls a LogAssertion method.

```cs
Say(string msg, bool clearPreviousMessages = false)
```

If the speak engine is initialized, instead of writing in the debug console you will hear the msg through your speakers. the clearPreviousMessages boolean clears the queue of any previous messages so that you can hear the message the time this method is invoked, useful if you have a lot of messages that play, but you need to hear a specific message the time the say method is called.

### Conditions

```cs
If(bool condition)
If(Func<bool> condition)
```

The next statement will be called if the condition is true. The overload ```If(Func<bool> condition)``` is useful to avoid the performance and boxing costs of expensive boolean expressions when you have disabled the debugger.

In Unity the ```if(somethingExpensive) Debug...``` and in the Fluent Debugger the ```myDebug.If(somethingExpensive).Debug...``` cost performance for no reason when the debug is disabled because you pay the cost of calculating the ```somethingExpensive``` in the if statements anyway. With the ```If(Func<bool> condition)``` overload: ```If(() => {return somethingExpensive;})``` the boolean expression ```somethingExpensive``` is not calculated if you have disabled the debugger.

```cs
And()
```

Use the ```And()``` method to add to statements together so both of them are only executed if the ```If()``` expression before them was true. For example:

```cs
myDebug.If( people > 420 ).LogWarning("Hello World").And().Say("Hello Everyone");
```

will write a warning in the console with the message ```Hello World``` and you will hear ```Hello everyone``` only if the ```people``` variable is greater than 420.

```cs
AndIf(bool condition)
AndIf(Func<bool> condition)
```

The ```AndIf()``` method adds a condition that executes the following statement only if the previous condition was true. For example:

```cs
myDebug.If(true).Log("citious").AndIf(true).Log("altius").AndIf(true).Log("fortius");
```

will print to the console: 

```
citius 
altius 
fortius
```
and

```cs
myDebug.If(true).Log("citious").AndIf(false).Log("altius").AndIf(true).Log("fortius");
```

will print to the console:

```
citius 
```

but

```cs
myDebug.If(true).Log("citious").If(false).Log("altius").If(true).Log("fortius");
```

will print to the console:

```
citius 
fortius
```

the overload ```AndIf(Func<bool> condition)``` serves the same purpose as the ```If(Func<bool> condition)``` overload.

If the ```AndIf()``` methods are called without an ```If()``` method before them they behave as the ```If()``` methods.

## Preprocessor Directives

The DISABLE_FLUENT_DEBUG preprocessor directive added to project settings -> scripting define symbols in Unity will disable all Debuggers.

## Unity console click

The Unity Fluent Debug uses the ```[HideInCallstack]``` attribute so that a double click in the unity console will take you to your own script that calls the debug command. This attribute is supported only in versions of Unity 2022.2 or newer.

In previous versions, if you want when you double click the unity console to be taken to your script instead in one of the Unity Fluent Debug scripts, compile the project in a DLL and put it in your assets folder.


## License

This project is licensed under the [Apache-2.0](LICENSE.md)
License - see the [LICENSE.md](LICENSE.md) file for
details.
()
This project uses modified code from [The UnityWindowsTTS project](https://github.com/VirtualityForSafety/UnityWindowsTTS) by [Virtuality for Safety](https://github.com/VirtualityForSafety)

The [The UnityWindowsTTS project](https://github.com/VirtualityForSafety/UnityWindowsTTS) is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for
details.

## Acknowledgments

The Speech Engine in this project is a modified version of the [https://github.com/VirtualityForSafety/UnityWindowsTTS](https://github.com/VirtualityForSafety/UnityWindowsTTS) by [Virtuality for Safety](https://github.com/VirtualityForSafety) which is based on code from [Chad Weisshaar](https://chadweisshaar.com/blog/)

This source code was originally from [here](https://chadweisshaar.com/blog/2015/07/02/microsoft-speech-for-unity/)
