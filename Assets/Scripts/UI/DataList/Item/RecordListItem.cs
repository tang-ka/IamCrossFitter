using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordListItem : ListItem<MovementRecord>
{
    [SerializeField] Text repsValue;
    [SerializeField] Text weightValue;
    [SerializeField] Text weigthUnit;

    public override void SetData(MovementRecord data, UIPanel panel)
    {
        base.SetData(data, panel);

        repsValue.text = data.reps.ToString();
        weightValue.text = data.weight_lb.ToString();
        weigthUnit.text = $"[{ResourceManager.Instance.SystemUnit}]";
    }
}
