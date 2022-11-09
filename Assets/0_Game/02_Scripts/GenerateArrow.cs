using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateArrow : MonoBehaviour
{
    public Vector2 ArrowOrigin;
    public Vector2 ArrowEnd;
    public float ArrowWidth;
    public Material ArrowMaterial;
    
    private List<Vector3> verticesList;
    private List<int> trianglesList;

    Mesh generatedMesh;

    public void CustomArrow()
    {
        GenArrow(ArrowOrigin, ArrowEnd, ArrowWidth,ArrowMaterial);
    }

    public void GenArrow(Vector2 origin, Vector2 end, float width, Material material)
    {
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();
        
        Vector2 direction = (end - origin).normalized;
        Vector2 perpendicular = Vector2.Perpendicular(direction) * (width / 2);
        
        verticesList.Add((origin + perpendicular));
        verticesList.Add(origin - perpendicular);
        verticesList.Add(end + perpendicular);
        verticesList.Add(end - perpendicular);

        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(3);

        trianglesList.Add(0);
        trianglesList.Add(3);
        trianglesList.Add(2);

        generatedMesh = new Mesh();
        
        generatedMesh.vertices = verticesList.ToArray();
        generatedMesh.triangles = trianglesList.ToArray();

        this.GetComponent<MeshRenderer>().material = material;
        this.GetComponent<MeshFilter>().mesh = generatedMesh;
    }
}
