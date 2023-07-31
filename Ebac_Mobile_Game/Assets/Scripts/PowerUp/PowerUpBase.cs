using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Power Up")]
    public float duration;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        if (BounceHelper.Instance != null)
        {
            BounceHelper.Instance.BounceCollected();
        }

        if(_audioManager != null) _audioManager.PowerUpCollectedSFX();

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
