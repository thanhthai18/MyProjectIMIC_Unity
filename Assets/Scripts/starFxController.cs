using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starFxController : MonoBehaviour
{

    public GameObject[] starFX;
    public static int ea;
    public int currentEa;
    public float delay;
    public float currentDelay;
    public bool isEnd;
    public int idStar;
    public static starFxController myStarFxController;
    public List<ParticleSystem> star = new List<ParticleSystem>();
    public ParticleSystem s1, s2, s3;

    void Awake()
    {
        myStarFxController = this;
    }

    void Start()
    {

        isEnd = true;
        s1 = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        s2 = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        s3 = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();

        star.Add(s1);
        star.Add(s2);
        star.Add(s3);
    }

    void Update()
    {

        if (!isEnd)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                if (currentEa != ea)
                {
                    currentDelay = delay;

                    starFX[currentEa].SetActive(true);
                    star[currentEa].Play();
                    currentEa++;
                }
                else
                {
                    isEnd = true;
                    currentDelay = delay;
                    currentEa = 0;
                }
            }
        }
    }
}