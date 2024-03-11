using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    MovementType type;
    MovementRecord record;
}

public class WOD
{
    WODType type;
}

public struct MovementRecord
{
    public int reps;

    public float weight_lb;
    public float weight_kg;
}

