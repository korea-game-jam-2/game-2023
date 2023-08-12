using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image[] image;

    int count = 0;
    void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeInStart());
        StartCoroutine(SpawnPuzzle());
    }

    public IEnumerator SpawnPuzzle()
    {
        yield return new WaitForSeconds(0.1f); 
        for (float f = 0f; f < 1; f += 0.02f)
        {
            image[count].GetComponent<CanvasGroup>().alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
        image[count].gameObject.SetActive(true);
        count++;
    }
    //페이드 아웃
    public IEnumerator FadeInStart()
    {
        for (float f = 0f; f < 1; f += 0.02f)
        {
            canvasGroup.alpha = f;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOutStart());
        yield break;
    }

    //페이드 인
    public IEnumerator FadeOutStart()
    {
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            canvasGroup.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
        canvasGroup.alpha = 0;
        this.gameObject.SetActive(false);
        yield break;
    }
}
