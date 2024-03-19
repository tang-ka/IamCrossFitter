using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class ToggleListPanel<T, W> : DataListPanel<T, W> where T : ListItem<W>
{
    protected ToggleGroup toggleGroup;
    protected virtual void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        toggleGroup.allowSwitchOff = true;
    }

    protected override bool ContainItem(W data)
    {
        throw new System.NotImplementedException();
    }

    protected override T Get(W data)
    {
        throw new System.NotImplementedException();
    }
}
