using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ex) AnchorListPanel
/// T : AnchorListItem, W : AnchorData
/// AnchorListITem : ListItem<AnchorData>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="W"></typeparam>
public abstract class DataListPanel<T, W> : UIPanel where T : ListItem<W>
{
    [SerializeField] GameObject itemPrefab;
    protected List<T> itemList = new List<T>();

    [SerializeField] protected ScrollRect scrollView;
    [SerializeField] protected RectTransform content;

    protected virtual void SetItems(List<W> itemList)
    {
        if (itemList == null)
            return;

        for (int i = 0; i < itemList.Count; i++)
            AddItem(itemList[i]);
    }

    protected virtual void AddItem(W data)
    {
        T item;

        if (ContainItem(data))
            item = Get(data);
        else
        {
            var gameObj = Instantiate(itemPrefab, content);
            item = gameObj.GetComponent<T>();
            itemList.Add(item);
        }

        item.SetData(data);
        item.GetComponent<RectTransform>().localScale = Vector3.one;
        item.gameObject.SetActive(true);
    }

    protected virtual void ClearList()
    {
        itemList.RemoveAll((item) => item == null);

        for (int i = 0; i < itemList.Count; i++)
            Destroy(itemList[i].gameObject);
    }

    protected virtual void OpenList()
    {
        content.gameObject.SetActive(true);
    }

    protected virtual void CloseList()
    {
        content.gameObject.SetActive(false);
    }

    protected abstract bool ContainItem(W data);
    protected abstract T Get(W data);
}
