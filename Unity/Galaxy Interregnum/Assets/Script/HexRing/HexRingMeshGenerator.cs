using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexRingMeshGenerator : MonoBehaviour
{
    [field:SerializeField] public LayerMask GridLayer { get; private set; }
    [field:SerializeField] public HexRing HexRing { get; private set; }

    private void Awake()
    {
        if (HexRing == null) {HexRing = GetComponentInParent<HexRing>();}
        if (HexRing == null) {Debug.LogError("HexRingMeshGenerator could not find a HexRing component in its parent or itself.");}
    }

    public void CreateHexMesh()
    {
        CreateHexMesh(HexRing.Layers, HexRing.Padding, HexRing.HexSize, GridLayer);
    }

    public void CreateHexMesh(HexRing hexRing, LayerMask layerMask)
    {
        HexRing = hexRing;
        GridLayer = layerMask;
        CreateHexMesh(HexRing.Layers, HexRing.Padding, HexRing.HexSize, GridLayer);
    }

    public void CreateHexMesh(int Layers, int Padding, int hexSize, LayerMask layerMask)
    {
        ClearHexRingMesh();
        int nbrOfHexagons = HexRingMetrics.nbrOfHexagons(Layers);
        Vector3[] vertices = new Vector3[nbrOfHexagons * 7];
         
        int counter = 0;
        int height = 2 * Layers - 1;
        for (int x = -Layers+1; x < Layers; x++)
        {
            int intervall = (height - Mathf.Abs(x)) / 2;
            for (int y = -intervall; y <= intervall; y++)
            {
                if (!(y == 0 && x%2 != 0))
                {
                    int hexagon = counter * 7;
                    Vector3 centerPosition = HexRingMetrics.Center(hexSize + Padding, x, y);
                    vertices[hexagon] = centerPosition;
                    for (int s = 1; s <= 6; s++)
                    {
                        vertices[hexagon + s] = centerPosition + HexRingMetrics.Corners(hexSize)[s % 6];
                    }
                    
                    counter++;
                }
            }
        }

        int[] triangles = new int[3 * 6 * nbrOfHexagons];

        counter = 0;
        for (int i = 0; i < nbrOfHexagons; i++)
        {
            int hexagon = counter * 7;
            for (int s = 0; s < 6; s++)
            {
                int cornerIndex = s + 2 > 6 ? s + 2 - 6 : s + 2;
                triangles[3 * 6 * counter + s * 3] = hexagon;
                triangles[3 * 6 * counter + s * 3 + 2] = hexagon + s + 1;
                triangles[3 * 6 * counter + s * 3 + 1] = hexagon + cornerIndex;
            }
            
            counter++;
        }

        Mesh mesh = new Mesh();
        mesh.name = "Hex Mesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int gridLayerIndex = GetLayerIndex(layerMask);
        Debug.Log("Layer Index: " + gridLayerIndex);

        gameObject.layer = gridLayerIndex;
    }

    public void ClearHexRingMesh()
    {
        if (GetComponent<MeshFilter>().sharedMesh == null)
        {
            return;
        }
        GetComponent<MeshFilter>().sharedMesh.Clear();
        GetComponent<MeshCollider>().sharedMesh.Clear();
    }

    private int GetLayerIndex(LayerMask layerMask)
    {
        int layerMaskValue = layerMask.value;
        Debug.Log("Layer Mask Value: " + layerMaskValue);
        for (int i = 0; i < 32; i++)
        {
            if (((1 << i) & layerMaskValue) != 0)
            {
                Debug.Log("Layer Index Loop: " + i);
                return i;
            }
        }
        return 0;
    }
}