using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    public bool IsInit { get; protected set; }

    protected abstract void Init();
    public abstract void Reset();
}
