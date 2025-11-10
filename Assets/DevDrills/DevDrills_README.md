# Dev Drills

A collections of daily dev drills to master Unity, C# and Game Dev concepts.

## Session 2024-10-10

About Events in Unity, recall the pattern:

```C#
// Declaration
UnityEvent myEvent;
UnityEvent<T> myEventOfTypeT;

// Initiallization, note that this will erase any manual setting in the Editor
myEventOfTypeT = new UnityEvent<T>();

// Addition of event handlers, note that typed events need that the event handler's signature
// contain the event type in the parameters
void EvenTypedHandler(<T> event){} // --> In the class that needs to be notified
myEventOfTypeT.AddEventListener(EvenTypedHandler);

void EvenHandler(){} // --> In the class that needs to be notified
myEventOfType.AddEventListener(EvenHandler);

// Invocation
myEventOfTypeT.Invoke(<T> variable);
myEvent.Invoke(); // --> Ideally, all invocations should happen in the same place

```