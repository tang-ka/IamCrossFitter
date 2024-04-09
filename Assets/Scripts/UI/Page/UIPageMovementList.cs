using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageMovementList : UIPage
{
    MovementType selectedType;
    Movement selectedMovement;

    public Action<MovementType> onSelectType;
    public Action<Movement> onSelectMovement;

    protected override void Init()
    {
        typeID = PageType.MovementList.ToString();

        base.Init();
    }

    public void SelectType(MovementType type)
    {
        selectedType = type;
        onSelectType?.Invoke(type);
    }

    public void SelectMovement(Movement movement)
    {
        selectedMovement = movement;
        onSelectMovement?.Invoke(movement);
    }
}
