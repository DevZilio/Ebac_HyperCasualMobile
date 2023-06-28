using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenuHelper : MonoBehaviour
{
 

    public void LoadMenu(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}