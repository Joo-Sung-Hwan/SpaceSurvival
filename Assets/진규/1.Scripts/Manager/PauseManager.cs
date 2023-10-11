using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace Singleton
{
    public class PauseManager : MonoBehaviour
    {
        private static PauseManager instance = null;

        [SerializeField] public GameObject pauseUI;

        public static PauseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("PauseManager");
                    obj.AddComponent<PauseManager>();
                    instance = obj.GetComponent<PauseManager>();
                }
                return instance;
            }
            set { instance = value; }
        }

        void Awake() => instance = this;

        void Update()
        {

        }

        public void Pause()
        {
            GameManager.instance.isPause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void EnterResum()
        {
            GameManager.instance.isPause = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        public void EnterReset()
        {

        }

        public void EneterExit()
        {

        }
    }
}
