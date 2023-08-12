using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public AudioSource audioSource;
    public VideoPlayer player;
    public bool videoSkip;
    private void Start()
    {
        player.Prepare();
    }
    public void ChangeScene()
    {
        if (videoSkip)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            audioSource.Stop();
            player.gameObject.SetActive(true);
            player.Play();
            player.loopPointReached += (source) =>
            {
                SceneManager.LoadScene(1);
            };
        }
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            player.Stop();
            SceneManager.LoadScene(1);
        }
    }
}
