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

        // Pause��ư ������ ���Ӹ���
        public void Pause()
        {
            GameManager.instance.gameUI.pauseButton.GetComponent<Image>().material = GameManager.instance.gameUI.material;
            GameManager.instance.isPause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        // ����ϱ� ������ �ٽ� ��������
        public void EnterResum()
        {
            GameManager.instance.isPause = false;
            pauseUI.SetActive(false);
            GameManager.instance.gameUI.pauseButton.GetComponent<Image>().material = null;
            Time.timeScale = 1;
        }

        // �ٽ��ϱ� ������ ���� ó������ �ٽ� ����
        public void EnterReset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.instance.ReStart();
        }

        // ������ ������ �κ�� Scene��ȯ
        public void EneterExit()
        {

        }
    }
}
