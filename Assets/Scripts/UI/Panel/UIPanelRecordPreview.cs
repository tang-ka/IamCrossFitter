using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPanelRecordPreview : DataListPanel<RecordListItem, MovementRecord>
{
    [SerializeField] List<RecordListItem> listItemPool;

    protected override void Init()
    {
        base.Init();

        (parentPage as UIPageMovementList).onSelectMovement += SetRecordPreview;
    }

    private void SetRecordPreview(Movement movement)
    {
        SetItems(movement.record);
    }

    protected override void SetItems(List<MovementRecord> itemList)
    {
        if (itemList == null) return;

        MovementRecord bestRecord;

        var oneRMList = itemList.FindAll(x => x.reps == 1);

        if (oneRMList.Count != 0)
        {
            bestRecord = oneRMList[0];
            oneRMList.ForEach(x =>
            {
                if (bestRecord.weight_lb < x.weight_lb)
                    bestRecord = x;
                else if (bestRecord.weight_lb == x.weight_lb)
                {
                    if (bestRecord.date < x.date)
                        bestRecord = x;
                }
            });

            listItemPool[0].SetData(bestRecord, this);
        }
        else
        {
            var unRecord = new MovementRecord(0, 0);
            listItemPool[0].SetData(unRecord, this);
        }

        var latest = itemList.Find(x => x.date.Equals(itemList.Max(x => x.date)));
        
        listItemPool[1].SetData(latest, this);
    }

    protected override void AddItem(MovementRecord data)
    {
        base.AddItem(data);
    }

    protected override bool ContainItem(MovementRecord data)
    {
        return itemList.Find(x => x.Data.id.Equals(data.id)) != null;
    }

    protected override RecordListItem Get(MovementRecord data)
    {
        return itemList.Find(x => x.Data.id.Equals(data.id));
    }
}
