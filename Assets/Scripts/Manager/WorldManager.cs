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

public class WorldManager : ManagerWithState<WorldManager, MainState>
{
    SceneHandler sceneHandler = new SceneHandler(
        new Dictionary<MainState, string>()
        {
            { MainState.Loading, "LoadingScene" },
            { MainState.Main, "MainScene" },
            { MainState.PersonalRecord, "PersonalRecordScene" }
        });

    [SerializeField] Stack<MainState> stateStack = new Stack<MainState>();

    public override MainState CurMainState
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

    public MainState PreMainState => stateStack.Peek();

    private async void SetSceneToState(MainState value)
    {
        await sceneHandler.LoadScene(value, LoadSceneMode.Additive, 
            finishCallback: () =>
            {
                NotifyChangeState(value);
            });
    }

    public override void Init()
    {
        CurMainState = MainState.Loading;
        base.Init();
    }

    public override void ReturnToPreState()
    {
        CurMainState = stateStack.Pop();
    }
}
