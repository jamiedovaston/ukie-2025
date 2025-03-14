using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButtons : MonoBehaviour
{
    //private bool _showInstructions = false;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject instructions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ToggleInstructions()
    {
        if (menu.active)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }

        if (instructions.active)
        {
            instructions.SetActive(false);
        }
        else
        {
            instructions.SetActive(true);
        }


    }
}
