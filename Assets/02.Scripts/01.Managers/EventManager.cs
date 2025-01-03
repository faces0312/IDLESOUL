using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine.Events;
using Enums;
using System.ComponentModel;
using UnityEngine;
using System;

public class EventManager : SingletonDDOL<EventManager>
{
    public Dictionary<Channel, List<Delegate>> Events = new Dictionary<Channel, List<Delegate>>();
    public event Action<SoulDB> OnPickUpSoul;

    public void Init()
    {
        
    }

    public void Subscribe(Channel channel, UnityAction listener)
    {
        if(Events?.ContainsKey(channel) == false)
        {
            Events[channel] = new List<Delegate>();
        }

        Events?[channel].Add(listener);
    }

    public void Subscribe<T>(Channel channel, UnityAction<T> listener) where T : IEventObject
    {
        if (Events?.ContainsKey(channel) == false)
        {
            Events[channel] = new List<Delegate>();
        }

        Events?[channel].Add(listener);
    }

    public void Unsubscribe(Channel channel, UnityAction listener)
    {
        if(Events.TryGetValue(channel, out var UnityActions))
        {
            UnityActions.Remove(listener);
        }
    }

    public void Unsubscribe<T>(Channel channel, UnityAction<T> listener) where T : IEventObject
    {
        if (Events.TryGetValue(channel, out var UnityActions))
        {
            UnityActions.Remove(listener);
        }
    }

    public void Publish<T>(Channel channel, T parameter)
    {
        if (Events?.ContainsKey(channel) == false) return;

        var UnityActions = Events?[channel];
        if (UnityActions == null) return;

        foreach (var action in UnityActions)
        {
            if(action.GetType() == typeof(UnityAction<T>))
            {
                ((UnityAction<T>)action)(parameter);
            }
        }
    }

    public void OnPickup(Soul soul)
    {
        OnPickUpSoul?.Invoke(DataManager.Instance.SoulDB.GetByKey(soul.ID));
    }
}