﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllWaypointThisLevel : MonoBehaviour
{

    [SerializeField] public List<Waypoint> _listWaypoint = new List<Waypoint>();
    public Vector3[] road;
    public List<Waypoint> ListWaypoint { get => _listWaypoint; set => _listWaypoint = value; }


    private void Start()
    {
        SetupWaypoints();
        road = new Vector3[_listWaypoint.Count];
        for (int i = 0; i < _listWaypoint.Count; i++)
        {
            road[i] = _listWaypoint[i].transform.position;
        }      
    }

    // Ham noi cac diem
    [ContextMenu("SetupWaypoints")]
    private void SetupWaypoints()
    {
        //Lay cac component co kieu Waypont

        ListWaypoint = GetComponentsInChildren<Waypoint>().ToList();
        for (int waypointIndex = 0; waypointIndex < ListWaypoint.Count - 1; waypointIndex++)
        {
            var nextIndex = waypointIndex + 1;
            var nexWaypoint = ListWaypoint[nextIndex];
            var currentWaypoint = ListWaypoint[waypointIndex];
            currentWaypoint.NextWaypoint = nexWaypoint;
        }
        ListWaypoint[ListWaypoint.Count - 1].IsEndWaypoint = true;
    }
}
