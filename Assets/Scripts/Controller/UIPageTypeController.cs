using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageTypeController : UIPageController
{
    public virtual UIPage OpenPage(PageType type)
    {
        return OpenPage(type.ToString());
    }

    public virtual void ClosePage(PageType type)
    {
        ClosePage(type.ToString());
    }

    protected override void Init()
    {
        base.Init();
        UIManager.Instance.RegisterPageController(this);
    }

    public override void Reset()
    {
        UIManager.Instance.UnregisterPageController(this);
        base.Reset();
    }
}
