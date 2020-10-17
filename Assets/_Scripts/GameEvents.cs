using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onTouchCube;
    public event Action onTouchCubeOther;
    public void CubeTouched()
    {
        if (onTouchCube != null)
        {
            onTouchCube();
        }
    }
    public void OtherCubeTouched()
    {
        if (onTouchCubeOther != null)
        {
            onTouchCubeOther();
        }
    }
}
