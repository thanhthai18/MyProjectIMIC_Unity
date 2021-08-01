using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Singleton
    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(instance);
        }
    }
    //end Singleton


    [SerializeField] private List<Map> map = new List<Map>();
    [SerializeField] private Map currentMap;
    [SerializeField] private Ladybug ladyBug;
    [SerializeField] private WaypointController waypointController;
    [SerializeField] private Transition transition;
    [SerializeField] private Button btnPause;
    
    private List<Map> mapRandom = new List<Map>();
    private List<Map> mapFirstPlay = new List<Map>();
    private Ladybug currentLadybug;
    public SpriteRenderer tutorial;
    public Camera cameraMain;
    public Canvas pauseUI;
    private int[] numOfRandom;
    public int index_NumOfRandom;
    public int level;
    public int countWin;
    public bool isNextLv;
    public bool isWin = false;
    public bool isBegin;  
    public int fullStep;
    public int myStep;
    public int tutorialIndex;
    public bool isResume;




    private void Start()
    {
        btnPause.onClick.AddListener(Pause);
        transition.gameObject.SetActive(true);
        isBegin = true;
        isNextLv = false;
        isResume = true;
        tutorial.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);

        for (int i = 0; i < map.Count; i++)
        {
            map[i].gameObject.SetActive(false);
        }
        fullStep = 0;
        myStep = 0;
        index_NumOfRandom = 0;
        level = 0;
        countWin = 1;
        tutorialIndex = -1;
        SetUpMap();
        SetSizeCamera();
    }

    void Pause()
    {
        isResume = false;
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        cameraMain.orthographicSize *= f1 / f2;
    }

    public void SetUpMap()
    {
        int ran1 = Random.Range(0, 3);
        int ran2 = Random.Range(3, 6);
        int ran3 = Random.Range(6, 9);
        mapRandom.Add(map[ran1]);
        mapRandom.Add(map[ran2]);
        mapRandom.Add(map[ran3]);
        numOfRandom = new int[3] { ran1, ran2, ran3 };
        isWin = false;
        tutorialIndex = ran1;

        for (int i = 0; i < mapRandom.Count; i++)
        {
            mapRandom[i].gameObject.SetActive(false);
        }
        currentMap = mapRandom[level];
        currentMap.gameObject.SetActive(true);
        SpawnLadyBug();
    }

    void SpawnLadyBug()
    {
        for (int i = 0; i < waypointController.transform.childCount; i++)
        {
            if (i == numOfRandom[index_NumOfRandom])
            {
                waypointController.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
                waypointController.transform.GetChild(i).gameObject.SetActive(false);
        }

        ladyBug.waypointThisLevel = waypointController.allWaypointThisLevel[numOfRandom[index_NumOfRandom]];
        currentLadybug = Instantiate(ladyBug);
    }

    public void MoveLadybug()
    {
        currentLadybug.Move();
    }

    public void SetNextLv()
    {
        level++;
        index_NumOfRandom++;
        NextLevel(level, index_NumOfRandom);
    }

    public void NextLevel(int level, int index)
    {
        currentMap.gameObject.SetActive(false);
        mapRandom[level].gameObject.SetActive(true);
        currentMap = mapRandom[level];
        SpawnLadyBug();
    }

    public void TransitionLevelStart()
    {
        transition.LoadTransitionStart();
    }

    public void TransitionLevelEnd()
    {
        transition.LoadTransitionEnd();
    }

    void PostMenuWin()
    {
        if ((myStep - fullStep) < 4)
        {
            starFxController.ea = 3;
            AnimationPopup.instance.ShowPopWinGame();
        }
        if ((myStep - fullStep) >= 4 && (myStep - fullStep) <= 7)
        {
            starFxController.ea = 2;
            AnimationPopup.instance.ShowPopWinGame();
        }
        if ((myStep - fullStep) > 7)
        {
            starFxController.ea = 1;
            AnimationPopup.instance.ShowPopWinGame();
        }
    }


    private void Update()
    {
        if (countWin == 4)
        {
            isWin = true;
        }

        if (isWin)
        {
            countWin = -1;
            isWin = false;
            isBegin = false;
            Invoke("PostMenuWin", 5.5f);
        }

        if (isResume)
        {
            Time.timeScale = 1;
        }
    }
}
