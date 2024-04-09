using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
  
public enum PageType
{
    None,

    // Managing Scene
    SystemUI, Setting,

    // Loading Scene
    Loading,

    // Main Scene
    Dashboard, MovementList, PersonalRecord

    // PersonalRecord Scene
}

public class UIManager : ManagerBase<UIManager>, IStateObserver<MainState>
{
    [SerializeField] List<UIPageController> pageControllerList = new List<UIPageController>();

    #region Page OpenMode Handler
    protected UIOpenHandler openHandler = new UIOpenHandler();

    public List<UIPage> BackgroundPages => openHandler.backgroundPageList;
    public Stack<UIPage> PageStack => openHandler.pageStack;
    public UIPage CurrentMainPage => openHandler.currentMainPage;
    #endregion

    public override void Init()
    {
        WorldManager.Instance.AddObserver(this);
        base.Init();
    }

    public override void Deinit()
    {
        WorldManager.Instance.RemoveObserver(this);
        base.Deinit();
    }

    #region Control Page with OpenMode
    public void OpenPage(PageType pageType, OpenMode openMode)
    {
        
    }


    #endregion

    public void OpenPage(PageType pageType)
    {
        try
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            controller.OpenPage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("The page you requested is not available. Make sure the page is active.");
        }
    }

    public void ClosePage(PageType pageType)
    {
        try
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));

            controller.ClosePage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("The page you requested is not available. Make sure the page is active.");
        }
    }

    public UIPage GetPage(PageType pageType)
    {
        try
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            return controller.GetPage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("The page you requested is not available. Make sure the page is active.");
        }
    }

    public void RegisterController(UIPageController controller)
    {
        if (!pageControllerList.Contains(controller))
            pageControllerList.Add(controller);
    }

    public void UnregisterController(UIPageController controller)
    {
        pageControllerList.Remove(controller);
    }

    public void Notify(MainState state)
    {
        Debug.Log($"{gameObject.name} listen notification : {state}");

        switch (state)
        {
            case MainState.None:
                break;
            case MainState.Loading:
                OpenPage(PageType.Loading); // OpenMode : Default
                break;
            case MainState.Main:
                OpenPage(PageType.SystemUI); // OpenMode : Background
                OpenPage(PageType.Dashboard); // OpenMode : Default
                break;
            default:
                break;
        }
    }
}
