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
    public IsPlayable GetPlayable(string name)
    {
        foreach(var data in characterSrcs)
        {
            if(name == data.GetName)
                return data.GetPlayable;
        }
        return IsPlayable.NonPlayable;
    }
    public void SetPlayable(string name)
    {
        foreach(var data in characterSrcs)
        {
            if(name == data.GetName)
            {
                data.SetPlayable(IsPlayable.Playable);
                break;
            }
        }
    }
}
[Serializable]
public struct CharacterSrc
{
    [SerializeField]
    string name;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    IsPlayable isPlayable; 

    public string GetName => name;
    public Sprite GetSprite => sprite;
    public IsPlayable GetPlayable => isPlayable;
    public void SetPlayable(IsPlayable isPlayable){this.isPlayable = isPlayable;}
}
public enum IsPlayable
{
    Playable, NonPlayable
} 