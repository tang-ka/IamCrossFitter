using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPageController : UIController
{
    protected Dictionary<string, UIPage> pageDic = new Dictionary<string, UIPage>();

    #region Mono
    private void Awake() { Init(); }
    private void OnDestroy() { Deinit(); }
    #endregion

    #region Method
    public virtual void OpenPage(PageType pageType)
    {
        GetPage(pageType).Open();
    }

    public virtual void ClosePage(PageType pageType)
    {
        GetPage(pageType).Close();
    }
    #endregion

    #region Util
    public bool IsPageAvailable(PageType pageType)
    {
        return pageDic.ContainsKey(pageType.ToString());
    }

    public UIPage GetPage(PageType pageType) 
    {
        if (IsPageAvailable(pageType))
            return pageDic[pageType.ToString()];

        throw new NullReferenceException($"This page is unavailable in PageController - {gameObject.name}");
    }
    #endregion

    #region Method : override
    protected override async void Init()
    {
        isInit = false;

        foreach (var page in GetComponentsInChildren<UIPage>())
        {
            pageDic.Add(page.TypeID, page);
            page.Init(this);
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
        pageDic.Clear();
        pageDic = null;
    } 
    #endregion
}
