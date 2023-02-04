using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public static class GenericEventManager 
{
	private static Dictionary<Type, List<EventListenerBase>> _subscribersList;

	static GenericEventManager()
	{
		_subscribersList = new Dictionary<Type, List<EventListenerBase>>();
	}

	public static void AddListener<GEvent>(EventListener<GEvent> listener) where GEvent : struct
	{
		Type eventType = typeof(GEvent);

		if (!_subscribersList.ContainsKey(eventType))
		{
			_subscribersList[eventType] = new List<EventListenerBase>();
		}

		if (!SubscriptionExists(eventType, listener))
		{
			_subscribersList[eventType].Add(listener);
		}
	}

	public static void RemoveListener<GEvent>(EventListener<GEvent> listener) where GEvent : struct
	{
		Type eventType = typeof(GEvent);

		if (!_subscribersList.ContainsKey(eventType))
		{
			return;

		}

		List<EventListenerBase> subscriberList = _subscribersList[eventType];


		for (int i = subscriberList.Count - 1; i >= 0; i--)
		{
			if (subscriberList[i] == listener)
			{
				subscriberList.Remove(subscriberList[i]);

				if (subscriberList.Count == 0)
				{
					_subscribersList.Remove(eventType);
				}

				return;
			}
		}

	}

	public static void TriggerEvent<GEvent>(GEvent newEvent) where GEvent : struct
	{
		List<EventListenerBase> list;

		if (!_subscribersList.TryGetValue(typeof(GEvent), out list))
			return;

		for (int i = list.Count - 1; i >= 0; i--)
		{
			(list[i] as EventListener<GEvent>).OnGEvent(newEvent);
		}
	}

	private static bool SubscriptionExists(Type type, EventListenerBase receiver)
	{
		List<EventListenerBase> receivers;

		if (!_subscribersList.TryGetValue(type, out receivers)) return false;

		bool exists = false;

		for (int i = receivers.Count - 1; i >= 0; i--)
		{
			if (receivers[i] == receiver)
			{
				exists = true;
				break;
			}
		}

		return exists;
	}


}

public static class EventRegister
{
	public delegate void Delegate<T>(T eventType);

	public static void EventStartListening<EventType>(this EventListener<EventType> caller) where EventType : struct
	{
		GenericEventManager.AddListener<EventType>(caller);
	}

	public static void EventStopListening<EventType>(this EventListener<EventType> caller) where EventType : struct
	{
		GenericEventManager.RemoveListener<EventType>(caller);
	}
}

public interface EventListenerBase { };

public interface EventListener<T> : EventListenerBase
{
	void OnGEvent(T eventType);
}

public class EventListenerWrapper<TOwner, TTarget, TEvent> : EventListener<TEvent>, IDisposable
		where TEvent : struct
{
	private Action<TTarget> _callback;

	private TOwner _owner;
	public EventListenerWrapper(TOwner owner, Action<TTarget> callback)
	{
		_owner = owner;
		_callback = callback;
		RegisterCallbacks(true);
	}

	public void Dispose()
	{
		RegisterCallbacks(false);
		_callback = null;
	}

	protected virtual TTarget OnEvent(TEvent eventType) => default;
	public void OnGEvent(TEvent eventType)
	{
		var item = OnEvent(eventType);
		_callback?.Invoke(item);
	}

	private void RegisterCallbacks(bool b)
	{
		if (b)
		{
			this.EventStartListening<TEvent>();
		}
		else
		{
			this.EventStopListening<TEvent>();
		}
	}
}
