using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSystemUIBottomButton : UIPanel
{
    [SerializeField] Button btnGoTemp1;
    [SerializeField] Button btnGoTemp2;
    [SerializeField] Button btnGoTemp3;
    [SerializeField] Button btnGoMovementListPage;

    protected override void Init()
    {
        btnGoTemp1.onClick.AddListener(GoTemp1);
        btnGoTemp2.onClick.AddListener(GoTemp2);
        btnGoTemp3.onClick.AddListener(GoTemp3);
        btnGoMovementListPage.onClick.AddListener(GoMovementListPage);

        base.Init();
    }

    private void GoTemp1()
    {
        UIManager.Instance.OpenPage(PageType.Temp1, PageCycleType.Main);
    }

    private void GoTemp2()
    {
        UIManager.Instance.OpenPage(PageType.Temp2, PageCycleType.Main);
    }

    private void GoTemp3()
    {
        UIManager.Instance.OpenPage(PageType.Temp3, PageCycleType.Main);
    }

    public void GoMovementListPage()
    {
        //UIManager.Instance.ClosePage(PageType.Dashboard);
        UIManager.Instance.OpenPage(PageType.MovementList, PageCycleType.Main);
    }
}


