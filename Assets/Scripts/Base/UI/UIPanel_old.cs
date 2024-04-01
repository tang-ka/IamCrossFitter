using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIPanel_old : UIBase_old
{
    protected UIPage_old parentPage;

    private Action onSetFinished;

    private void OnDestroy() { Reset(); }

    public virtual void Init(UIPage_old parent)
    {
        parentPage = parent;
        Init();
    }

    protected override void Init()
    {
        typeID = gameObject.name.Split('_')[1];
        IsInit = true;
    }

    public override void Reset() { }

    public override void Activate(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public virtual void SetPanel(Action finishCallback)
    {
        onSetFinished = finishCallback;
    }
}
