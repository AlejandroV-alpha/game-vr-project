using UnityEngine;
using TMPro;

public class RankingRowUIs : MonoBehaviour
{
    public TMP_Text rowText;

    void Awake()
    {
        var rt = rowText.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        rt.localRotation = Quaternion.identity;
        rt.localScale = Vector3.one;
        rt.anchoredPosition3D = Vector3.zero;

        rowText.enableAutoSizing = true;
        rowText.fontSizeMin = 1f;
        rowText.fontSizeMax = 6f;
        rowText.textWrappingMode = TextWrappingModes.NoWrap;
        rowText.alignment = TextAlignmentOptions.MidlineLeft;
    }

    public void SetData(int position, RankingEntry entry)
    {
        int min = (int)(entry.seconds / 60f);
        float sec = entry.seconds % 60f;
        rowText.text = $"#{position}  {min:00}:{sec:00.0}  {entry.dateTime}";
    }
}