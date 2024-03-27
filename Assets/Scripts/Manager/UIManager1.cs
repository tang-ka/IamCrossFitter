using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageType
{
    None,

    // Managing Scene
    SystemUI, Setting,

    // Loading Scene
    Loading,

    // Main Scene
    Dashboard,

    // PersonalRecord Scene
}

public class UIManager1 : ManagerBase<UIManager>, IStateObserver<MainState>
{
    // UIPageTypeController를 관리하고 싶다.
    List<UIPageTypeController> pageControllers = new List<UIPageTypeController>();

    UIPage_old curPage;
    UIPage_old prePage;

    [SerializeField] List<UIPage_old> curOpenedPages = new List<UIPage_old>();

    public override void Init()
    {
        WorldManager.Instance.AddObserver(this);
        base.Init();
    }

    public override void Reset()
    {
        WorldManager.Instance.RemoveObserver(this);
        base.Reset();
    }

    #region Utility
    /// <summary>
    /// Utility : Set visibility for all UI(PageController)
    /// </summary>
    /// <param name="visible"></param>
    public void SetUIVisibility(bool visible)
    {
        pageControllers.ForEach((controller) =>
        {
            controller.SetVisible(visible);
        });
    }

    /// <summary>
    /// Utility : Get page with page's name
    /// </summary>
    /// <param name="pageName"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public UIPage_old GetPage(string pageName)
    {
        foreach (var controller in pageControllers)
            return controller.GetPage(pageName);

        throw new NullReferenceException($"There is no page with {pageName}'s name. Please check the page name again.");
    }
    #endregion

    public void OpenPage(PageType page)
    {
        var pageController = pageControllers.Find((controller) => controller.IsPageAvailable(page.ToString()));

        if (pageController != null)
        {
            if (curOpenedPages.Contains(pageController.GetPage(page.ToString())))
                return;

            Debug.Log($"Open page : {page}");

            if (curPage != null)
            {
                prePage = curPage;
            }

            curPage = pageController.OpenPage(page);
            curOpenedPages.Add(curPage);
        }
    }

    public void CloseCurPage()
    {
        if (curPage != null)
        {
            Debug.Log($"Close current page : {curPage.TypeID}");
            curPage.Close();

            curPage.Close();
            curOpenedPages.Remove(curPage);

            curPage = null;
        }
    }

    public void CloseStackedPages()
    {
        
    }

    public void CloseAllPages()
    {
        
    }


    public void RegisterPageController(UIPageTypeController controller)
    {
        if (!pageControllers.Contains(controller))
            pageControllers.Add(controller);
    }

    public void UnregisterPageController(UIPageTypeController controller)
    {
        pageControllers.Remove(controller);
    }

    // Set the page to open first as the MainState changes
    public void Notify(MainState state)
    {
        switch (state)
        {
            case MainState.None:
                OpenPage(PageType.None);
                return;
            case MainState.Loading:
                OpenPage(PageType.Loading);
                return;
            case MainState.Main:
                OpenPage(PageType.SystemUI);
                OpenPage(PageType.Dashboard);
                return;
            case MainState.PersonalRecord:
                break;
        }

    }
}
