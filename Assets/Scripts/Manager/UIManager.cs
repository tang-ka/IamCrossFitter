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
    Dashboard, Record

    // PersonalRecord Scene
}

public class UIManager : ManagerBase<UIManager>, IStateObserver<MainState>
{
    [SerializeField] List<UIPageController> pageControllerList = new List<UIPageController>();

    #region Page OpenMode Handler
    List<UIPage> backgroundPages = new List<UIPage>();
    List<UIPage> statePages = new List<UIPage>();
    List<UIPage> additivePages = new List<UIPage>();
    #endregion

    protected override void StartSettting()
    {
        base.StartSettting();
        WorldManager.Instance.AddObserver(this);
    }

    public override void Deinit()
    {
        WorldManager.Instance.RemoveObserver(this);
        base.Deinit();
    }

    public void OpenPage(PageType pageType)
    {
        Debug.Log("1 : " + pageControllerList.Count);

        var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
        Debug.Log("2 : " + pageControllerList.Count);

        controller.OpenPage(pageType);
        Debug.Log("3 : " + pageControllerList.Count);
        //try
        //{
        //    var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
        //    Debug.Log(pageControllerList.Count);

        //    controller.OpenPage(pageType);
        //}
        //catch (NullReferenceException)
        //{
        //    throw new NullReferenceException("The page you requested is not available. Make sure the page is active.");
        //}
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
