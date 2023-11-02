using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
                Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Lobby_GM")
            DOTween.KillAll();
        else if (SceneManager.GetActiveScene().name == "GameScene")
            DOTween.PlayAll();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(1080,1920,true);
    }
}
