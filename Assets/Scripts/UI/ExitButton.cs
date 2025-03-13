using UnityEngine;

public class ExitButton : MonoBehaviour
{
    //[SerializeField] private bool _drawDebug = false;
    [SerializeField] private GameObject _ExitMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Shows menu if escape is pressed
        if (!_ExitMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _ExitMenu.SetActive(true);
            Debug.Log("space key was pressed");

        }
    }

    //Exits Game
    public void QuitGame()
    {
        Debug.Log("Game has been exited");
        Application.Quit();
    }

    public void ExitMenu()
    {
        _ExitMenu.SetActive(false);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Event.current.mousePosition, 5);
    }*/
}
