using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainScene : MonoBehaviour
{
    [SerializeField]
    TextAsset playerStatDatabase;
    [SerializeField]
    TMP_Text[] Gem;
    List<PlayerStatDatabase> statDB = new List<PlayerStatDatabase>();

    void Awake()
    {
        foreach(var TMP in Gem)
            TMP.text = string.Format("{0}", DataController.Instance.gameData.Gem);
        Setting();
    }
    void Setting()
    {
        string[] stat = playerStatDatabase.text.Substring(0, playerStatDatabase.text.Length-1).Split('\n');
        for(int i=1 ; i<stat.Length ; i++)
        {
            string[] row = stat[i].Split('\t');
            statDB.Add(new PlayerStatDatabase(row[0], row[1], row[2]));
        }
        foreach(var data in statDB)
            DataController.Instance._gameData.PlayableData.Add(data.Name, false);
    }

    public void GetPlayerButton()
    {
        GameObject select = EventSystem.current.currentSelectedGameObject;
        int price = int.Parse(select.GetComponentInChildren<TextMeshProUGUI>().text);
        DataController.Instance.gameData.Gem -= price;
        foreach(var TMP in Gem)
            TMP.text = string.Format("{0}", DataController.Instance.gameData.Gem);
        DataController.Instance.gameData.PlayableData[select.name] = true;
        select.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SelectPlayer");
    }
}
