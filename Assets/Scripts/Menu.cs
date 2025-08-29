using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(""); // Substitua pelo nome da sua cena de jogo
    }

    public void OpenSettings()
    {
        Debug.Log("Abrindo configura��es..."); // Aqui voc� pode abrir um painel de configura��es
        // Ex: settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
