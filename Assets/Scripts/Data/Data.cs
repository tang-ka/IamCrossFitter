using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public MovementType type;
    public string name;
    public List<MovementRecord> record = new List<MovementRecord>();
    public bool hasRecord = false;

    public Movement(MovementType type, string name, List<MovementRecord> record = null)
    {
        this.type = type;
        this.name = name;

        if (record == null || record.Count == 0)
            hasRecord = false;
        else
        {
            this.record = record;
            hasRecord = true;
        }
    }
}

public class WOD
{
    public WODType type;
}

public struct MovementRecord
{
    public string id;
    public int reps;

    public float weight_lb;
    public float weight_kg;

    public DateTime date;

    public MovementRecord(int reps, float lb, DateTime date = default)
    {
        this.reps = reps;
        weight_lb = lb;
        weight_kg = (float)Math.Round(lb * 0.453592f, 2);

        this.date = date;

        id = Guid.NewGuid().ToString();
    }
}

public class DataStorage
{
    List<string> movementTypeList = new List<string>();
    Dictionary<MovementType, List<Movement>> movementDataDic = new Dictionary<MovementType, List<Movement>>();

    public DataStorage()
    {
        for (int i = 0; i <= (int)MovementType.Other; i++)
        {
            movementTypeList.Add(((MovementType)i).ToString());
        }

        for (int i = 0; i < movementTypeList.Count; i++)
        {
            MovementType type = (MovementType)Enum.Parse(typeof(MovementType), movementTypeList[i].ToString());
            List<Movement> list = new List<Movement>();
            switch (type)
            {
                case MovementType.Clean:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Power Clean", new List<MovementRecord>()
                        {
                            new MovementRecord(1, 215, new DateTime(2024, 01, 10)),
                            new MovementRecord(1, 225, new DateTime(2023, 12, 13)),
                            new MovementRecord(1, 195, new DateTime(2023, 11, 21)),
                            new MovementRecord(1, 225, new DateTime(2023, 10, 15)),
                            new MovementRecord(3, 155, new DateTime(2023, 9, 1)),
                        }),
                        new Movement(type, "Squat Clean"),
                        new Movement(type, "Hang Power Clean"),
                        new Movement(type, "Hang Squat Clean"),
                        new Movement(type, "Clean Pull"),
                        new Movement(type, "Hang Pull")
                    };
                    break;
                case MovementType.Snatch:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Power Snatch"),
                        new Movement(type, "Squat Snatch"),
                        new Movement(type, "Hang Power Snatch"),
                        new Movement(type, "Hang Squat Snatch"),
                        new Movement(type, "Snatch Pull"),
                        new Movement(type, "Snatch Balance")
                    };
                    break;
                case MovementType.Jerk:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Push Jerk"),
                        new Movement(type, "Split Jerk")
                    };
                    break;
                case MovementType.Deadlift:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Deadlift", new List<MovementRecord>()
                        {
                            new MovementRecord(1, 325, new DateTime(2024, 01, 20)),
                        }),
                        new Movement(type, "Snatch Deadlift")
                    };
                    break;
                case MovementType.Squat:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Back Squat"),
                        new Movement(type, "Front Squat", new List<MovementRecord>()
                        {
                            new MovementRecord(1, 255, new DateTime(2024, 03, 10)),
                            new MovementRecord(10, 195, new DateTime(2024, 02, 10)),
                        }),
                        new Movement(type, "Overhead Squat"),
                        new Movement(type, "Split Barbell Squat")
                    };
                    break;
                case MovementType.Press:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Bench Press"),
                        new Movement(type, "Overhead Press"),
                        new Movement(type, "Push Press"),
                        new Movement(type, "Snatch Push Press")
                    };
                    break;
                case MovementType.Gymnastic:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Stric Pull-up"),
                        new Movement(type, "Keeping Pull-up"),
                        new Movement(type, "Butterfly Pull-up"),
                        new Movement(type, "Toes To Bar"),
                        new Movement(type, "Muscle-up"),
                        new Movement(type, "Ring Muscle-up")
                    };
                    break;
                case MovementType.Machine:
                    list = new List<Movement>()
                    {
                        new Movement(type, "Row"),
                        new Movement(type, "Run"),
                        new Movement(type, "Ski"),
                        new Movement(type, "Bike"),
                        new Movement(type, "Assault Bike")
                    };
                    break;
                case MovementType.Other:
                    break;
            }

            movementDataDic.Add(type, list);
        }
    }

    public List<string> GetMovementTypeList()
    {
        return movementTypeList;
    }

    public Dictionary<MovementType, List<Movement>> GetMovementDataDic()
    {
        return movementDataDic;
    }

}
