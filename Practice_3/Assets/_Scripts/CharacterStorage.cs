using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName ="Scriptable Object/CharacterStorage")]
public class CharacterStorage : ScriptableObject
{
    [SerializeField]
    CharacterSrc[] characterSrcs;
    Dictionary<string, Sprite> dicCharacter = new Dictionary<string, Sprite>();

    void GenerateDictionary() 
    {
        for (int i = 0; i < characterSrcs.Length; i++)
        {
            dicCharacter.Add(characterSrcs[i].GetName, characterSrcs[i].GetSprite);
        }
    }
    
    public Sprite GetSprite(string name) 
    {
        Debug.Assert(characterSrcs.Length > 0, "No Character data!");
        if(dicCharacter.Count == 0)
        {
            GenerateDictionary();
        }
        return dicCharacter[name];
    }
}
[Serializable]
public struct CharacterSrc
{
    [SerializeField]
    string name;
    [SerializeField]
    Sprite sprite;

    public string GetName => name;
    public Sprite GetSprite => sprite;
}