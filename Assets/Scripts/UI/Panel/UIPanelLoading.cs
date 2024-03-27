using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLoading : UIPanel_old
{
    [SerializeField] List<Transform> titleText = new List<Transform>();
    [SerializeField] Image imgLoadging;
    [SerializeField] Text txtLoading;

    string animationText = "";
    readonly string defaultText = "Loading";

    float curTime = 0;
    float timeCap = 0.3f;

    bool isFinished = false;

    protected override void Init()
    {
        InitializeManager.Instance.onCompleteInitialize += (isSucess) => isFinished = true;

        animationText = defaultText;
        txtLoading.text = animationText;
        base.Init();
    }

    public override void Activate(bool isActive)
    {
        base.Activate(isActive);

        if (isActive)
        {
            UniTask.WaitUntil(() =>
            {
                curTime += Time.deltaTime;
                if (curTime > timeCap)
                {
                    TextAnimation();
                    curTime = 0;
                }

                txtLoading.text = $"{animationText}  {InitializeManager.Instance.Progress.ToString("F2")}%";
                return isFinished;
            });
        }
        else
        {
            curTime = 0;
            animationText = "";
            txtLoading.text = "";
        }
    }



    private void TextAnimation()
    {
        if (animationText.Length == (defaultText.Length + 3))
            animationText = defaultText;
        else
            animationText += '.';
    }
}
