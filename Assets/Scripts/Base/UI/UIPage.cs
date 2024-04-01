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
    public bool isStartPage = false;
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
        //Debug.Log($"Activate Page : {typeID}");
        Active(true);
    }


    public virtual void Close() 
    { 
        //Debug.Log($"Deactivate Page : {typeID}");
        Active(false);
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
