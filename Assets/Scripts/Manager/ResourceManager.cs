using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : ManagerBase<ResourceManager>
{
    WeightUnit systemUnit = WeightUnit.lb;
    public WeightUnit SystemUnit { get => systemUnit; set => systemUnit = value; }

    public List<MovementType> movementTypeList = new List<MovementType>();
    public Dictionary<MovementType, List<Movement>> movementDic = new Dictionary<MovementType, List<Movement>>();

    protected override void Awake()
    {
        base.Awake();

        var db = new DataStorage();

        var list = db.GetMovementTypeList();
        for (int i = 0; i < list.Count; i++)
        {
            movementTypeList.Add((MovementType)Enum.Parse(typeof(MovementType), list[i]));
        }
        movementDic = db.GetMovementDataDic();

        string log = "";
        foreach (var movementType in movementTypeList)
        {
            log += movementType.ToString() + "\n";
        }
        Debug.Log(log);

        string logDic = "";
        foreach (var item in movementDic)
        {
            logDic += $"===={item.Key}====\n";
            logDic += ListLog(item.Value) + "\n";
        }
        Debug.Log(logDic);
    }

    string ListLog(List<Movement> list)
    {
        string log = "";
        foreach (var item in list)
        {
            log += item.name + "\n";
        }

        return log;
    }
}
