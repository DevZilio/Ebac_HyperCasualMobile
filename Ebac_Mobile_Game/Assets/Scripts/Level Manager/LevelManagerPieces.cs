using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float timeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;


    private void Awake()
    {

        //CreateLevelPieces();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }


    #region

    public void CreateLevelPieces()
    {

        CleanSpawnedPieces();

        if (CoinsAnimationManager.Instance == null)
        {
            CoinsAnimationManager.Instance = new CoinsAnimationManager();

        }
        else
        {
            CoinsAnimationManager.Instance.ResetCoins();
        }


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

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);

        StartCoroutine(ScalePiecesByTime());
        CoinsAnimationManager.Instance.StartAnimationsCoins();

        Debug.Log("createLevelPieces");
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for (int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(timeBetweenPieces);
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

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
        Debug.Log("CreateLevelPiece");


    }

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
        Debug.Log("SpawPieces");
    }
    #endregion

    private void ResetLevelIndex()
    {
        _index = 0;
        Debug.Log("resetLevel");
    }


}

