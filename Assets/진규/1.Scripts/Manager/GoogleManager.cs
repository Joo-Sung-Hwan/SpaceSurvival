using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;
public class GoogleManager : MonoBehaviour
{
    public static GoogleManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }
    public class GooglePlay
    {
        public TMP_Text _TMP_Google = null;


    }

}
