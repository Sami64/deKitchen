using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    enum Mode
    {
        LookAt,
        CameraFoward
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.CameraFoward:
                transform.forward = Camera.main.transform.forward;
                break;
        }
    }
}
