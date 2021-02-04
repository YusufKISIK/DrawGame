using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnPlane : MonoBehaviour
{
    [SerializeField] Camera drawCam;
    
    private LineRenderer lineRenderer;
    private List<Vector3> linePositions;
    private Vector3 previousPosition;
    private CarPanretCode _carPar;

    void Start()
    {
        _carPar = FindObjectOfType<CarPanretCode>();
        linePositions = new List<Vector3>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = drawCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!linePositions.Contains(hit.point))
                {
                    linePositions.Add(hit.point);
                }

            }
            DrawScreen();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _carPar.DrawingCar(linePositions);
            linePositions.Clear();
            
        }
    }
    void DrawScreen()
    {
        lineRenderer.positionCount = linePositions.Count;
        lineRenderer.SetPositions(linePositions.ToArray());
    }
    

}