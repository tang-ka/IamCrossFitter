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
        // �̹� �����ִٸ�
        if (IsRegisteredToOpenedList(page))
            return false; // ��û ���

        // �ƴ϶��
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

        // �����ִ� �������� �ִ�.
        if (openedPageStack.Count != 0)
        {
            // �����ִ� �������� Home�϶�
            if (main == home)
            {
                
            }
            // �����ִ� �������� Main�϶�
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

    // ��� ����Ʈ���� ����� �� �ִ� -> ���� �����ִ�. -> ���������� Open��� ����
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
