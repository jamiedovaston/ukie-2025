using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene("HomeMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }
}
