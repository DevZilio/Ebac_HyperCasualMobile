using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Power Up")]
    public float duration;

    protected override void OnCollect()
    {
        base.OnCollect();
        if (BounceHelper.Instance != null)
        {
            BounceHelper.Instance.BounceCollected();
        }

        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Debug.Log("Start power Up");
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("End power Up");
    }
}
