using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageTypeController : UIPageController_old
{
    public virtual UIPage_old OpenPage(PageType type)
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
        //UIManager.Instance.RegisterPageController(this);
    }

    protected override void Deinit()
    {
        //UIManager.Instance.UnregisterPageController(this);
        base.Deinit();
    }
}
