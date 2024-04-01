using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase_old : MonoBehaviour
{
    [SerializeField] protected string typeID;

    public virtual string TypeID => typeID;

    public bool IsInit { get; protected set; }

    protected abstract void Init();
    public abstract void Reset();
    public abstract void Activate(bool isActive);
}
