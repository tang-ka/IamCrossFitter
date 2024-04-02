using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementListItem : ToggleListItem<Movement>
{
    [SerializeField] Image background;
    [SerializeField] Text title;
    [SerializeField] Image recordBackground;
    [SerializeField] Text recordValue;

    [SerializeField] GameObject record;

    public override void SetData(Movement data, UIPanel panel)
    {
        base.SetData(data, panel);

        title.text = data.name;
        record.SetActive(data.hasRecord);
        recordValue.text = data.record.Find(x => x.reps.Equals(1)).weight_lb.ToString();
        gameObject.name = $"MovementListItem - {data.name}";
    }

    protected override void SelectItem()
    {
        base.SelectItem();

        SetColor(ColorDefine.BLACK15_COLOR, ColorDefine.SELECT_COLOR);
    }

    protected override void DeselectItem()
    {
        base.DeselectItem();

        SetColor(ColorDefine.WHITE_COLOR, ColorDefine.BLACK20_COLOR);
    }

    private void SetColor(Color titleColor, Color backgroundColor)
    {
        title.color = titleColor;
        background.GetComponent<Image>().color = backgroundColor;
    }
}
