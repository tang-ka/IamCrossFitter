using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageSystemUI : UIPage
{
    protected override void Init()
    {
        typeID = PageType.SystemUI.ToString();

        base.Init();
    }
}
