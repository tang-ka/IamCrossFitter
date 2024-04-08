using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class UIPageController_old : UIController
{
    protected Dictionary<string, UIPage_old> pageDic = new Dictionary<string, UIPage_old>();

    #region Mono
    private void Awake() { Init(); }
    private void OnDestroy() { Deinit(); }
    #endregion

    #region Methods
    public bool IsPageAvailable(string pageID)
    {
        return pageDic.ContainsKey(pageID);
    }

    public void SetVisible(bool isVisible)
    {
        GetComponent<Canvas>().enabled = isVisible;
    }

    public UIPage_old GetPage(string pageID)
    {
        if (IsPageAvailable(pageID))
            return pageDic[pageID];

        //return null;
        throw new NullReferenceException("That page is unavailable.");
    }

    public virtual UIPage_old OpenPage(string pageID)
    {
        var pageToOpen = GetPage(pageID);
        pageToOpen.Open();

        return pageToOpen;
    }

    public virtual void ClosePage(string pageID)
    {
        var pateToClose = GetPage(pageID);
        pateToClose.Close();
    }
    #endregion

    #region UIController
    protected override async void Init()
    {
        isInit = false;

        foreach (var page in GetComponentsInChildren<UIPage_old>(true))
        {
            page.Init(this);
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

        isInit = true;
    }

    protected override void Deinit()
    {
        foreach (var page in pageDic.Values)
            page.Reset();

        pageDic.Clear();
    }
    #endregion
}