using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PageCycleType
{
    None = -1,
    Background,
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

    [SerializeField] List<PageType> backgroundPageList = new();
    Stack<PageType> pageHistoryStack = new Stack<PageType>() { };
    Stack<PageType> curOpenedPageStack = new();

    [SerializeField] List<PageType> historyTest = new();
    [SerializeField] List<PageType> openedTest = new();

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

    private void Awake()
    {
        
    }

    //public UIPageCycleController(Action<PageType> openAction, Action<PageType> closeAction)
    //{
    //    this.openAction = openAction;
    //    this.closeAction = closeAction;
    //}

    public void OpenPage(PageType pageType, PageCycleType cycleType, Action<PageType> openAction = null)
    {
        switch (cycleType)
        {
            case PageCycleType.None:
                Debug.LogWarning($"requested cycleType is None. Please check cycleType of {pageType}.");
                return;

            case PageCycleType.Background:
                if (TryOpenBackgorundPage(pageType) == false) return;
                break;

            case PageCycleType.Home:
                if (TryOpenHomePage(pageType) == false) return;
                break;

            case PageCycleType.Main:
                if (TryOpenMainPage(pageType) == false) return;
                break;

            case PageCycleType.Additive:
                if (TryOpenAdditivePage(pageType) == false) return;
                break;

            case PageCycleType.Popup:
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

        historyTest = pageHistoryStack.ToList();
    }

    public void GoBack()
    {
        ClosePage(pageHistoryStack.Pop());
    }

    private bool TryOpenBackgorundPage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        backgroundPageList.Add(pageType);
        return true;
    }

    private bool TryOpenHomePage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        RegisterHomePage(pageType);
        SetDisplayingPage(pageType);
        return true;
    }

    private bool TryOpenMainPage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        curOpenedPageStack.Clear();
        SetDisplayingPage(pageType);

        pageHistoryStack.Push(curHomePage);
        SetBackpage(curHomePage);

        historyTest = pageHistoryStack.ToList();

        return true;
    }
    // 히스토리는 클로즈를 하면서 추가 하고
    // 오픈드페이지는 어디디티브로 열었을때 추가하고
    // 백을 누르면 오픈드 페이지 먼저 소모하고 히스토리 소모하는 방식으로
    private bool TryOpenAdditivePage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        SetBackpage(curOpenedPageStack.Peek());

        SetDisplayingPage(pageType);

        return true;
    }

    private void RegisterHomePage(PageType pageType)
    {
        PageStackReset();

        CurHomePage = pageType;
    }

    private void SetDisplayingPage(PageType pageType)
    {
        CurDisplayPage = pageType;
        curOpenedPageStack.Push(pageType);

        openedTest = curOpenedPageStack.ToList();
    }

    private void SetBackpage(PageType pageType)
    {
        CurBackPage = pageType;
    }

    bool IsAlreadyOpened(PageType pageType)
    {
        return curOpenedPageStack.Contains(pageType) || backgroundPageList.Contains(pageType);

        //if (curOpenedPageStack != null && backgroundPageList != null)
        //    return curOpenedPageStack.Contains(pageType) || backgroundPageList.Contains(pageType);
        //else if (curOpenedPageStack == null && backgroundPageList != null)
        //    return backgroundPageList.Contains(pageType);
        //else if (curOpenedPageStack != null && backgroundPageList == null)
        //    return curOpenedPageStack.Contains(pageType);        
        //else
        //    return true;
    }

    private void PageStackReset()
    {
        pageHistoryStack.Clear();
        curOpenedPageStack.Clear();
    }
}
