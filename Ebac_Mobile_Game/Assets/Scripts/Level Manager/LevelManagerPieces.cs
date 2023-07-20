using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManagerPieces : MonoBehaviour
{
    // Publics
    public Transform container;
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    // Privates
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;
    private int _index = 1;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float timeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        // CreateLevelPieces();
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

    // Criar a peça do início do cenário (peça fixa)
    if (_currSetup.pieceStartNumber > 0 && _currSetup.levelPieceStart.Count > 0)
    {
        CreateLevelPiece(_currSetup.levelPieceStart[0]);
    }

    // Criar as peças intermediárias randomicamente
    List<LevelPieceBase> availablePieces = new List<LevelPieceBase>(_currSetup.levelPieces);
    int consecutiveCount = 0;
    int piecesCount = 0;

    while (piecesCount < _currSetup.piecesNumber)
    {
        // Verificar se ainda há peças disponíveis para criar
        if (availablePieces.Count == 0)
        {
            availablePieces = new List<LevelPieceBase>(_currSetup.levelPieces); // Resetar a lista de peças disponíveis
            continue; // Continuar o loop para tentar novamente criar as peças restantes
        }

        // Obtemos a próxima peça aleatória
        LevelPieceBase nextPiece = availablePieces[Random.Range(0, availablePieces.Count)];

        // Removemos a peça da lista de peças disponíveis para evitar repetições em sequência
        availablePieces.Remove(nextPiece);

        // Se a próxima peça for igual à última criada, adicionamos 1 ao contador de peças consecutivas
        if (_spawnedPieces.Count > 0 && nextPiece == _spawnedPieces[_spawnedPieces.Count - 1])
        {
            consecutiveCount++;
        }
        else
        {
            // Se a próxima peça for diferente, resetamos o contador
            consecutiveCount = 0;
        }

        // Se tivermos 3 peças consecutivas iguais, adicionamos novamente a peça removida para evitar quebras na sequência
        if (consecutiveCount >= 3 && availablePieces.Count > 0)
        {
            availablePieces.Add(nextPiece);
            nextPiece = availablePieces[Random.Range(0, availablePieces.Count)];
            availablePieces.Remove(nextPiece);
            consecutiveCount = 0; // Resetamos o contador após escolher uma peça diferente
        }

        CreateLevelPiece(nextPiece);
        piecesCount++; // Incrementar a contagem de peças criadas
    }

    // Criar a peça do fim do cenário (peça fixa)
    if (_currSetup.pieceEndNumber > 0 && _currSetup.levelPieceEnd.Count > 0)
    {
        CreateLevelPiece(_currSetup.levelPieceEnd[0]);
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

    private void CreateLevelPiece(LevelPieceBase piece)
    {
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

    public void ResetLevelIndex()
    {
        _index = 0;
        Debug.Log("resetLevel");
    }
}
