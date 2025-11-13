using System;
using UnityEngine;

/***
*In Unity, when you declare something as static, it lives for the lifetime of the app, not just the scene. 
Even if you reload or switch scenes, the static class (and any static variables or events it holds) 
stay in memory, they’re not automatically destroyed when the scene unloads.

If a new scene is loaded and all the subscriber are distroyed, there is a risk that the event invocation list
keeps a reference to those null pointers, creating an exception.
Or, that the list fills up with ghost listeners.

*/
namespace Broadcaster
{
    public static class BroadcastMessenger
    {
        //* Actions groups for GamePlay
        public static Action GamePlayStarted;
        public static Action GamePlayEnded;
        public static Action GamePlayPaused;

        //* Provide menthods to trigger the events
        // You may use: public static void GamePlayStart() => GamePlayStarted?.Invoke();
        public static void GamePlayStart()
        {
            GamePlayStarted?.Invoke();
        }
        public static void GamePlayEnd()
        {
            GamePlayEnded?.Invoke();
        }
        public static void GamePlayPause()
        {
            GamePlayPaused?.Invoke();
        }
        
    }
}
    
