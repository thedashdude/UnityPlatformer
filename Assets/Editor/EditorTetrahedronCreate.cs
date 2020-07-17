using UnityEngine;
using UnityEditor;
 
public class TetrahedronEditor : Editor
{
    [MenuItem("GameObject/Create Other/Tetrahedron")]
    static void Create()
    {
        GameObject gameObject = new GameObject("Tetrahedron");
        Tetrahedron s = gameObject.AddComponent<Tetrahedron>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        s.Rebuild();
    }
}