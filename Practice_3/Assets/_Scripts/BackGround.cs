using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    float speed;
    int startIndex=2;
    int endIndex=0;
    float viewWidth;
    [SerializeField]
    Transform[] sprites;
    bool isMoving = true;
    void Awake()
    {
        viewWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }

    public void checkMove(bool check)
    {
        isMoving = check;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
            return;
        Vector3 curPosition = transform.position;
        Vector3 nextPosition = Vector3.left * speed * Time.deltaTime;
        transform.position = curPosition + nextPosition;

        if(sprites[endIndex].position.x < viewWidth*(-1))
        {
            Vector3 backSpritePosition = sprites[startIndex].localPosition;
            Vector3 frontSpritePosition = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePosition + Vector3.right * viewWidth * 0.9f;

            int temp = startIndex;
            startIndex = endIndex;
            endIndex = (temp-1 == -1)? sprites.Length-1 : temp-1;
        }
    }
}