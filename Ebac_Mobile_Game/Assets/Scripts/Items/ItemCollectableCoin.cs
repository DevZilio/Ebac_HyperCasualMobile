using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Animation")]
    // Public variable to store the animator component
    public Animator animator;
    public Collider2D collider;

    [Header("Sounds")]
    public AudioSource audioSource;



    // Override of the OnCollect function from the ItemCollectableBase class
    protected override void OnCollect()
    {
        // Call the base implementation of OnCollect to handle common behavior
        base.OnCollect();
        
        // Block player catch twice the same item
        collider.enabled = false;

        // Trigger the "Collected" animation state in the animator component
        animator.SetTrigger("Collected");
        if (audioSource != null) audioSource.Play();

        // Call the AddCoins function in the ItemManager singleton
        ItemManager.Instance.AddCoins();


        // Destroy the game object after 1 second
        Destroy(gameObject, 1);
    }

    // Update function called every frame
    private void Update()
    {
        // Check if the "Collected" animation state has finished and re-enable the Collider2D component
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}
