using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField] private bool isEndWaypoint;


    public Waypoint NextWaypoint { get => nextWaypoint; set => nextWaypoint = value; }
    public bool IsEndWaypoint { get => isEndWaypoint; set => isEndWaypoint = value; }


}
