using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void MudarCena(string cena)
    {
        SceneManager.LoadScene(cena); // Substitua pelo nome da sua cena de jogo
    }


    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
