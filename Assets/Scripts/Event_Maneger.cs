using System;
using System.Collections.Generic;

public static class Event_Maneger
{
    public static Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = delegate { };
        }
        eventDictionary[eventName] += listener;
    }

    // Unsubscribe
    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    // Trigger Event
    public static void Trigger(string eventName, object param = null)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke(param);
        }

    }

}
