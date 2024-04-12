using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenMode
{
    None = -1,
    Background, 
    Default, 
    Additive, 
    OnlyWithBackground, 
    Only
}

public class UIOpenHandler
{
    UIPageController controller;

    public PageType home = PageType.None;

    public List<PageType> openedBackgroundPageList = new List<PageType>();
    public Stack<PageType> pageHistory = new Stack<PageType>(); 

    public PageType main = PageType.None;
    public Stack<PageType> openedPageStack = new Stack<PageType>();

    public void RegisterHomePage(PageType type)
    {
        home = type;
    }

    public void AllocatePageController(UIPageController controller)
    {
        if (controller != null)
            this.controller = controller;
        else
            throw new NullReferenceException("The page is not available. Make sure the page is active.");
    }

    public void OpenPage(PageType type, OpenMode mode)
    {
        switch (mode)
        {
            case OpenMode.None:
                break;
            case OpenMode.Background:
                if (TryOpenBackgroundPage(type) == false) 
                    return;
                break;
            case OpenMode.Default:
                if (TryOpenMainPage(type) == false) 
                    return;
                break;
            case OpenMode.Additive:
                if (TryOpenAdditivePage(type) == false) 
                    return;
                break;
            case OpenMode.OnlyWithBackground:
                break;
            case OpenMode.Only:
                break;
            default:
                break;
        }

        var openedPage  = controller.OpenPage(type);
        openedPage.openMode = mode;
    }

    public bool TryOpenBackgroundPage(PageType page)
    {
        // 이미 열려있다면
        if (IsRegisteredToOpenedList(page))
            return false; // 요청 취소

        // 아니라면
        openedBackgroundPageList.Add(page);
        return true;
    }

    public bool TryOpenMainPage(PageType page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;

        if (openedPageStack.Count == 0)
        {

        }
        else
        {
            // 현재 메인 페이지가 열려있다. -> 그에 딸리 추가 페이지도 열려있을 수 있다.
            // pageStack.Count != 0;
            // 그걸 다 처리하고 다시 열려고 하는 게 목표일듯.
            // 그걸 여기서 처리할지 경고만 던질지 정해야 함.
        }

        openedPageStack.Clear();
        openedPageStack.Push(page);
        return true;
    }

    public bool TryOpenAdditivePage(PageType page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;

        if (openedPageStack.Count == 0)
            Debug.LogWarning("The main page is not currently open, but an attempt was made to open an additional page.");

        openedPageStack.Push(page);
        return true;
    }

    public void ClosePage(UIPageController controller, PageType pageType)
    {
        controller.ClosePage(pageType);
    }

    public void CloseBackgroundPage(UIPage page)
    {

    }

    public void CloseAdditivePage(UIPage page)
    {

    }


    // 어느 리스트에라도 등록이 돼 있다 -> 현재 열려있다. -> 열려있으면 Open명령 무시
    public bool IsRegisteredToOpenedList(PageType page)
    {
        bool isRegistered = false;

        if (openedBackgroundPageList.Contains(page))
        {
            isRegistered = true;
            Debug.Log("This page is registered to backgroundPageList");
        }

        if (openedPageStack.Contains(page))
        {
            isRegistered = true;
            Debug.Log("This page is registered to pageStack");
        }

        return isRegistered;
    }
}
