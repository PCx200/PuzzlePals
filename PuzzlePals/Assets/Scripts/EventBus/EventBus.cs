using System;
using System.Collections.Generic;
using UnityEngine;

//EventBus Class: Publish, Subscribe, Unsubscribe

namespace EventBus
{
    public static class EventBus
    {
        private static Dictionary<Type, Delegate> assignedActions = new(); 
        //Delegate is a kind of action that can be used as both a value and a method

        public static void Publish(EventData data) //Publish event
        {
            Type type = data.GetType();
            //Debug.Log("Raising event of type " + type);
            if (assignedActions.TryGetValue(type, out Delegate existingAction)) //checks for subscribers
            {
                existingAction?.DynamicInvoke(data);
            } else
            {
                Debug.Log("No listeners");
            }
        }

        public static void Subscribe<T>(Action<T> action) where T : EventData
        {
            Type type = typeof(T);

            if (assignedActions.ContainsKey(type))
            {
                assignedActions[type]=Delegate.Combine(assignedActions[type], action); //delegates can t use +=, but use combine
            }
            else
            {
                assignedActions[type] = action;
            }
        }

        public static void Unsubscribe<T>(Action<T> action) where T : EventData
        {
            Type type = typeof(T);

            if (!assignedActions.ContainsKey(type))
            {
                assignedActions[type] = Delegate.Remove(assignedActions[type], action);
            }
        }


    }
}
