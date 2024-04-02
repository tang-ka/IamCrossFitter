using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleListItem<T> : ListItem<T>
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

    protected override void SelectItem()
    {
        //Debug.Log($"Select {gameObject.name}");
    }

    protected override void DeselectItem()
    {
        //Debug.Log($"Deselect {gameObject.name}");
    }

    protected virtual void DeletItem() { }
}
