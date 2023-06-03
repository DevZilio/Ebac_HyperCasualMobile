using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

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

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;

    private void Start()
    {
        StartScreen.SetActive(true);
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void StartToRun()
    {
        _canRun = true;
        Time.timeScale = 1f; // Unpause game
    }


#region POWERUPS

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

#endregion
}
