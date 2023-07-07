using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using DG.Tweening;
using UnityEngine;

public class BounceHelper : Singleton<BounceHelper>
{

    [Header("Animation")]
    public float scaleBounce = 1.2f;
    public float scaleDuration = .2f;
    public float scaleStartDuration = .8f;
    public Ease ease = Ease.OutBack;

    private Tweener scaleTweener = null;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BounceCollected();

        }
    }

//Faz com que o player ao iniciar saia de escala zero para 1
    public void BounceStart()
    {
        transform.DOScale(Vector3.one, scaleStartDuration);
    }

    public void BounceCollected()
    {
        if(scaleTweener != null)
        {
            scaleTweener.Kill();
        }
        transform.localScale = Vector3.one;
        scaleTweener = transform.DOScale(scaleBounce, scaleDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }

  
}
