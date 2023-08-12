using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public VideoPlayer player;
    public bool videoSkip;
    public void ChangeScene()
    {
        if (videoSkip)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            player.gameObject.SetActive(true);
            player.Play();
            player.loopPointReached += (source) =>
            {
                SceneManager.LoadScene(1);
            };
        }
    }
    private void Start()
    {
        player.Prepare();
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
