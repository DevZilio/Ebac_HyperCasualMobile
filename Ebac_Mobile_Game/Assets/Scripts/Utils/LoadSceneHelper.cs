using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneHelper : MonoBehaviour
{
    public Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(RandomizeLevelPieces);
        Debug.Log("Awake");
    }

    public void RandomizeLevelPieces()
    {
        LevelManagerPieces levelManagerPieces = FindObjectOfType<LevelManagerPieces>();
        if (levelManagerPieces != null)
        {
            levelManagerPieces.CreateLevelPieces();
        }
        Debug.Log("RandomizeLevelPieces");
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
