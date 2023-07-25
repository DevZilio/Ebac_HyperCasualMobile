using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    public ParticleSystem _confettis;
    public static ConfettiManager instance;

    public void Start()
    {
        _confettis = GetComponent<ParticleSystem>();
       
    }

    public void Awake() {
        instance = this;
    }

    public void PlayConfetti()
    {
        
            _confettis.Play();
            
        
    }
}
