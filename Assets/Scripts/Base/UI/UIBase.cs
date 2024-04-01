using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    #region Member
    [SerializeField] protected string typeID;
    [SerializeField] protected bool isInit;
    #endregion

    #region Property
    public string TypeID => typeID;
    public bool IsInit => isInit;
    #endregion

    #region Method : abstract
    protected abstract void Init();
    protected abstract void Deinit();
    protected abstract void Active(bool isActive); 
    #endregion
}
