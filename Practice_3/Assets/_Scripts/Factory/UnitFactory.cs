using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory
{
    public Unit GetUnit(Unit[] unit, string name)
    {
        Unit returnUnit = CreateUnit(unit, name);
        return returnUnit;
    }
    public abstract Unit CreateUnit(Unit[] unit, string name);
}
