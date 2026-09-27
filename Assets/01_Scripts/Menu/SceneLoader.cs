using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Recarga la escena actual desde cero (útil para el botón "Reiniciar").
    /// </summary>
    public void RestartGame()
    {
        // Importante: si el juego estaba pausado (Game Over), el tiempo queda en 0
        // y eso puede arrastrarse a la escena recargada. Lo reseteamos antes de cargar
        // para que todo (física, joints de agarre, etc.) arranque limpio de verdad.
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Cierra el juego. Enganchalo al botón "Salir".
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}