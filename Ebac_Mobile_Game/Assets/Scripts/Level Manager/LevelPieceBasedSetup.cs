using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBasedSetup : ScriptableObject
{
    [Header("Pieces")]
    public List<LevelPieceBase> levelPieceStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPieceEnd;

    public int pieceStartNumber = 1;
    public int piecesNumber = 5;
    public int pieceEndNumber = 1;

}
