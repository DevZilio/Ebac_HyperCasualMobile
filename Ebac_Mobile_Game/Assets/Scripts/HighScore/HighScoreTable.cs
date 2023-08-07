using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highScoreEntryTransformList;
    public int maxNumHighScore = 5;

    // Chave para salvar os High Scores no PlayerPrefs
    private const string highScoreKey = "highscoreTable";


    private void Awake()
    {
        _entryContainer = transform.Find("HighScoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighScoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        // AddHighScoreEntry(10, "PHZ");
        // ResetHighScores();

        string jsonString = PlayerPrefs.GetString(highScoreKey);
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);



        if (highscores == null)
        {


            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighScoreEntry(2, "CMK");
            AddHighScoreEntry(1, "JOE");
            AddHighScoreEntry(1, "DAV");

            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<HighScores>(jsonString);
        }

        //Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        // Remover entradas excedentes, se houver
        if (highscores.highscoreEntryList.Count > maxNumHighScore)
        {
            highscores.highscoreEntryList.RemoveAt(maxNumHighScore);
        }

        _highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, _entryContainer, _highScoreEntryTransformList);
        }

    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();


    }

    public void UpdateHighScoreTable()
    {
        // Limpar a tabela atual
        foreach (Transform entryTransform in _highScoreEntryTransformList)
        {
            Destroy(entryTransform.gameObject);
        }
        _highScoreEntryTransformList.Clear();

        // Carregar novamente os dados dos high scores
        string jsonString = PlayerPrefs.GetString(highScoreKey);
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        // Ordenar a lista de high scores em ordem decrescente
        highscores.highscoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));

        // Remover entradas excedentes, se houver
        if (highscores.highscoreEntryList.Count > maxNumHighScore)
        {
            highscores.highscoreEntryList.RemoveAt(maxNumHighScore);
        }

        // Recrear a tabela com os dados atualizados
        _highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, _entryContainer, _highScoreEntryTransformList);
        }
    }


    public void AddHighScoreEntry(int score, string name)
    {
        // Carregar os high scores existentes do PlayerPrefs
        string jsonString = PlayerPrefs.GetString(highScoreKey);
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highscores == null)
        {
            // Não há tabela de high scores armazenada, inicialize com valores padrão
            highscores = new HighScores()
            {
                highscoreEntryList = new List<HighScoreEntry>()
            };
        }

        // Criar HighScoreEntry para o novo resultado
        HighScoreEntry newHighScoreEntry = new HighScoreEntry { score = score, name = name };

        // Adicionar o novo resultado à lista existente de high scores
        highscores.highscoreEntryList.Add(newHighScoreEntry);

        // Ordenar a lista de high scores em ordem decrescente
        highscores.highscoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));

        // Remover entradas excedentes, se houver
        if (highscores.highscoreEntryList.Count > maxNumHighScore)
        {
            highscores.highscoreEntryList.RemoveAt(maxNumHighScore);
        }

        // Converter o objeto HighScores para JSON
        string updatedJsonString = JsonUtility.ToJson(highscores);

        // Salvar os high scores atualizados no PlayerPrefs
        PlayerPrefs.SetString(highScoreKey, updatedJsonString);
        PlayerPrefs.Save();

        

    }


    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }


        entryTransform.Find("PositionText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highScoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }


    private class HighScores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }


    //Represents a single High Score entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
