using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnReplay;


    private void Start()
    {
        btnBack.onClick.AddListener(Back);
        btnResume.onClick.AddListener(Resume);
        btnReplay.onClick.AddListener(Replay);
    }

    void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Resume()
    {
        GameController.instance.pauseUI.gameObject.SetActive(false);
        GameController.instance.isResume = true;
    }

    void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
