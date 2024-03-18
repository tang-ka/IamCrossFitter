using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public enum MainState
{
    None,
    Loading,
    Main,
    PersonalRecord
}

public class WorldManager : SingletonMonoBehaviour<WorldManager>, IStateSubject<MainState>
{
    SceneHandler sceneHandler = new SceneHandler(
        new Dictionary<MainState, string>()
        {
            { MainState.Loading, "LoadingScene" },
            { MainState.Main, "MainScene" },
            { MainState.PersonalRecord, "PersonalRecordScene" }
        });

    List<IStateObserver<MainState>> observers = new List<IStateObserver<MainState>>();

    [SerializeField] MainState curState = MainState.None;
    [SerializeField] Stack<MainState> stateStack = new Stack<MainState>();

    public MainState CurMainState
    {
        get => curState;
        set
        {
            if (curState == value) return;

            if (value != MainState.None || value != MainState.Loading)
                stateStack.Push(curState);
            
            curState = value;
            SetSceneToState(value);
        }
    }

    private async void SetSceneToState(MainState value)
    {
        await sceneHandler.LoadScene(value, LoadSceneMode.Additive);
    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        CurMainState = MainState.Loading;
    }

    public MainState PreMainState => stateStack.Peek();

    public void ReturnToPrestate()
    {
        CurMainState = stateStack.Pop();
    }

    public void AddObserver(IStateObserver<MainState> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IStateObserver<MainState> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyChangeStat(MainState state)
    {
        foreach (var observer in observers)
            observer.Notify(state);
    }
}
