using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPanelMovementList : ToggleListPanel<MovementListItem, Movement>
{
    [SerializeField] List<MovementListItem> listItemPool = new List<MovementListItem>();

    protected override void Init()
    {
        base.Init();

        (parentPage as UIPageMovementList).onSelectType += OpenMovementList;
    }

    private void OpenMovementList(MovementType type)
    {
        //Debug.Log("Open movement list is type " + type.ToString());
        SetItems(ResourceManager.Instance.movementDic[type]);
    }

    private void ResetMovementList()
    {
        itemList.ForEach(item =>
        {
            item.gameObject.SetActive(false);
            item.Toggle.isOn = false;
        });
        itemList.Clear();
    }

    protected override void SetItems(List<Movement> itemList)
    {
        if (itemList == null) return;

        ResetMovementList();

        for (int i = 0; i < itemList.Count; i++)
        {
            if (listItemPool.Count <= i)
                AddItem(itemList[i]);
            else
                PoolItem(itemList[i], i);
        }
    }

    protected override void AddItem(Movement data)
    {
        base.AddItem(data);

        listItemPool.Add(Get(data));
    }

    private void PoolItem(Movement data, int index)
    {
        MovementListItem movement = listItemPool[index];

        itemList.Add(movement);
        movement.SetData(data, this);
        movement.GetComponent<RectTransform>().localScale = Vector3.one;
        movement.gameObject.SetActive(true);
    }

    protected override bool ContainItem(Movement data)
    {
        return itemList.Find((x) => x.Data.name.Equals(data.name)) != null;
    }

    protected override MovementListItem Get(Movement data)
    {
        return itemList.Find((x) => x.Data.name.Equals(data.name));
    }
}
