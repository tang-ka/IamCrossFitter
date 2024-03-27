using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageLoading : UIPage_old
{
    protected override void Init()
    {
        typeID = PageType.Loading.ToString();

        base.Init();
    }
}
