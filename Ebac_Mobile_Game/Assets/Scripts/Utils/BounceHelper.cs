using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using DG.Tweening;
using UnityEngine;

public class BounceHelper : Singleton<BounceHelper>
{

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BouncePowerUp();

        }
    }

    public void Bounce()
    {
        transform.DOScale(Vector3.one, 1f);
    }

    public void BouncePowerUp()
    {
        transform.DOScale(scaleBounce, scaleDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }

  
}
