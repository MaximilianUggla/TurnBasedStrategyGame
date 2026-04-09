using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexRings : MonoBehaviour
{
    [field:SerializeField] public int Layers { get; private set; }
    [field:SerializeField] public int Padding { get; private set; }
    [field:SerializeField] public float HexSize { get; private set; }
    [field:SerializeField] public GameObject HexPreFab { get; private set; }
    void Start()
    {
        Debug.Log("Hello world");
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        int height = 2 * Layers - 1;
        for (int x = -Layers+1; x < Layers; x++)
        {
            int intervall = (height - Mathf.Abs(x)) / 2;
            for (int y = -intervall; y <= intervall; y++)
            {
                if (!(y == 0 && x%2 != 0))
                {
                    Vector3 centerPosition = HexRingMetrics.Center(HexSize, x, y, Padding) + transform.position;
                        for (int s = 0; s < 6; s++)
                        {
                            Gizmos.DrawLine(
                                centerPosition + HexRingMetrics.Corners(HexSize)[s % 6],
                                centerPosition + HexRingMetrics.Corners(HexSize)[(s + 1) % 6]
                            );
                        }
                }
            }
        }
    }
}