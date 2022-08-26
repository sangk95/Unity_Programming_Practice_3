using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class SelectCharacter : MonoBehaviour
{
    [SerializeField]
    CharacterStorage characterStorage;
    [SerializeField]
    Image[] characterImages;
    [SerializeField]
    Image[] selectedCharacter;
    Sprite selectedImage;
    Sprite noneImage;
    RaycastHit2D hit;

    public Action<List<string>> StartGame;

    void Start()
    {
        for(int i=0 ; i<characterImages.Length ; i++)
        {
            string temp;
            if(i+1<10)
                temp = "Player0"+(i+1);
            else
                temp = "Player"+(i+1);
            if(DataController.Instance.gameData.PlayableData[temp] == true)
                characterImages[i].sprite = characterStorage.GetSprite(temp);

            else
            {
                characterImages[i].sprite = characterStorage.GetSprite(temp);
                Color color = characterImages[i].color;
                color.a = 0.3f;
                characterImages[i].color = color;
            }
            characterImages[i].sprite.name = temp;
        }
        foreach(var image in selectedCharacter)
        {
            noneImage = image.sprite;
            break;
        }
    }

    public void SelectedCharacterImage()
    {
        GameObject select = EventSystem.current.currentSelectedGameObject;
        foreach(var image in characterImages)
        {
        if(DataController.Instance.gameData.PlayableData[image.sprite.name] == true && image.gameObject == select)
            {
                selectedImage = select.GetComponent<Image>().sprite;
                break;
            }
        }
    }

    public void ChangeSelectedCharacter()
    {
        GameObject select = EventSystem.current.currentSelectedGameObject;
        if(selectedImage != null)
        {
            foreach(var image in selectedCharacter)
            {
                if(image.sprite == selectedImage)
                {
                    image.sprite = noneImage;
                    break;
                }
            }
            select.GetComponent<Image>().sprite = selectedImage;
            selectedImage = null;
        }
        else
            select.GetComponent<Image>().sprite = noneImage;
    }

    public void GoToInGame()
    {
        List<string> list = new List<string>();
        foreach(var data in selectedCharacter)
            list.Add(data.sprite.name);
        StartGame?.Invoke(list);
    }
}
