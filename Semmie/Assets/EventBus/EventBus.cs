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
public struct UnlockPushEvent : IGameEvent { }
public struct LockPullEvent : IGameEvent { }
public struct UnlockPullEvent : IGameEvent { }
public struct OnDamageEvent : IGameEvent { }
public struct OnDeathEvent : IGameEvent { }
public struct OnReviveEvent : IGameEvent { }
public struct OnCoinPickup : IGameEvent{ public int coinNum; }
public struct AllCoinsAquired: IGameEvent{  }

