using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenMode
{
    Background, 
    Default, 
    Additive, 
    OnlyWithBackground, 
    Only
}

public class UIOpenHandler
{
    UIManager manager;
    UIPageController controller;
    List<UIPageController> pageControllerList = new List<UIPageController>();

    public List<UIPage> openedBackgroundPageList = new List<UIPage>();

    public Stack<UIPage> openedPageStack = new Stack<UIPage>();
    public UIPage openedCurrentMainPage;

    public UIOpenHandler(UIManager manager, List<UIPageController> pageControllerList)
    {
        this.manager = manager;
        this.pageControllerList = pageControllerList;
    }

    public void OpenPage(PageType pageType)
    {
        try
        {
            controller = pageControllerList.Find((x) => x.IsPageAvailable(pageType));
            controller.OpenPage(pageType);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("The page can't be Opend. because it is not available. Make sure the page is active.");
        }
    }

    public bool TryOpenBackgroundPage(UIPage page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;



        openedBackgroundPageList.Add(page);
        return true;
    }

    public bool TryOpenMainPage(UIPage page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;

        if (openedCurrentMainPage == null)
        {
            openedCurrentMainPage = page;
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

    public bool TryOpenAdditivePage(UIPage page)
    {
        if (IsRegisteredToOpenedList(page))
            return false;

        if (openedCurrentMainPage == null)
            Debug.LogWarning("The main page is not currently open, but an attempt was made to open an additional page.");

        openedPageStack.Push(page);
        return true;
    }

    public void CloseBackgroundPage(UIPage page)
    {

    }

    public void CloseAdditivePage(UIPage page)
    {

    }


    // 어느 리스트에라도 등록이 돼 있다 -> 현재 열려있다. -> 열려있으면 Open명령 무시
    public bool IsRegisteredToOpenedList(UIPage page)
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
