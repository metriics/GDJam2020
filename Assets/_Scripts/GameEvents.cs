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
    public event Action onHotLoot;
    public event Action onInvUpdate;
    public event Action onDig;
    public event Action onDugUp;
    public event Action onEnemySpawn;

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

    public void HotLoot()
    {
        if (onHotLoot != null)
        {
            onHotLoot();
        }
    }

    public void InvUpdate()
    {
        if(onInvUpdate != null)
        {
            onInvUpdate();
        }
    }

    public void Dig()
    {
        if (onDig != null)
        {
            onDig();
        }
    }

    public void DugUp()
    {
        if(onDugUp != null)
        {
            onDugUp();
        }
    }

    public void EnemySpawn()
    {
        if(onEnemySpawn != null)
        {
            onEnemySpawn();
        }
    }
}
