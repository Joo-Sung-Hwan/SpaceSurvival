using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Singleton
{
    public class PauseManager : MonoBehaviour
    {
        private static PauseManager instance = null;

        [SerializeField] public GameObject pauseUI;
        [SerializeField] List<Button> buttons = new(); 
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

        // Pause버튼 누르면 게임멈춤
        public void Pause()
        {
            GameManager.instance.gameUI.pauseButton.GetComponent<Image>().material = GameManager.instance.gameUI.material;
            GameManager.instance.isPause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        // 계속하기 누르면 다시 게임진행
        public void EnterResum()
        {
            GameManager.instance.isPause = false;
            pauseUI.SetActive(false);
            GameManager.instance.gameUI.pauseButton.GetComponent<Image>().material = null;
            Time.timeScale = 1;
        }

        // 다시하기 누르면 게임 처음부터 다시 진행
        public void EnterReset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.instance.ReStart();
        }

        // 나가기 누르면 로비로 Scene전환
        public void EneterExit()
        {

        }
    }
}
