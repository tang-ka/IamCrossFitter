using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    private Dictionary<MainState, string> sceneDic;

    public SceneHandler(Dictionary<MainState, string> dic)
    {
        sceneDic = dic;
    }

    public async UniTask LoadScene(MainState state, LoadSceneMode mode, Action finishCallback = null)
    {
        if (!sceneDic.ContainsKey(state) || IsSceneLoaded(sceneDic[state]))
        {
            finishCallback();
            return;
        }

        await SceneManager.LoadSceneAsync(sceneDic[state], mode);
        finishCallback?.Invoke();
    }

    public async UniTask LoadScene(string sceneName, LoadSceneMode mode, Action finishCallback = null)
    {
        await SceneManager.LoadSceneAsync(sceneName, mode);
        finishCallback?.Invoke();
    }

    public void UnloadScene(MainState state, Action finishCallback = null)
    {
        if (sceneDic.ContainsKey(state) && IsSceneLoaded(sceneDic[state]))
            SceneManager.UnloadSceneAsync(sceneDic[state]).completed += FinishUnload;

        void FinishUnload(AsyncOperation op)
        {
            if (op.isDone)
            {
                finishCallback?.Invoke();
                op.completed -= FinishUnload;
            }
        }
    }

    public async UniTask UnloadScene(string sceneName, Action finishCallback = null)
    {
        if (IsSceneLoaded(sceneName))
            await SceneManager.UnloadSceneAsync(sceneName);

        finishCallback?.Invoke();
    }

    private bool IsSceneLoaded(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }
}
