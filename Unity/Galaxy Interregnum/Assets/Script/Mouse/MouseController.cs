using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Singleton<MouseController>
{
    public Action<RaycastHit> OnLeftMouseCLick;
    public Action<RaycastHit> OnRightMouseCLick;
    public Action<RaycastHit> OnMiddleMouseCLick;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            CheckMouseClick(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            CheckMouseClick(2);
        }
    }

    void CheckMouseClick(int mouseButton)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (mouseButton == 0) { OnLeftMouseCLick?.Invoke(hit); }
            else if (mouseButton == 1) { OnRightMouseCLick?.Invoke(hit); }
            else if (mouseButton == 2) { OnMiddleMouseCLick?.Invoke(hit); }
        }
    }
}
