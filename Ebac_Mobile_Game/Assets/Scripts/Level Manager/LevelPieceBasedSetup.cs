using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBasedSetup : ScriptableObject
{
    [Header("Arts")]
    public ArtManager.ArtType artType;
    
    [Header("Pieces")]
    public List<LevelPieceBase> levelPieceStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPieceEnd;

    public int pieceStartNumber = 1;
    public int pieceEndNumber = 1;

    [Header("Random Range for piecesNumber")]
    public int minPiecesNumber = 2;
    public int maxPiecesNumber = 6;

    //Propriedade para piecesNumber com um getter personalizado que retorna um número aleatório toda vez que é acessada
    public int PiecesNumber
    {
        get
        {
            return Random.Range(minPiecesNumber, maxPiecesNumber + 1);
            Debug.Log("Número aleatório para piecesNumber: " + PiecesNumber);
        }
    }

}
