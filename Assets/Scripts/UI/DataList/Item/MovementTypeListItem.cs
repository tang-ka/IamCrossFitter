using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MovementTypeListItem : ToggleListItem<MovementType>
{
    [SerializeField] Text title;
    [SerializeField] RectTransform background;
    RectTransform rectTransform;

    protected override async void Start()
    {
        base.Start();

        rectTransform = GetComponent<RectTransform>();
        await UniTask.WaitUntil(() => title.text.Length > 0);
        rectTransform.sizeDelta = new Vector2(background.rect.width, rectTransform.rect.height);

        if (data == MovementType.Clean)
            toggle.isOn = true;
    }

    public override void SetData(MovementType data, UIPanel panel)
    {
        base.SetData(data, panel);
        
        title.text = data.ToString();
    }

    protected override void SelectItem()
    {
        //Debug.Log($"Select {data}");

        SetColor(ColorDefine.SELECT_COLOR, ColorDefine.SELECT_COLOR);
        ((parentPanel as UIPanelMovementTypeList).ParentPage as UIPageMovementList).SelectType(data);
    }

    protected override void DeselectItem()
    {
        //Debug.Log($"Deselect {data}");

        SetColor(ColorDefine.WHITE_COLOR, ColorDefine.WHITE_COLOR);
    }

    private void SetColor(Color titleColor, Color backgroundColor)
    {
        title.color = titleColor;
        background.GetComponent<Image>().color = backgroundColor;
    }
}
