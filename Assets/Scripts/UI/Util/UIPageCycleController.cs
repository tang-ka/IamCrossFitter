using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
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
    public PageType curHomePage = PageType.None;
    public PageType curDisplayPage
    {
        get
        {
            if (curOpenedPageStack.Count == 0)
                return PageType.None;

            return curOpenedPageStack.Peek();
        }
    }

    public PageType curBackPage = PageType.None;

    [SerializeField] List<PageType> backgroundPageList = new();
    Stack<PageType> pageHistoryStack = new Stack<PageType>() { };
    Stack<PageType> curOpenedPageStack = new();

    [SerializeField] List<PageType> historyTest = new();
    [SerializeField] List<PageType> openedTest = new();

    public Action<PageType> openAction;
    public Action<PageType> closeAction;
    #endregion

    #region Property
    public PageType CurBackPage
    {
        get { return curBackPage; }
        private set { curBackPage = value; }
    }
    #endregion

    //public UIPageCycleController(Action<PageType> openAction, Action<PageType> closeAction)
    //{
    //    this.openAction = openAction;
    //    this.closeAction = closeAction;
    //}

    #region Method : Main (public)
    public void OpenPage(PageType pageType, PageCycleType cycleType = PageCycleType.None, Action<PageType> openAction = null)
    {
        (bool isPossible, Action onCompletePageOpen) result = (false, null);

        switch (cycleType)
        {
            case PageCycleType.None:
                //Debug.LogWarning($"requested cycleType is None. Please check cycleType of {pageType}.");
                break;

            case PageCycleType.Background:
                if (TryOpenBackgroundPage(pageType) == false) return;

                break;

            case PageCycleType.Home:
                result = TryOpenHomePage(pageType);
                if (!result.isPossible)
                    return;
                break;

            case PageCycleType.Main:
                result = TryOpenMainPage(pageType);
                if (!result.isPossible)
                    return;
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

        result.onCompletePageOpen?.Invoke();
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
        if (curDisplayPage == curHomePage)
            return;

        if (curOpenedPageStack.Count != 0)
        {
            ClosePage(curOpenedPageStack.Pop());
            openedTest = curOpenedPageStack.ToList();
        }
        
        if (curOpenedPageStack.Count == 0)
        {            
            if (pageHistoryStack.Count > 0)
            {
                var pageToOpen = pageHistoryStack.Pop();
                OpenPage(pageToOpen);
                SetDisplayingPage(pageToOpen);
            }
        }
    }
    #endregion

    private bool TryOpenBackgroundPage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        backgroundPageList.Add(pageType);
        return true;
    }

    private (bool isPossible, Action onCompletePageOpen) TryOpenHomePage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return (false, null);

        CloseAllPage();

        return (true, CompleteOpneHomePage);

        void CompleteOpneHomePage()
        {
            RegisterHomePage(pageType);
            SetDisplayingPage(pageType);
        }
    }

    private (bool isPossible, Action onCompletePageOpen) TryOpenMainPage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return (false, null);

        CloseAllPage();

        pageHistoryStack.Push(curHomePage);
        SetBackPage(curHomePage);

        historyTest = pageHistoryStack.ToList();

        return (true, CompleteOpneHomePage);

        void CompleteOpneHomePage()
        {
            SetDisplayingPage(pageType);
        }
    }

    private bool TryOpenAdditivePage(PageType pageType)
    {
        if (IsAlreadyOpened(pageType)) return false;

        SetBackPage(curOpenedPageStack.Peek());
        SetDisplayingPage(pageType);

        return true;
    }

    private void CloseAllPage()
    {
        int openedPageCount = curOpenedPageStack.Count;
        for (int i = 0; i < openedPageCount; i++)
        {
            ClosePage(curOpenedPageStack.Pop());
        }

        PageStackReset();
    }

    private void RegisterHomePage(PageType pageType)
    {
        PageStackReset();
        curHomePage = pageType;
    }

    private void SetDisplayingPage(PageType pageType)
    {
        if (curOpenedPageStack.Contains(pageType))
            return;

        curOpenedPageStack.Push(pageType);
        openedTest = curOpenedPageStack.ToList();
    }

    private void SetBackPage(PageType pageType)
    {
        CurBackPage = pageType;
    }

    bool IsAlreadyOpened(PageType pageType)
    {
        return curOpenedPageStack.Contains(pageType) || backgroundPageList.Contains(pageType);
    }

    private void PageStackReset()
    {
        pageHistoryStack.Clear();
        curOpenedPageStack.Clear();
    }
}