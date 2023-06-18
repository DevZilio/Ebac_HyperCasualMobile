using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Publics
    public Transform container;
    public List<GameObject> levels;

    //Priavetes
    [SerializeField] private int _index;
    private GameObject _currentLevel;


    private void Awake()
    {
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            //Verifica quantidade de levels dentro da lista e quando chega ao final, reseta
            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }

        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }


    private void Update()
    {
        if(Input. GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
