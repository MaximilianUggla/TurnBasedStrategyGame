using UnityEngine;

public static class HexRingMetrics
{
    public static float SideRadius (float hexSize)
    {
        return hexSize * 0.866025404f;
    }

    public static Vector3[] Corners (float hexSize)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, i);
        }
        return corners;
    }

    public static Vector3 Corner (float hexSize, int index)
    {
        float angle = 60f * index;

        Vector3 corner = new Vector3(hexSize * Mathf.Cos(angle * Mathf.Deg2Rad), 
        0f,
        hexSize * Mathf.Sin(angle * Mathf.Deg2Rad)
        );

        return corner;
    }

    public static Vector3 Center(float cornerRadius, int col, int r)
    {
        float factor;

        if (col % 2 != 0)
        {
            if (r == 1 || r == -1)
            {
                if (r < 0) {factor = -1;}
                else {factor = 1;}
            }
            else {
                if (r < 0) {factor = (2 * r + 1);}
                else {factor = 2 * r - 1;}
            }
        }
        else {factor = 2 * r;}

        Vector3 centerPosition;
        centerPosition.x = col * cornerRadius * 1.5f;
        centerPosition.y = 0f;
        centerPosition.z = factor * (SideRadius(cornerRadius));

        return centerPosition;
    }

    public static int nbrOfHexagons(int Layers)
    {
        if (Layers == 0) { return 1; }
        else { return (Layers-1) * 6 + nbrOfHexagons(Layers-1); }
    }
}