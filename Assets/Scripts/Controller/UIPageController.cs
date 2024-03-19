using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using TMPro.EditorUtilities;

public class UIPageController : UIController
{
    protected Dictionary<string, UIPage> pageDic = new Dictionary<string, UIPage>();

    #region Mono
    private void Awake() { Init(); }
    private void OnDestroy() { Reset(); }
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

    public UIPage GetPage(string pageID)
    {
        if (IsPageAvailable(pageID))
            return pageDic[pageID];

        //return null;
        throw new NullReferenceException("That page is unavailable.");
    }

    public virtual UIPage OpenPage(string pageID/*, PageOpenMode openMode*/)
    {
        var pageToOpen = GetPage(pageID);
        pageToOpen.Open();
        //pageToOpen.Open(openMode);

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
        IsInit = false;

        foreach (var page in GetComponentsInChildren<UIPage>(true))
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