using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListItem<T> : MonoBehaviour
{
    protected UIPanel parentPanel;
    protected T data;

    public T Data => data;

    public bool IsSelected { get; protected set; }

    public virtual void SetData(T data, UIPanel panel)
    {
        this.data = data;
        parentPanel = panel;
    }
}
