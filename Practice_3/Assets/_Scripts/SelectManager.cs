using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    [SerializeField]
    SelectCharacter selectCharacter;
    List<string> characterList;
    public List<string> GetCharacterList => characterList;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var obj = FindObjectsOfType<SelectManager>();

        if (obj.Length == 1) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        selectCharacter.StartGame += this.SetCharacterList;
    }
    void SetCharacterList(List<string> list)
    {
        selectCharacter.StartGame -= this.SetCharacterList;
        characterList = list;

        SceneManager.LoadScene("InGame");
    }
}
