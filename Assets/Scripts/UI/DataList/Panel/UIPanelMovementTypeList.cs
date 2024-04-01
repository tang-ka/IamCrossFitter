using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelMovementTypeList : ToggleListPanel<MovementTypeListItem, MovementType>
{
    protected override void Init()
    {
        base.Init();

        SetItems(ResourceManager.Instance.movementTypeList);
    }

    protected override bool ContainItem(MovementType data)
    {
        return itemList.Find((x) => x.Data.Equals(data)) != null;
    }

    protected override MovementTypeListItem Get(MovementType data)
    {
        return itemList.Find((x) => x.Data.Equals(data));
    }
}
