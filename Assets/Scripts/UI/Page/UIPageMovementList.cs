using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageMovementList : UIPage
{
    MovementType selectedType;
    public Action<MovementType> onSelectType;

    protected override void Init()
    {
        typeID = PageType.Record.ToString();

        base.Init();
    }

    public void SelectType(MovementType type)
    {
        selectedType = type;
        onSelectType?.Invoke(type);
    }
}
