using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DevZilio.Core.Singleton;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    public float speedPlayer = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";


    //UIs
    public GameObject endScreen;
    public GameObject StartScreen;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject triggerCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    [SerializeField] private BounceHelper _bounceHelper;
    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private bool _invencible = false;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;
    private bool _isFlying = false;




    private void Start()
    {
        StartScreen.SetActive(true);
        _startPosition = transform.position;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position =
            Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    public void Bounce()
    
    {
        if(_bounceHelper != null)
        _bounceHelper.Bounce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            if (!_invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative(); // SetRelative get the current positionm
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
        // Time.timeScale = 0f; // Pause game
    }

    public void StartToRun()
    {
       
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation );
        Time.timeScale = 1f; // Unpause game
    }


    #region POWERUPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
        Debug.Log("Up Speed");
    }

    public void ResetSpeed()
    {
        _currentSpeed = speedPlayer;
        Debug.Log("Reset Speed");
    }

    public void PowerUpInvencible(bool b = true)
    {
        _invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        // Code go up 1 option
        /* var p = transform.position;
         p.y = _startPosition.y + amount;
         transform.position = p;
        */

        //Code animation go up with DGTween
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        _isFlying = true;
        animatorManager.Play(AnimatorManager.AnimationType.FLY);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        // Code go up 1 option
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/

        //Code animation go up with DGTween
        transform.DOMoveY(_startPosition.y, 1f);

        _isFlying = false;

        if(!_isFlying)
        {
            animatorManager.Play(AnimatorManager.AnimationType.RUN);
        }

    }

    public void ChangeColliderCollectorSize(float amount)
    {
        triggerCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion
}
