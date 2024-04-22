using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum OpenMode
{
    None = -1,
    Background,
    Home,
    Main, 
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
            case OpenMode.Home:
                if (TryOpenHomePage(type) == false)
                    return;
                break;
            case OpenMode.Main:
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

        var openedPage = controller.OpenPage(type);
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

    private bool TryOpenHomePage(PageType page)
    {
        RegisterHomePage(page);
        PushPageStack(page);

        return true;
    }

    public bool TryOpenMainPage(PageType page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;

        // 열려있는 페이지가 있다.
        if (openedPageStack.Count != 0)
        {
            // 열려있는 페이지가 Home일때
            if (main == home)
            {
                
            }
            // 열려있는 페이지가 Main일때
            else
            {


            }

            openedPageStack.Clear();
        }

        PushPageStack(page);
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

    private void PushPageStack(PageType page)
    {
        main = page;
        openedPageStack.Push(page);
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
