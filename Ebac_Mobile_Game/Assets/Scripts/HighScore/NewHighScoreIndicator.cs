using UnityEngine;

public class NewHighScoreIndicator : MonoBehaviour
{
    public GameObject newHighScoreButton;

    public void ToggleIndicator(bool show)
    {
        newHighScoreButton.SetActive(show);
    }
}
