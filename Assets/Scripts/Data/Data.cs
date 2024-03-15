using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public MovementType type;
    public MovementRecord record;
    bool hasRecord = false;
}

public class WOD
{
    public WODType type;
}

public struct MovementRecord
{
    public int reps;

    public float weight_lb;
    public float weight_kg;
}

