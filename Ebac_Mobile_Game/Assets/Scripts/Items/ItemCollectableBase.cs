using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    // public ParticleSystem coinCollected;
    public float timeToHide = 3;
    public GameObject graphicItem;

   


    //Identify the collision using a Taga name and call the function Collect()
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    // Hide the main renderer sprit, starts particle system and wait to end animation
    protected virtual void Collect()
    {
        // if (graphicItem != null) graphicItem.SetActive(false);
        // Invoke("HideObject", timeToHide);
        OnCollect();
    }

    // Make false the main object, hiding while particle system works
    public void HideObject()
    {
        gameObject.SetActive(false);
    }

    // Starts Particle System when item is collected
    protected virtual void OnCollect()
    {
        // if (coinCollected != null) coinCollected.Play();
        // if(audioSource != null) audioSource.Play();
    }
}
