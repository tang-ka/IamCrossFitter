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
    
    public void CloseAllPage()
    {
        pageDic.Values.ToList().ForEach(x => x.Close());
    }
    #endregion

    #region Method : override
    protected override async void Init()
    {
        Debug.Log($"{name} Awake() 시작");
        isInit = false;

        foreach (var page in GetComponentsInChildren<UIPage>(true))
        {
            page.Init(this);
            pageDic.Add(page.TypeID, page);
        }
        Debug.Log($"{name} Awake() 도중 await");
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

        UIManager.Instance.RegisterController(this);
        Debug.Log($"{name} Awake() 끝");
    }

    protected override void Deinit()
    {
        pageDic.Clear();
        pageDic = null;

        UIManager.Instance.UnregisterController(this);
    } 
    #endregion
}
