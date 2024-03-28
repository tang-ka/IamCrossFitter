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
}

public class WorldManager : ManagerWithStateSubject<WorldManager, MainState>
{
    private SceneHandler sceneHandler = new SceneHandler(
        new Dictionary<MainState, string>()
        {
            { MainState.Loading, "LoadingScene" },
            { MainState.Main, "MainScene" },
        });

    [SerializeField] Stack<MainState> stateStack = new Stack<MainState>();

    public override MainState CurMainState
    {
        get => curState;
        set
        {
            if (curState == value) return;

            if (value != MainState.None && value != MainState.Loading)
            {
                if (value != PreMainState)
                    stateStack.Push(curState);
            }

            if (stateStack.Count > 0)
                preState = stateStack.Peek();
            curState = value;

            SetSceneToState(value);
        }
    }

    public MainState PreMainState => preState;

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
        base.Init();
    }

    public override void ReturnToPreState()
    {
        CurMainState = stateStack.Pop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CurMainState = (curState + 1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ReturnToPreState();
        }
    }
}
