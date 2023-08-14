using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{
    public Rigidbody2D player;
    public CanvasGroup canvasGroup;
    public Image[] image;

    int count = 0;

    void OnEnable()
    {
        // player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeInStart());
        StartCoroutine(SpawnPuzzle());
    }

    public IEnumerator SpawnPuzzle()
    {
        yield return new WaitForSecondsRealtime(0.1f); 
        for (float f = 0f; f < 1; f += 0.02f)
        {
            image[count].GetComponent<CanvasGroup>().alpha = f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        image[count].gameObject.SetActive(true);
        count++;
    }
    //���̵� �ƿ�
    public IEnumerator FadeInStart()
    {
        for (float f = 0f; f < 1; f += 0.005f)
        {
            canvasGroup.alpha = f;
            // if (f > .3f) player.bodyType = RigidbodyType2D.Static;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(FadeOutStart());
        yield break;
    }

    //���̵� ��
    public IEnumerator FadeOutStart()
    {
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            canvasGroup.alpha = f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        canvasGroup.alpha = 0;
        // player.bodyType = RigidbodyType2D.Dynamic;
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
        yield break;
    }
}
