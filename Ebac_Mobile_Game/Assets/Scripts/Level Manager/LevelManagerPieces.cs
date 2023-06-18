using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerPieces : MonoBehaviour
{
    //Publics
    public Transform container;

    public List<LevelPieceBasedSetup> levelPieceBasedSetups;



    //Privates
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;
    private int _index = 1;


    private void Awake()
    {

        CreateLevelPieces();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }


    #region

    private void CreateLevelPieces()
    {

        CleanSpawnedPieces();

        if (_currSetup != null)
        {
            _index++;
            if (_index >= levelPieceBasedSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _currSetup = levelPieceBasedSetups[_index];

        for (int i = 0; i < _currSetup.pieceStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieceStart);
        }
        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
        }
        for (int i = 0; i < _currSetup.pieceEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieceEnd);
        }
    }


    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        _spawnedPieces.Add(spawnedPiece);


    }

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }
    #endregion

    private void ResetLevelIndex()
    {
        _index = 0;
    }


}

