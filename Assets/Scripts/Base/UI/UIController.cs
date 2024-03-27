using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    [SerializeField] protected bool isInit;

    public bool IsInit => isInit;

    protected abstract void Init();
    protected abstract void Deinit();
}
