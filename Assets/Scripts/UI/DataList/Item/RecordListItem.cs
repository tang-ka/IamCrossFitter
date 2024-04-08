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

        if (data.reps != 0)
            repsValue.text = data.reps.ToString();
        else
            repsValue.text = "-";

        if (data.weight_lb != 0)
            weightValue.text = data.weight_lb.ToString();
        else
            weightValue.text = "-";

        weigthUnit.text = $"[{ResourceManager.Instance.SystemUnit}]";
    }
}
