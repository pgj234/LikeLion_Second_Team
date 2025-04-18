using System;
using UnityEngine;

public class EnemyEvents
{
    internal event Action enemyGetDamageAction;

    public void EnemyGetDamageAction()
    {
        enemyGetDamageAction?.Invoke();
    }
}
