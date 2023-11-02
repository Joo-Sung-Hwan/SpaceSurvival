using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class DotweenManager : MonoBehaviour
{
    public static DotweenManager instance;

    void Awake() => instance = this;

    void Start()
    {
        
    }
}
