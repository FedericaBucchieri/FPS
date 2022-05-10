using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine(points);
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
            lr.SetPosition(i, points[i].position);
    }

    public void RemoveAt(int index)
    {
        Debug.Log("Removing in position " + index);

        for (int a = index; a < points.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            points[a] = points[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref points, points.Length - 1);

       SetUpLine(points);
    }

    public void AddAt(int index, Transform newPoint)
    {
        Debug.Log("Adding in position " + index + " the point " + newPoint.gameObject.name);

        // let's increment Array's size by one
        Array.Resize(ref points, points.Length + 1);

        for (int a = points.Length -1 ; a > index; a--)
        {
            // moving elements downwards, to fill the gap at [index]
            points[a] = points[a - 1];
        }

        // insert the new value
        points[index] = newPoint;

        SetUpLine(points);
    }


    public void CheckWaypoint(Transform waypoint)
    {
        int pos = Array.IndexOf(points, waypoint);

        // if the waypoint is already in the list
        if (pos > -1)
            RemoveAt(pos);
        else
            AddAt(1, waypoint); // 1 = always after the Player in position 0

    }

}
