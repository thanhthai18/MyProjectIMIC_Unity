using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private SpriteRenderer Menu;
    private int ScaleUp;

    private void Start()
    {
        ScaleUp = -1;
        btnPlay = Menu.GetComponentInChildren<Button>();
        btnPlay.onClick.AddListener(GameScene);
        SetSizeCamera();
        //ScaleButton(); // ko hoat dong khi load lai scene vi DOTween

    }

    void GameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        cameraMain.orthographicSize *= f1 / f2;
    }

    private void Update()
    {
        if (ScaleUp == 1)
        {
            btnPlay.transform.localScale += new Vector3(0.0001f , 0.0001f , 0.0001f );
        }

        if (ScaleUp == 0)
        {
            btnPlay.transform.localScale -= new Vector3(0.0001f, 0.0001f, 0.0001f);
        }

        if (btnPlay.transform.localScale.x > 0.33f)
        {
            ScaleUp = 0;
        }

        if (btnPlay.transform.localScale.x <= 0.3f)
        {
            ScaleUp = 1;
        }




    }
}
