using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public Material mat;

    float width = 100;
    float height = 100;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];

        vertices[0] = new Vector3(-width, -height);
        vertices[1] = new Vector3(-width, height);
        vertices[2] = new Vector3(70, 70f);
        vertices[3] = new Vector3(width, -height);

        GetComponent<MeshRenderer>().material = mat;

        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
