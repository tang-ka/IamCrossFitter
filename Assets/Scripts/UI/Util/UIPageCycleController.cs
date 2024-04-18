using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageCycleType
{
    None = -1,
    background,
    Home,
    Main,
    Additive,
    Popup,
}

public class UIPageCycleController : MonoBehaviour
{
    #region Members
    [SerializeField] PageType curHomePage = PageType.None;
    [SerializeField] PageType curDisplayPage = PageType.None;
    [SerializeField] PageType curBackPage = PageType.None;

    [SerializeField] List<PageType> backgroundPageList = new List<PageType>();
    [SerializeField] Stack<PageType> pageHistoryStack = new Stack<PageType>();
    [SerializeField] Stack<PageType> curOpenedPageStack = new Stack<PageType>();

    Action<PageType> openAction;
    Action<PageType> closeAction;
    #endregion

    #region Property
    public PageType CurHomePage
    {
        get { return curHomePage; }
        private set { curHomePage = value; }
    }

    public PageType CurDisplayPage
    {
        get { return curDisplayPage; }
        private set { curDisplayPage = value; }
    }

    public PageType CurBackPage
    {
        get { return curBackPage; }
        private set { curBackPage = value; }
    }
    #endregion

    public UIPageCycleController(Action<PageType> openAction, Action<PageType> closeAction)
    {
        this.openAction = openAction;
        this.closeAction = closeAction;
    }

    public void OpenPage(PageType pageType, PageCycleType cycleType, Action<PageType> openAction = null)
    {
        switch (cycleType)
        {
            case PageCycleType.None:
                Debug.LogWarning("requested cycle Type is None. Please check cycleType.");
                return;
            case PageCycleType.background:
                break;
            case PageCycleType.Home:
                break;
            case PageCycleType.Main:
                break;
            case PageCycleType.Additive:
                break;
            case PageCycleType.Popup:
                break;
            default:
                break;
        }

        if (openAction == null)
            this.openAction?.Invoke(pageType);
        else
            openAction?.Invoke(pageType);
    }

    public void ClosePage(PageType pageType, Action<PageType> closeAction = null)
    {


        if (closeAction == null)
            this.closeAction?.Invoke(pageType);
        else
            closeAction?.Invoke(pageType);
    }

    public void GoBack()
    {
        ClosePage(pageHistoryStack.Pop());
    }

    private void RegisterHomePage(PageType pageType)
    {
        CurHomePage = pageType;
    }

    private void SetDisplayingPage(PageType pageType)
    {
        CurDisplayPage = pageType;
    }

    private void setBackpage(PageType pageType)
    {
        CurBackPage = pageType;
    }
}
