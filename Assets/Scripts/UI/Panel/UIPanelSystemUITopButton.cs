using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSystemUITopButton : UIPanel
{
    [SerializeField] Button btnBack;
    [SerializeField] Button btnSetting;

    protected override void Init()
    {
        btnBack.onClick.AddListener(() => WorldManager.Instance.ReturnToPreState());
        btnSetting.onClick.AddListener(async () =>
        {
            UIManager.Instance.OpenPage(PageType.Setting);
            await UniTask.Delay(1500);
            UIManager.Instance.ClosePage(PageType.Setting);
        });

        base.Init();
    }

    public void OnClickBack()
    {

    }

    public override void Activate()
    {
        base.Activate();
    }
}
