using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPip : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Color fullColor, emptyColor;

    public void SetFull()
    {
        image.color = fullColor;
    }

    public void SetEmpty()
    {
        image.color = emptyColor;
    }

    public void ToggleImage(bool enabled)
    {
        image.enabled = enabled;
    }
}
