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
    public event Action onEnemyAttack;
    public event Action onEnemyHit;
    public event Action onColdLoot;
    public event Action onWarmLoot;
    public event Action onInvUpdate;

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

    public void EnemyAttack()
    {
        if (onEnemyAttack != null)
        {
            onEnemyAttack();
        }
    }

    public void EnemyHit()
    {
        if (onEnemyHit != null)
        {
            onEnemyHit();
        }
    }

    public void ColdLoot()
    {
        if (onColdLoot != null)
        {
            onColdLoot();
        }
    }

    public void WarmLoot()
    {
        if (onWarmLoot != null)
        {
            onWarmLoot();
        }
    }

    public void InvUpdate()
    {
        if(onInvUpdate != null)
        {
            onInvUpdate();
        }
    }
}
