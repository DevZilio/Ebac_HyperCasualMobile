using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    // [Header("Animation")]
    // Public variable to store the animator component
    // public Animator animator;
    // public Collider2D collider;
    //[Header("Sounds")]
    // public AudioSource audioSource;
    public bool collect = false;
    public Collider collider;
    public float minDistance = 1f;

    public float lerp = 5f;

    // Override of the OnCollect function from the ItemCollectableBase class
    protected override void OnCollect()
    {
        // Call the base implementation of OnCollect to handle common behavior
        base.OnCollect();

        // Block player catch twice the same item
        collider.enabled = false;
        collect = true;

        // Trigger the "Collected" animation state in the animator component
        //animator.SetTrigger("Collected");
        // if (audioSource != null) audioSource.Play();
        // Call the AddCoins function in the ItemManager singleton
        // ItemManager.Instance.AddCoins();

        // Destroy the game object after 1 second
        Destroy(gameObject, 1);
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
            transform.position = Vector3.Lerp(transform.position,PlayerController.Instance.transform.position,lerp * Time.deltaTime);

            if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
        }
    }
}
