using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : PowerUpBase
{
    [Header("Collector")]
    public float sizeAmount = 7;


    protected override void StartPowerUp()
    {
        
        base.StartPowerUp();
        PlayerController.Instance.ChangeColliderCollectorSize(sizeAmount);
        PlayerController.Instance.SetPowerUpText("Magnet");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ChangeColliderCollectorSize(1);
        PlayerController.Instance.SetPowerUpText("");
    }
}
