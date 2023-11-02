using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManger : MonoBehaviour
{
    [HideInInspector] public int gold;
    // Start is called before the first frame update
    void Start()
    {
        gold = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGold(int gold)
    {
        this.gold = gold;
    }
    public void OnClickGameScene()
    {
        SceneManager.LoadScene("GameScene");
        DontDestroyOnLoad(gameObject);
    }

    public void OnClickLobby()
    {
        GameManager.instance.isPause = false;
        SceneManager.LoadScene("Lobby_GM");
        InventoryManager.Instance.Gold += gold;
        Destroy(gameObject);
    }
}
