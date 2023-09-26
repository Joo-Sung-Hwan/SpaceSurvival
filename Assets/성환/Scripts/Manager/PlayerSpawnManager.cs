using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public Player player_prefab;
    public Transform parent;
    public Transform tmp_bullet_parent;
    public Transform tmp_bomb_parent;
    public Transform tmp_enemy_parent;

    [HideInInspector] public Player player;
    // Start is called before the first frame update
    void Start()
    {
        Create_Player();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create_Player()
    {
        player = Instantiate(player_prefab, parent);
    }
}
