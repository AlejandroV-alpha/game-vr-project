using System.Collections.Generic; // NUEVO: para la lista de paneles ocultados
using UnityEngine;

public class GameManagerMenu : MonoBehaviour
{
    // [Header("Menú")]
    // [Tooltip("El Canvas o GameObject raíz del menú completo (contiene los dos paneles de abajo).")]
    // [SerializeField] private GameObject menuUI;

    [Header("Ranking")]
    [Tooltip("Panel del ranking. Tiene que ser un objeto de la escena, no un prefab del Project.")]
    [SerializeField] private GameObject panelRanking;

    // NUEVO
    [Tooltip("Paneles que se ocultan mientras el ranking está abierto (Inicio, Game Over, etc.). " +
             "Al cerrar el ranking se vuelven a mostrar los que estaban activos. Puede quedar vacío.")]
    [SerializeField] private GameObject[] panelesAOcultar;

    // [Header("Paneles del menú")]
    // [Tooltip("Panel con el botón 'Inicio' (se muestra la primera vez, al arrancar).")]
    // [SerializeField] private GameObject panelInicio;
    // [Tooltip("Panel con los botones 'Reiniciar' y 'Salir' (se muestra al morir).")]
    // [SerializeField] private GameObject panelGameOver;

    // [Header("Componentes a bloquear mientras el menú está activo")]
    // [Tooltip("Arrastrá acá los providers de movimiento del XR Origin: " +
    //          "Move, Turn, Teleportation, Climb, Gravity, Jump, etc. " +
    //          "También podés meter interactores (Near-Far, Poke) o scripts como el " +
    //          "LevelGenerator si mueven el escenario hacia el jugador.")]
    // [SerializeField] private Behaviour[] componentesABloquear;
    // private GameObject panelAnterior;

    // NUEVO
    private RankingUI rankingUI;
    private readonly List<GameObject> panelesOcultados = new List<GameObject>();

    private void Awake()
    {
        // Al arrancar la escena: se ve el panel de Inicio, movimiento bloqueado.
        // MostrarPanel(panelInicio);
        // SetBloqueo(true);

        // NUEVO: el ranking arranca cerrado.
        if (panelRanking != null)
            panelRanking.SetActive(false);
    }

    // /// <summary>
    // /// Enganchá este método en el OnClick() del botón "Inicio".
    // /// </summary>
    // public void StartGame()
    // {
    //     if (menuUI != null)
    //         menuUI.SetActive(false);
    //
    //     SetBloqueo(false);
    // }

    // /// <summary>
    // /// Llamalo desde PlayerHealth cuando el jugador muere:
    // /// muestra el panel de Game Over (Reiniciar / Salir) y bloquea el movimiento.
    // /// </summary>
    // public void ShowGameOverMenu()
    // {
    //     MostrarPanel(panelGameOver);
    //     SetBloqueo(true);
    // }

    // private void MostrarPanel(GameObject panelAMostrar)
    // {
    //     if (menuUI != null)
    //         menuUI.SetActive(true);
    //
    //     if (panelInicio != null)
    //         panelInicio.SetActive(panelInicio == panelAMostrar);
    //
    //     if (panelGameOver != null)
    //         panelGameOver.SetActive(panelGameOver == panelAMostrar);
    //
    //     if (panelRanking != null)
    //         panelRanking.SetActive(panelRanking == panelAMostrar);
    // }

    // private void SetBloqueo(bool bloqueado)
    // {
    //     // Congela/descongela TODO lo que dependa del tiempo: movimiento del player,
    //     // enemigos, animaciones, física, y cualquier coroutine con WaitForSeconds
    //     // (como el LevelGenerator moviendo tiles). Así no hace falta listar cada
    //     // script ni cada prefab de escenario.
    //     Time.timeScale = bloqueado ? 0f : 1f;
    //
    //     // Además, apaga puntualmente los que quieras arrastrar acá (por ejemplo
    //     // los interactores de la mano) para que ni siquiera se pueda intentar
    //     // agarrar o disparar mientras el menú está arriba.
    //     foreach (var comp in componentesABloquear)
    //     {
    //         if (comp != null)
    //             comp.enabled = !bloqueado;
    //     }
    // }

    /// <summary>
    /// Enganchá esto en el botón "Ver Ranking" (desde el panel de Inicio o GameOver).
    /// </summary>
    public void ShowRanking()
    {
        // ---- CÓDIGO ANTERIOR ----
        // Guarda cuál estaba activo antes de mostrar el ranking
        // if (panelInicio != null && panelInicio.activeSelf) panelAnterior = panelInicio;
        // else if (panelGameOver != null && panelGameOver.activeSelf) panelAnterior = panelGameOver;
        //
        // MostrarPanel(panelRanking);
        // SetBloqueo(true);
        //
        // var rankingUI = panelRanking.GetComponentInChildren<RankingUI>(true);
        // if (rankingUI != null)
        //     rankingUI.Refresh();

        // ---- NUEVO ----
        if (panelRanking == null)
            return;

        // Solo recordamos qué paneles ocultar si el ranking todavía no estaba abierto.
        if (!panelRanking.activeSelf)
        {
            panelesOcultados.Clear();

            foreach (var panel in panelesAOcultar)
            {
                if (panel != null && panel != panelRanking && panel.activeSelf)
                {
                    panelesOcultados.Add(panel);
                    panel.SetActive(false);
                }
            }

            panelRanking.SetActive(true);
        }

        if (rankingUI == null)
            rankingUI = panelRanking.GetComponentInChildren<RankingUI>(true);

        if (rankingUI != null)
            rankingUI.Refresh();
    }

    public void CloseRanking()
    {
        // ---- CÓDIGO ANTERIOR ----
        // MostrarPanel(panelAnterior != null ? panelAnterior : panelInicio);

        // ---- NUEVO ----
        if (panelRanking != null)
            panelRanking.SetActive(false);

        foreach (var panel in panelesOcultados)
        {
            if (panel != null)
                panel.SetActive(true);
        }

        panelesOcultados.Clear();
    }
}