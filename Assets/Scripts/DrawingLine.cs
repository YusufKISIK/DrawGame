using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public List<Vector2> fingerPosition;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            createLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos,fingerPosition[fingerPosition.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    void createLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPosition.Clear();
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPosition[0]);
        lineRenderer.SetPosition(1, fingerPosition[1]);
    }

    void UpdateLine(Vector2 newFingerPosition)
    {
        fingerPosition.Add(newFingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPosition);
    }
}
