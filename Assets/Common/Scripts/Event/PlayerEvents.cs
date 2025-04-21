using System;
using UnityEngine;

public class PlayerEvents
{
    internal event Action playerGetDamageAction;

    public void PlayerGetDamageAction()
    {
        playerGetDamageAction?.Invoke();
    }
}
