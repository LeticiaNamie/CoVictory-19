using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    Image image;
    public float duration = 0.7f;

    void Start()
    {
    	Time.timeScale = 1;
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        image.enabled = true;

        image.DOFade(0, duration)
            .OnComplete(() => { image.enabled = false; });
    }

    public void FadeToGame()
    {
        image.enabled = true;
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1, duration)
            .OnComplete(() => { SceneManager.LoadScene("Instructions"); });
    }

    public void FadeToMenu()
    {
        image.enabled = true;
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1, duration)
            .OnComplete(() => { GameManager.Instance.GameOver = false; SceneManager.LoadScene("Menu"); });
    }

    public void Flash()
    {
        image.enabled = true;
        image.color = new Color(1, 1, 1, 0);
        image.DOFade(1, 0.1f)
            .SetLoops(2, LoopType.Yoyo);
    }
}
