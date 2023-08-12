using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<SavePoint> savePoints;
    public PlayerController playerController;
    public List<PlayableDirector> stageTransition;

    private SavePoint _currentSavePoint;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if(savePoints.Count > 0)
        {
            _currentSavePoint = savePoints.First();
        }
    }
    private void Start()
    {
        StartGame();
    }
    public void SetSavePoint(SavePoint savePoint, int order) {
        if (_currentSavePoint.order <= order) { 
            _currentSavePoint = savePoint;
        }
    }
    public void StartGame() {
        ShowCinematic(0);
    }
    public void Respawn() { 
        playerController.ResetState();
        playerController.transform.SetParent(null);
        playerController.transform.position = _currentSavePoint.transform.position;
    }
    public void ShowCinematic(int index) {
        playerController.SetFreeze(true);

        stageTransition[index].stopped += (_)=>playerController.SetFreeze(false);
        stageTransition[index].Play();
    }
}
