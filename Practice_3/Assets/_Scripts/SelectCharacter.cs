using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCharacter : MonoBehaviour
{
    [SerializeField]
    CharacterStorage characterStorage;
    [SerializeField]
    Image[] CharacterImages;

    void Start()
    {
        for(int i=0 ; i<CharacterImages.Length ; i++)
        {
            string temp;
            if(i+1<10)
                temp = "Player0"+(i+1);
            else
                temp = "Player"+(i+1);
            if(characterStorage.GetPlayable(temp) == IsPlayable.Playable)
                CharacterImages[i].sprite = characterStorage.GetSprite(temp);
            else
            {
                CharacterImages[i].sprite = characterStorage.GetSprite(temp);
                Color color = CharacterImages[i].color;
                color.a = 0.3f;
                CharacterImages[i].color = color;
            }
        }
    }

    public void SetPlayable()
    {
        
    }

}
