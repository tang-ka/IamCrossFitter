using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageDashboard : UIPage
{
    protected override void Init()
    {
        typeID = PageType.Dashboard.ToString();

        base.Init();
    }

    public override UIPage Open()
    {
        Debug.Log("Opne DashBoard");
        return base.Open();
    }
}
