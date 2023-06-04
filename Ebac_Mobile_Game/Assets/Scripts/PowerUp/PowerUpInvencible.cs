using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{
      

protected override void StartPowerUp()
{

    base.StartPowerUp();
    PlayerController.Instance.PowerUpInvencible();
    PlayerController.Instance.SetPowerUpText("Invencible");
 
}

protected override void EndPowerUp()
{
    base.EndPowerUp();
    PlayerController.Instance.PowerUpInvencible(false);
    PlayerController.Instance.SetPowerUpText("");
    
}
}
