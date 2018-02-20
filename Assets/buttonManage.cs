using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManage : MonoBehaviour
{

    // runs a new game when the "start" button is pressed
    public void startGameButton(string newGame)
    {
        SceneManager.LoadScene(newGame);
    }

}
