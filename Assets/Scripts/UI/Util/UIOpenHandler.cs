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
    public List<UIPage> backgroundPageList = new List<UIPage>();

    public Stack<UIPage> pageStack = new Stack<UIPage>();
    public UIPage currentMainPage;

    
    public bool TryOpenBackgroundPage(UIPage page)
    {
        return true;
    }

    public bool TryOpenMainPage(UIPage page)
    {
        return true;
    }

    public bool TryOpenAdditivePage(UIPage page)
    {
        return true;
    }


}
