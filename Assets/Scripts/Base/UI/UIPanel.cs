using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIPanel : UIBase
{
    protected UIPage parentPage;

    private Action onSetFinished;

    private void OnDestroy() { Reset(); }

    public virtual void Init(UIPage parent)
    {
        parentPage = parent;
        Init();
    }

    public override void Init()
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
