using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableEnergy : ItemCollectableBase
{

    public Animator animator;

    protected override void OnCollect()
    {
        base.OnCollect();
        animator.SetTrigger("Collected");
        Destroy(gameObject, 1);

        ItemManager.Instance.AddCoins();
    }

    private void Update()
    {
        // Verifica se a animação terminou e reativa o Collider2D
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