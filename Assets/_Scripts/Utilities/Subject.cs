using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    // a collection of all the observers of this subject
    private List<IObserver> _observers = new List<IObserver>();
    // add the observer to the subject's collection
    public void AddObserver(IObserver observer) 
    {
        _observers.Add(observer);
    }
    // remove the observer from the subject's collection
    public void RemoveObserver(IObserver observer) 
    {
        _observers.Remove(observer);
    }
    // notify each observer that an event has occurred
    protected void NotifyObservers(PlayerActions action) {
        // _observers.ForEach((_observer) => {
        //     _observer.OnNotify(action) ;
        // });
        //Obviuosly, your do something code is trying to modify the collection.
        // It's generally a bad idea to modify the collection while iterating on it.

        foreach(var _observer in _observers.ToArray())  //create copy of list to modify copy
        {
            _observer.OnNotify(action);
        }
    }
}
