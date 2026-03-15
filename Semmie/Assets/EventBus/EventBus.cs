using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus<T> where T : IGameEvent
{
    
    public delegate void Event(T evt);

    public static event Event OnEvent;

    public static void Raise([CanBeNull] T evt) => OnEvent?.Invoke(evt);


}

public struct LockPushEvent : IGameEvent { }

