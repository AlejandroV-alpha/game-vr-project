using UnityEngine;
using TMPro;

public class RankingRowUIs : MonoBehaviour
{
    public TMP_Text rowText;

    public void SetData(int position, RankingEntry entry)
    {
        rowText.text = $"#{position}   {entry.playerName}   {entry.score}s";
    }
}