using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class UIPageController : UIController
{
    protected Dictionary<string, UIPage> pageDic = new Dictionary<string, UIPage>();

    #region Mono
    private void Awake() { Init(); }
    private void OnDestroy() { Reset(); }
    #endregion

    #region MyRegion
    public bool IsPageAvailable(string pageID)
    {
        return pageDic.ContainsKey(pageID);
    }

    public void SetVisible(bool isVisible)
    {
        GetComponent<Canvas>().enabled = isVisible;
    }

    public UIPage GetPage(string pageID)
    {
        if (IsPageAvailable(pageID))
            return pageDic[pageID];

        //return null;
        throw new NullReferenceException("That page is unavailable.");
    }

    //public virtual UIPage Openpage(string pageID, pageOno)
    #endregion

    #region UIController
    protected override async void Init()
    {
        IsInit = false;

        foreach (var page in GetComponentsInChildren<UIPage>())
        {
            page.Init();
            pageDic.Add(page.TypeID, page);
        }

        await UniTask.WaitUntil(() =>
        {
            foreach (var page in pageDic.Values)
            {
                if (!page.IsInit)
                    return false;
            }
            return true;
        });

        IsInit = true;
    }

    public override void Reset()
    {
        foreach (var page in pageDic.Values)
            page.Reset();

        pageDic.Clear();
    }
    #endregion
}
