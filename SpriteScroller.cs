using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
	//vai multiplicar pelo scrollspeed (?)
	public float SpeedScale = 1f;

	float spriteWidth;

    void Start()
    {
    	Time.timeScale = 1;
        GameObject rightGO = Instantiate<GameObject>(gameObject, transform);
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;        
        Destroy(rightGO.GetComponent<SpriteScroller>());
        spriteWidth = sprite.bounds.size.x;
        rightGO.transform.localPosition = new Vector3(spriteWidth - 1, 0, 0);
    }

    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        //pega a posição do chão
        Vector3 curPos = transform.position;
        
        //instancia mais chao
        curPos.x -= GameManager.Instance.ScrollSpeed * SpeedScale * Time.deltaTime;

        //chao se desloca para esquerda
        curPos.x = curPos.x % (spriteWidth - 1);

        transform.position = curPos;
    }
}
