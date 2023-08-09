using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckNewHighScore : MonoBehaviour
{
    // public NewHighScoreIndicator newHighScoreButton; //GameObject to indicate New High Score

    public GameObject inputName;

    public GameObject textTryAgain;



    private HighScoreTable highScoreTable;

    private void Start()
    {
        // newHighScoreButton = FindObjectOfType<NewHighScoreIndicator>(); // Encontre o objeto com o novo script
        highScoreTable = FindObjectOfType<HighScoreTable>();

        if (highScoreTable == null)
        {
            Debug.LogError("HighScoreTable component not found!");
            return;
        }

        CheckNewHighscore();



    }


    // Start is called before the first frame update
    public void CheckNewHighscore()
    {
        if (highScoreTable == null)
        {
            Debug.LogError("HighScoreTable 222 component not found!");
            return;
        }

        int totalCoinsCollected = ItemManager.Instance.GetTotalCoins();
        // Obter as entradas de High Score
        var highScoreEntries = highScoreTable.GetHighScoreEntries();

        // Verificar se o totalCoinsCollected está entre os 10 melhores
        bool isNewHighScore = false;

        if (highScoreEntries.Count < 10 || totalCoinsCollected > highScoreEntries[highScoreEntries.Count - 1].score)
        {
            isNewHighScore = true;
        }

        if (isNewHighScore)
        {
            inputName.SetActive(true);
        }

        else if (isNewHighScore == false)
        {
            textTryAgain.SetActive(true);
        }

    }
}