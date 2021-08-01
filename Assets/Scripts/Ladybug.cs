using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    public AllWaypointThisLevel waypointThisLevel;
    private float speed = 11f;
    private Tween ladybugMovement;
    private Vector3[] roadLadybug;
    private int angleZ;


    private void Start()
    {
        angleZ = 0;
        if (GameController.instance.isBegin)
        {
            Invoke("SetPos", 0.1f);
        }
    }

    public void SetPos()
    {
        transform.position = waypointThisLevel.road[0];
    }

    public void Move()
    {
        roadLadybug = waypointThisLevel.road;
        float distance = 0;
        for (int i = 1; i < roadLadybug.Length; i++)
        {
            distance += (roadLadybug[i - 1] - roadLadybug[i]).magnitude;
        }

        //anim

        ladybugMovement = transform.DOPath(roadLadybug, distance / speed, PathType.Linear).SetEase(Ease.Linear);
        ladybugMovement.OnComplete(() =>
        {
            if (GameController.instance.countWin <= 3)
            {
                if (GameController.instance.level < 2)
                {
                    Invoke("LevelUp", 1f);
                    Destroy(gameObject, 1f);
                    GameController.instance.TransitionLevelStart();
                    Invoke("PlayTransitionEnd", 0.933f);
                    //anim
                }
            }
            if(GameController.instance.countWin == -1)
            {
                //anim
            }

            GameController.instance.isNextLv = false;
            ladybugMovement.Kill();
        });
    }

    void LevelUp()
    {
        GameController.instance.SetNextLv();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            angleZ += 90;
            gameObject.transform.DORotate(new Vector3(0, 0, angleZ), 0.2f);
        }
        if (collision.gameObject.CompareTag("Toy"))
        {
            angleZ -= 90;
            gameObject.transform.DORotate(new Vector3(0, 0, angleZ), 0.2f);
        }
    }

    void PlayTransitionEnd()
    {
        GameController.instance.TransitionLevelEnd();
    }


}




