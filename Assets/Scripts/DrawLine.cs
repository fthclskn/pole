using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public List<Vector2> fingerPositions;
    public Camera cam;
    public DetectShapes shapesDetector;
    


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
           
            Vector2 tempFingerPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
                shapesDetector.lineLength = lineRenderer.positionCount;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
            shapesDetector.CheckPointMethod();
            Destroy(currentLine);
        }
    }
    void CreateLine()
    {
        Time.timeScale = 0.2f;
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
  
        fingerPositions.Clear();
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        shapesDetector.LinePrefab = lineRenderer;
       
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

    
}
