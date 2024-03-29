using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
{
    [SerializeField] protected bool isInit;
    public bool IsInit => isInit;

    protected void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        isInit = true;
    }

    public virtual void Deinit() { }
}


public abstract class ManagerWithStateSubject<T, U> : ManagerBase<T>, IStateSubject<U> 
    where T : MonoBehaviour where U : System.Enum
{
    protected List<IStateObserver<U>> observers = new List<IStateObserver<U>>();
    [SerializeField] protected U curState;
    [SerializeField] protected U preState;

    public virtual U CurMainState
    {
        get => curState;
        set
        {
            if (curState.Equals(value)) return;

            preState = curState;
            curState = value;

            NotifyChangeState(value);
        }
    }

    public virtual void ReturnToPreState()
    {
        CurMainState = preState;
    }

    #region State Subject
    public void AddObserver(IStateObserver<U> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IStateObserver<U> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyChangeState(U state)
    {
        observers.ForEach((observer) => observer.Notify(state));
    } 
    #endregion
}