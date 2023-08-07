using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{

    public Collider collider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;

    private AudioManager _audioManager;
    





    public void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();

        if (CoinsAnimationManager.Instance != null)
        {
            CoinsAnimationManager.Instance.RegisterCoin(this);
        }
        else
        {
            Debug.LogWarning("CoinsAnimationManager instance is null. Make sure it's properly initialized.");
        }
    }

    // Override of the OnCollect function from the ItemCollectableBase class
    protected override void OnCollect()
    {
        // Call the base implementation of OnCollect to handle common behavior
        base.OnCollect();

        // Block player catch twice the same item
        collider.enabled = false;
        collect = true;
        BounceHelper.Instance.BounceCollected();
        if(_audioManager != null) _audioManager.CoinCollectedSFX();

        // Call the AddCoins function in the ItemManager singleton
        ItemManager.Instance.AddCoins();

    

    }

    protected override void Collect()
    {
        OnCollect();
    }

    // Update function called every frame
    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
        }
    }
}
