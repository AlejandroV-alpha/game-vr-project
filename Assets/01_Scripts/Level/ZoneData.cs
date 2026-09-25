using System;
using UnityEngine;

[Serializable]
public class ZoneData
{
    [Header("Informacion")]
    [Tooltip("Nombre de la zona")]
    public string Name;

    [Header("Inicio")]
    [Tooltip("Tile fijo que se genera al comenzar esta zona")]
    public GameObject StartPrefab;

    [Header("Tiles normales")]
    [Tooltip("Tiles que se generan aleatoriamente durante la zona")]
    public GameObject[] Prefabs;

    [Header("Transicion")]
    [Tooltip("Tile fijo que se genera al terminar la zona y contiene la puerta")]
    public GameObject TransitionPrefab;
}
