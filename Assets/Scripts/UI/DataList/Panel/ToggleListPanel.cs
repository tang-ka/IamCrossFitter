using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public abstract class ToggleListPanel<T, W> : DataListPanel<T, W> where T : ListItem<W>
{
    protected ToggleGroup toggleGroup;
    protected virtual void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    protected abstract override bool ContainItem(W data);
    protected abstract override T Get(W data);
}
