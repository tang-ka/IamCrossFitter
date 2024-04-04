using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class ToggleListItem<T> : ListItem<T>
{
    protected Toggle toggle;
    [SerializeField] Button btnDelete;

    public Toggle Toggle => toggle;

    protected virtual void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.group = transform.GetComponentInParent<ToggleGroup>();
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(isSelected =>
        {
            if (isSelected)
                SelectItem();
            else
                DeselectItem();

            IsSelected = isSelected;
        });

        LinkDeleteButtonListener();
    }

    private void LinkDeleteButtonListener()
    {
        if (btnDelete == null)
            return;

        btnDelete.onClick.RemoveAllListeners();
        btnDelete.onClick.AddListener(DeletItem);
    }

    protected abstract void SelectItem();
    protected abstract void DeselectItem();

    protected virtual void DeletItem() { }
}
