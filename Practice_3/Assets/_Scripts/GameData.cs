using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class GameData
{
    public Dictionary<string, bool> PlayableData = new Dictionary<string, bool>();
    public int Gem=999;
}
