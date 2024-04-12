using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIPageController : UIController
{
    protected Dictionary<string, UIPage> pageDic = new Dictionary<string, UIPage>();

    #region Mono
    private void Awake() { Init(); }
    private void OnDestroy() { Deinit(); }
    #endregion

    #region Method
    public virtual UIPage OpenPage(PageType pageType)
    {
        return GetPage(pageType).Open();
    }

    public virtual UIPage ClosePage(PageType pageType)
    {
        return GetPage(pageType).Close();
    }

    private async void PageInit()
    {
        foreach (var page in GetComponentsInChildren<UIPage>(true))
        {
            page.Init(this);
            pageDic.Add(page.TypeID, page);

            if (page.isStartPage)
            {
                UIManager.Instance.RegisterHomePage((PageType)Enum.Parse(typeof(PageType), page.TypeID));
                page.Open();
            }
            else
                page.Close();
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
    
    public void CloseAllPage()
    {
        pageDic.Values.ToList().ForEach(x => x.Close());
    }
    #endregion

    #region Method : override
    protected override void Init()
    {
        isInit = false;

        PageInit();

        UIManager.Instance.RegisterController(this);
    }

    protected override void Deinit()
    {
        pageDic.Clear();
        pageDic = null;

        UIManager.Instance.UnregisterController(this);
    } 
    #endregion
}
