using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
    protected UIOpenHandler openHandler;

    public List<PageType> BackgroundPages => openHandler.openedBackgroundPageList;
    public Stack<PageType> PageStack => openHandler.openedPageStack;
    public PageType HomePage => openHandler.home;
    #endregion
    [SerializeField] UIPageCycleController pageCycleController;

    public override void Init()
    {
        pageCycleController = new UIPageCycleController(OpenPage, ClosePage);
        openHandler = new UIOpenHandler();
        WorldManager.Instance.AddObserver(this);
        base.Init();
    }

    public override void Deinit()
    {
        WorldManager.Instance.RemoveObserver(this);
        base.Deinit();
    }

    #region UIPageCycleController
    public void OpenPage(PageType pageType, PageCycleType cycleType)
    {
        pageCycleController.OpenPage(pageType, cycleType);
    }

    private void OpenPage(PageType pageType)
    {
        try
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            controller.OpenPage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("You can't get it. because it is not available. Make sure the page is active.");
        }
    }

    public void ClosePage(PageType pageType)
    {
        var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
        openHandler.ClosePage(controller, pageType);
    }

    public void GoBackPage()
    {
        pageCycleController.GoBack();
    }

    #endregion

    #region Control Page with OpenMode
    public void RegisterHomePage(PageType pageType)
    {
        openHandler.RegisterHomePage(pageType);
    }

    public void OpenPage(PageType pageType, OpenMode openMode = OpenMode.Main)
    {
        openHandler.AllocatePageController(pageControllerList.Find((x) => x.IsPageAvailable(pageType)));
        openHandler.OpenPage(pageType, openMode);
    }

    #endregion

    //public void ClosePage(PageType pageType)
    //{
    //    var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
    //    openHandler.ClosePage(controller, pageType);
    //}

    public UIPage GetPage(PageType pageType)
    {
        try
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            return controller.GetPage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("You can't get it. because it is not available. Make sure the page is active.");
        }
    }

    #region About regist and observer
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
                OpenPage(PageType.Loading); // OpenMode : Main
                break;
            case MainState.Main:
                OpenPage(PageType.SystemUI, OpenMode.Background); // OpenMode : Background
                OpenPage(PageType.Dashboard, OpenMode.Home); // OpenMode : Home
                break;
            default:
                break;
        }
    } 
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            string log = $"Home : {openHandler.home}\n";
            Debug.Log(log);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            string log = "==== Background Page ====\n";
            log += ListLog(openHandler.openedBackgroundPageList);
            Debug.Log(log);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            string log = $"Main : {openHandler.main}";
            Debug.Log(log);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            string log = "==== Page History ====\n";

            log += ListLog(openHandler.pageHistory.ToList<PageType>());
            Debug.Log(log);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            string log = "==== Opened Page Stack ====\n";

            log += ListLog(openHandler.openedPageStack.ToList<PageType>());
            Debug.Log(log);
        }
    }

    string ListLog(List<PageType> list)
    {
        string log = "";
        foreach (var item in list)
        {
            log += item + "\n";
        }

        return log;
    }
}
