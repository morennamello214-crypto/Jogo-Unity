using UnityEngine;
using UnityEngine.SceneManagement; // Importe para carregar cenas, se for o próximo passo

public class GoalFlag : MonoBehaviour
{
    // Variável opcional para o nome da próxima cena a ser carregada
    // Se quiser carregar a próxima fase, defina o nome no Inspector.
    // public string nextSceneName = "Level2";

    /// <summary>
    /// Chamado quando um Collider 2D entra no trigger da bandeira.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se o objeto que tocou a bandeira é o jogador
        //    (Assumindo que o objeto principal do jogador tem a Tag "Player")
        if (other.CompareTag("Player"))
        {
            // Tenta obter o script de controle do personagem
            MainCharacterController playerController = other.GetComponent<MainCharacterController>();

if (playerController != null)
{
    playerController.AchieveVictory();
}
        }
    }

    /*
    // Método de exemplo para carregar a próxima cena (Descomente se precisar)
    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    */
}