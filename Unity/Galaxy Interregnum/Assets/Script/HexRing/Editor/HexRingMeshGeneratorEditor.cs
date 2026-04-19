using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexRingMeshGenerator))]
public class HexRingMeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HexRingMeshGenerator hexRingMeshGenerator = (HexRingMeshGenerator)target;

        if (GUILayout.Button("Generate Hex Mesh"))
        {
            hexRingMeshGenerator.CreateHexMesh();
        }

        if (GUILayout.Button("Clear Hex Mesh"))
        {
            hexRingMeshGenerator.ClearHexRingMesh();
        }
    }
}