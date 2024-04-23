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
    Dashboard, MovementList, RecordManagement,

    // PersonalRecord Scene

    //TempPage
    Temp1, Temp2, Temp3
}

public class UIManager : ManagerBase<UIManager>, IStateObserver<MainState>
{
    [SerializeField] List<UIPageController> pageControllerList = new List<UIPageController>();

    [SerializeField] UIPageCycleController pageCycleController;

    public override void Init()
    {
        pageCycleController.closeAction += CloseAction;
        pageCycleController.openAction += OpenAction;

        //pageCycleController = new UIPageCycleController(OpenAction, ClosePage);

        WorldManager.Instance.AddObserver(this);
        base.Init();

         void CloseAction(PageType pageType)
        {
            var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            controller?.ClosePage(pageType);
        }

        void OpenAction(PageType pageType)
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
    }

    public override void Deinit()
    {
        WorldManager.Instance.RemoveObserver(this);
        base.Deinit();
    }

    #region UIPageCycleController
    public void OpenPage(PageType pageType, PageCycleType cycleType)
    {
        pageCycleController.OpenPage(pageType, cycleType, OpenAction);

        void OpenAction(PageType pageType)
        {
            try
            {
                var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
                controller.GetPage(pageType).cycleType = cycleType;
                controller.OpenPage(pageType);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("You can't get it. because it is not available. Make sure the page is active.");
            }
        }
    }    

    public void ClosePage(PageType pageType)
    {
        var controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
        pageCycleController.ClosePage(pageType, _=> controller.ClosePage(pageType));
    }

    public void GoBackPage()
    {
        pageCycleController.GoBack();
    }

    public bool IsNowHome()
    {
        return pageCycleController.curDisplayPage == pageCycleController.curHomePage;
    }
    #endregion

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
                OpenPage(PageType.Loading, PageCycleType.Home);
                break;
            case MainState.Main:
                OpenPage(PageType.SystemUI, PageCycleType.Background);
                OpenPage(PageType.Dashboard, PageCycleType.Home);
                break;
            default:
                break;
        }
    } 
    #endregion

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
