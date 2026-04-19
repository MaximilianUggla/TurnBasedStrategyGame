using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexRing))]
public class HexRingEditor : Editor
{
    private void showCords(HexRing HR)
    {
        int height = 2 * HR.Layers - 1;
        for (int x = -HR.Layers+1; x < HR.Layers; x++)
        {
            int intervall = (height - Mathf.Abs(x)) / 2;
            for (int y = -intervall; y <= intervall; y++)
            {
                if (!(y == 0 && x%2 != 0))
                {
                    Vector3 labelPosition = HexRingMetrics.Center(HR.HexSize + HR.Padding, x, y) + HR.transform.position - new Vector3(HR.HexSize/2, 0, 0);

                    Handles.Label(labelPosition, $"[{x}, {y}]");
                    //Handles.Label(centerPosition, $"({centerPosition.x}, {centerPosition.y}, {centerPosition.z})");
                }
            }
        }

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HexRing hexRing = (HexRing)target;

        if (GUILayout.Button(hexRing.Coordinates ? "Hide Coordinates" : "Show Coordinates"))
        {
            hexRing.Coordinates = !hexRing.Coordinates;
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI()
    {
        HexRing hexRing = (HexRing)target;

        if (hexRing.Coordinates)
        {
            showCords(hexRing); 
        }
    }
}