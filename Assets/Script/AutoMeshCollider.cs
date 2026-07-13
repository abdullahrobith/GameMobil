using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class AutoMeshCollider : MonoBehaviour
{
    private void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = meshFilter.sharedMesh;

        meshCollider.convex = false;
        meshCollider.enabled = true;
    }
}