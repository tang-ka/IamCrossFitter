using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : ManagerBase<InitializeManager>, IStateObserver<MainState>
{
    float curTime = 0;
    public float Progress { get => (curTime / 2) * 100; }

    public Action<bool> onFinishInitialize;

    public override void Init()
    {
        WorldManager.Instance.AddObserver(this);
        base.Init();
    }

    public void InitializeApplication()
    {
        UniTask.WaitUntil(() =>
        {
            curTime += Time.deltaTime;
            Debug.Log($"Loading.. : {Progress}%");

            if (curTime > 2)
            {
                curTime = 0;
                onFinishInitialize?.Invoke(true);
                return true;
            }
            
            return false;
        });
    }

    public void Notify(MainState state)
    {
        if (state.Equals(MainState.Loading))
        {
            InitializeApplication();
        }
    }
}
