using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory
{
    Unit GetUnit(Unit[] unit, string name, int defaultPoolSize)
    {
        Unit returnUnit = CreateUnit(unit, name, defaultPoolSize);
        return returnUnit;
    }
    public abstract Unit CreateUnit(Unit[] unit, string name, int defaultPoolSize);
}
