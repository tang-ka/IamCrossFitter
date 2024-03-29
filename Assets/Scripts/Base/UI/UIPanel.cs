using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class UIPanel : UIBase
{
    #region Member
    UIPage parentPage; 
    #endregion

    #region Mono
    private void OnDestroy() { Deinit(); }
    #endregion

    #region Method : virtual
    public virtual void Init(UIPage page)
    {
        parentPage = page;

        Init();
    }

    public virtual void Activate()
    {
        Debug.Log($"Activate Panel : {typeID}");
        Active(true);
    }

    public virtual void Deactivate()
    {
        Debug.Log($"Deactivate Panel : {typeID}");
        Active(false);
    }
    #endregion

    #region Method : override
    protected override void Init()
    {
        typeID = gameObject.name.Split('_')[1];

        isInit = true;
    }

    protected override void Deinit() { }

    /// <summary>
    /// The part that activates the panel by page
    /// </summary>
    /// <param name="isActive"></param>
    protected override void Active(bool isActive)
    {
        if (!isInit)
            Init(GetComponentInParent<UIPage>());

        gameObject.SetActive(isActive);
    }
    #endregion
}
