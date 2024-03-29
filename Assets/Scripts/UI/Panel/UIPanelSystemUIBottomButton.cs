using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSystemUIBottomButton : UIPanel
{
    [SerializeField] Button btnTemp1;
    [SerializeField] Button btnTemp2;
    [SerializeField] Button btnTemp3;
    [SerializeField] Button btnTemp4;

    protected override void Init()
    {
        btnTemp1.onClick.AddListener(() => Debug.Log("Click Temp1"));
        btnTemp2.onClick.AddListener(() => Debug.Log("Click Temp2"));
        btnTemp3.onClick.AddListener(() => Debug.Log("Click Temp3"));
        btnTemp4.onClick.AddListener(OnClickTmep4);

        base.Init();
    }

    public void OnClickTmep4()
    {
        UIManager.Instance.ClosePage(PageType.Dashboard);
        UIManager.Instance.OpenPage(PageType.Record);
    }
}


