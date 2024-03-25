using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageLoading : UIPage
{
    protected override void Init()
    {
        typeID = PageType.Loading.ToString();

        base.Init();
    }
}
