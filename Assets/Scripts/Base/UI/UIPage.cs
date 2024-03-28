using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : UIBase
{
    #region Member
    UIPageController parentController;
    List<UIPanel> myPanelList = new List<UIPanel>();
    [SerializeField] PageType pageType;
    #endregion

    #region Property
    public PageType PageType => pageType;
    #endregion

    #region Mono
    private void OnDestroy() { Deinit(); }
    #endregion

    #region Method : virtual
    public virtual void Init(UIPageController controller)
    {
        parentController = controller;

        Init();

        pageType = (PageType)Enum.Parse(typeof(PageType), typeID);
    }

    public virtual void Open() 
    { 
        Active(true);
        Debug.Log($"Activate Page : {typeID}");
    }


    public virtual void Close() 
    { 
        Active(false);
        Debug.Log($"Deactivate Page : {typeID}");
    }
    #endregion

    #region Method : override
    protected override void Init()
    {
        foreach (var panel in GetComponentsInChildren<UIPanel>(true))
        {
            myPanelList.Add(panel);
            panel.Init(this);
        }

        isInit = true;
    }

    protected override void Deinit()
    {
        myPanelList.Clear();
        myPanelList = null;
    }

    protected override void Active(bool isActive)
    {
        if (!isInit)
            Init(GetComponentInParent<UIPageController>());

        gameObject.SetActive(isActive);

        if (myPanelList != null)
        {
            foreach (var panel in myPanelList)
            {
                if (isActive)
                    panel.Activate();
                else
                    panel.Deactivate();
            }
        }

    }
    #endregion

    #region Util
    public virtual TPanel GetPanel<TPanel>() where TPanel : UIPanel
    {
        return myPanelList.Find((panel) => panel.GetType() == typeof(TPanel)) as TPanel;
    }
    #endregion

}
