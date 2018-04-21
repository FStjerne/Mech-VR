using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeScreen6 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        Vector3 BottomLeft = vertices[0];
        Vector3 TopRight = vertices[1];
        Vector3 BottomRight = vertices[2];
        Vector3 TopLeft = vertices[3];
        //switch around where the start and end positions go, just try it out until all four vertices go to the right positions
        vertices[0] = new Vector3(BottomLeft.x, BottomLeft.y, BottomLeft.z);
        vertices[1] = new Vector3(TopRight.x, TopRight.y, TopRight.z);
        vertices[2] = new Vector3(BottomRight.x, BottomRight.y, BottomRight.z);
        vertices[3] = new Vector3(TopLeft.x + 0.0898f, TopLeft.y - 0.011f, TopLeft.z + 0.0231f);
        mesh.vertices = vertices;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
