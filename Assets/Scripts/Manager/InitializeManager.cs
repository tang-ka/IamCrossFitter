using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class InitializeManager : ManagerBase<InitializeManager>, IStateObserver<MainState>
{
    float curTime = 0;
    public float Progress { get => (curTime / completeTime) * 100; }

    public Action<bool> onCompleteInitialize;
    public bool isSuccess = false;

    float completeTime = 3;
    double timeOutSeconds = 5;

    public override void Init()
    {
        WorldManager.Instance.AddObserver(this);
        WorldManager.Instance.CurMainState = MainState.Loading;
        base.Init();
    }

    public async void InitializeApplication()
    {
        var timeOutCTS = new CancellationTokenSource();
        timeOutCTS.CancelAfterSlim(TimeSpan.FromSeconds(timeOutSeconds));

        try
        {
            await UniTask.WaitUntil(() =>
            {
                curTime += Time.deltaTime;
                //Debug.Log($"Loading.. : {Progress}%");

                if (curTime > completeTime)
                {
                    curTime = completeTime;
                    isSuccess = true;

                    return true;
                }

                return false;
            }).WithCancellation(timeOutCTS.Token);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == timeOutCTS.Token)
            {
                Debug.Log($"Time out : {timeOutSeconds}[sec]");
                isSuccess = false;
            }
        }

        onCompleteInitialize?.Invoke(isSuccess);

        //WorldManager.Instance.sceneHandler.UnloadScene(MainState.Loading);
        WorldManager.Instance.CurMainState = MainState.Main;
    }

    public void Notify(MainState state)
    {
        if (state.Equals(MainState.Loading))
        {
            InitializeApplication();
        }
    }
}
