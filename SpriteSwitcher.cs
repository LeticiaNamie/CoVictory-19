using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite[] sprites;
    public float interval = 0.1f;

    Image image;
    SpriteRenderer sr;
    float curTime;
    int spriteIdx;

    void Start()
    {
    	Time.timeScale = 1;
        image = GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > interval)
        {
            curTime -= interval;
            spriteIdx = (spriteIdx + 1) % sprites.Length;

            //se a imagem nao for nula, roda como se fosse para imagem
            if (image)
                image.sprite = sprites[spriteIdx];

			//se o sprite nao for nulo, roda como se fosse para ele
            if (sr)
                sr.sprite = sprites[spriteIdx];
        }
    }
}