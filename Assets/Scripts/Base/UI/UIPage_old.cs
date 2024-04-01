using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIPage_old : UIBase_old
{
    public PageType pageType => (PageType)Enum.Parse(typeof(PageType), typeID);
    [SerializeField] protected List<UIPanel_old> panelList = new List<UIPanel_old>();
    protected UIPageController_old parentController;

    public float currentWidth => GetComponent<RectTransform>().rect.width;

    private void OnDestroy() { Reset(); }

    protected override void Init()
    {
        foreach (var panel in GetComponentsInChildren<UIPanel_old>(true))
        {
            panelList.Add(panel);
            InitPanel(panel);
        }

        IsInit = true;
    }

    public virtual void Init(UIPageController_old Parent)
    {
        Init();
        parentController = Parent;
    }

    public override void Reset()
    {
        panelList?.Clear();
        panelList = null;
    }

    public override void Activate(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (panelList != null)
        {
            foreach(var panel in panelList)
                panel.Activate(isActive);
        }
    }

    #region Method
    public virtual void Open() { Activate(true); }

    public virtual void Close() { Activate(false); }

    public virtual void ExcuteData(Dictionary<string, object> data) { }
    public virtual void SetCallbacks(Dictionary<string, Action> callbacks) { }
    public virtual void SetResultCallback(Action<object> callback) { }

    protected virtual void InitPanel(UIPanel_old panel)
    {
        panel.Init(this);
    }

    public virtual TPanel GetPanel<TPanel>() where TPanel : UIPanel_old
    {
        return panelList.Find((x) => x.GetType() == typeof(TPanel)) as TPanel;
    }
    #endregion
}
