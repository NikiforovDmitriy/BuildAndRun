using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LinesRender : MonoBehaviour
{
    public event Action<List<Vector3>> OnNewPathCreated = delegate { };
    private LineRenderer lineRenderer;
    private Vector3 mousePointer;
    private Vector3 pos;
    private float minimumDistance = 0.01f;
    private List<Vector3> points = new List<Vector3>();

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private float DistanceToLastPoint(Vector2 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector2.Distance(points.Last(), point);
    }

    public void DrawLine()
    {
        mousePointer = Input.mousePosition;
        mousePointer.z = 10;
        pos = Camera.main.ScreenToWorldPoint(mousePointer);
        points.Add(pos);
        Render();
        if (DistanceToLastPoint(new Vector2(pos.x, pos.y)) > minimumDistance)
        {

        }
    }

    private void Render()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void EndOfRender()
    {
        OnNewPathCreated(points);
        points.Clear();
        Render();
    }
}
