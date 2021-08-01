using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Square> square = new List<Square>();
    [SerializeField] private List<FadeFX> line = new List<FadeFX>();
    private int CountSquareComplete;
    private Tween tut;


    private void Start()
    {
        line = GetComponentsInChildren<FadeFX>().ToList();
        Invoke("ShowTutorial", 0.5f);

        for (int i = 0; i < square.Count; i++)
        {
            if (square[i].CompareTag("Item"))
            {
                square[i].angle += 90;
                square[i].Rotation();
                GameController.instance.fullStep++;
            }
            else
            {
                int r = Random.Range(1, 4);
                float a = r * 90;
                square[i].angle += a;
                square[i].Rotation();
                GameController.instance.fullStep += (4 - r);
            }
        }
    }

    void ShowTutorial()
    {
        var index = GameController.instance.tutorialIndex;
        if (index == 0 || index == 1 || index == 2)
        {
            GameController.instance.tutorial.transform.position = square[0].transform.position;
            GameController.instance.tutorial.gameObject.SetActive(true);
            tut = GameController.instance.tutorial.transform.DOScale(1.2f, 1f).OnComplete(() =>
            {
                GameController.instance.tutorial.transform.DOScale(0.9f, 0.5f);
            });
            tut.SetLoops(-1);
        }
    }

    void CallMove()
    {
        GameController.instance.MoveLadybug();
    }

    private void Update()
    {
        for (int i = 0; i < square.Count; i++)
        {
            if (square[i].CompareTag("Item"))
            {
                if (square[i].angle % 180 == 0)
                {
                    square[i].isComplete = true;
                }
                else
                {
                    square[i].isComplete = false;
                }
            }
            if (square[i].CompareTag("Enemy"))
            {
                if (square[i].angle % 360 == 0)
                {
                    square[i].isComplete = true;
                }
                else
                {
                    square[i].isComplete = false;
                }
            }
        }

        for (int j = 0; j < square.Count; j++)
        {
            if (square[j].isComplete)
            {
                if (!square[j].isLock)
                {
                    CountSquareComplete++;
                    square[j].isLock = true;
                }
            }
            else
            {
                if (square[j].isLock)
                {
                    CountSquareComplete--;
                    square[j].isLock = false;
                }
            }
        }

        if (CountSquareComplete == square.Count)
        {
            GameController.instance.isNextLv = true;
            for (int i = 0; i < line.Count; i++)
            {
                line[i].FadeFx();
            }

            if (GameController.instance.countWin <= 3)
            {
                GameController.instance.countWin++;
                Debug.Log(GameController.instance.countWin);
                CountSquareComplete = 0;
            }
            if (GameController.instance.countWin <= 4)
            {
                Invoke("CallMove", 1.8f);
            }
        }
    }
}