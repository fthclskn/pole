using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PoleRenderer : MonoBehaviour
{
	[SerializeField] public float maxSize = 4.0f;
	[SerializeField] public int segmentCount = 4;
	[SerializeField] public int verticesCount = 16;

	
	private float _currentSize;
	private int _currentSegment = 0;

	private void Start()
	{
		
	}

	public void IncreasePoleSize ()
	{
		if (IsMaxLength())
		{
			return;
		}
		
		_currentSize += maxSize / segmentCount; // currentSize++; 0->1
		_currentSegment++; //0->1
		InitPole();
	}

	public void InitPole ()
	{
		GenerateMesh(_currentSize);
		UpdateBounds();
	}

	public float GetSize()
	{
		return _currentSize;
	}

	public float GetMaxSize()
	{
		return maxSize;
	}

	private bool IsMaxLength()
	{
		return _currentSegment == segmentCount;
	}

	//Procedural mesh generation
	void UpdateBounds()
	{
		var mesh = GetComponent<MeshRenderer>();
		var center = new Vector3(0, (_currentSize / 2f) - 0.1f, 0);
		GetComponent<BoxCollider>().center = center;
		GetComponent<BoxCollider>().size = mesh.bounds.size;
	}

	void GenerateMesh(float size)
	{
		var mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = GenerateVertices(size, verticesCount);
		mesh.triangles = GenerateTriangles(mesh.vertices.Length);
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

    Vector3[] GenerateVertices(float maxLength, int count)
    {
	    List<Vector3> vertices = new List<Vector3>();

	    for (int i = 0; i < count; i++)
	    {
		    Vector3[] points = GetVerticesPoints(i, maxLength / count);
		    
		    vertices.Add(points[0]);
		    vertices.Add(points[1]);
		    vertices.Add(points[2]);
		    vertices.Add(points[3]);
		    
		    vertices.Add(points[7]);
		    vertices.Add(points[4]);
		    vertices.Add(points[0]);
		    vertices.Add(points[3]);
		    
		    vertices.Add(points[4]);
		    vertices.Add(points[5]);
		    vertices.Add(points[1]);
		    vertices.Add(points[0]);
		    
		    vertices.Add(points[6]);
		    vertices.Add(points[7]);
		    vertices.Add(points[3]);
		    vertices.Add(points[2]);
		    
		    vertices.Add(points[5]);
		    vertices.Add(points[6]);
		    vertices.Add(points[2]);
		    vertices.Add(points[1]);
		    
		    vertices.Add(points[7]);
		    vertices.Add(points[6]);
		    vertices.Add(points[5]);
		    vertices.Add(points[4]);
	    }

	    return vertices.ToArray();
    }

    Vector3[] GetVerticesPoints(int y, float length)
    {
	    float cubeWidth = 0.2f;
	    float cubeHeight = length;
	    float cubeLength = 0.2f;
	    
	    return new[]
	    {
		    new Vector3( -cubeWidth * .5f, -cubeHeight * .5f + (cubeHeight * y), cubeLength * .5f),
		    new Vector3( cubeWidth * .5f, -cubeHeight * .5f + (cubeHeight * y), cubeLength * .5f),
		    new Vector3( cubeWidth * .5f, -cubeHeight * .5f + (cubeHeight * y), -cubeLength * .5f),
		    new Vector3( -cubeWidth * .5f, -cubeHeight * .5f + (cubeHeight * y), -cubeLength * .5f),
		    new Vector3( -cubeWidth * .5f, cubeHeight * .5f + (cubeHeight * y), cubeLength * .5f),
		    new Vector3( cubeWidth * .5f, cubeHeight * .5f + (cubeHeight * y), cubeLength * .5f),
		    new Vector3( cubeWidth * .5f, cubeHeight * .5f + (cubeHeight * y), -cubeLength * .5f),
		    new Vector3( -cubeWidth * .5f, cubeHeight * .5f + (cubeHeight * y), -cubeLength * .5f)
	    };
    }

    int[] GenerateTriangles(int verticesLength)
    {
	    int count = verticesLength / 24;
	    List<int> triangles = new List<int>();

	    for (int i = 0; i < count; i++)
	    {
		    triangles.Add(3 + (24 * i));
		    triangles.Add(1 + (24 * i));
		    triangles.Add(0 + (24 * i));
		    triangles.Add(3 + (24 * i));
		    triangles.Add(2 + (24 * i));
		    triangles.Add(1 + (24 * i));

		    for (int j = 1; j <= 5; j++)
		    {
			    triangles.Add((3 + 4 * j) + (24 * i));
			    triangles.Add((1 + 4 * j) + (24 * i));
			    triangles.Add((0 + 4 * j) + (24 * i));
			    triangles.Add((3 + 4 * j) + (24 * i));
			    triangles.Add((2 + 4 * j) + (24 * i));
			    triangles.Add((1 + 4 * j) + (24 * i));
		    }
	    }

	    return triangles.ToArray();
    }
}
