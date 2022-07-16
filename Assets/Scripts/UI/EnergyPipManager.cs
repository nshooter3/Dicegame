using System.Collections.Generic;
using UnityEngine;

public class EnergyPipManager : MonoBehaviour
{
    [SerializeField]
    private List<EnergyPip> pips;

    private int maxPips = 3;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxPips(3);
    }

    public void SetMaxPips(int maxPips)
    {
        this.maxPips = maxPips;
        for (int i = 0; i < pips.Count; i++)
        {
            pips[i].ToggleImage(i < maxPips);
        }
    }

    public void SetCurrentPips(int currentPips)
    {
        for (int i = 0; i < pips.Count; i++)
        {
            if (i < currentPips)
            {
                pips[i].SetFull();
            }
            else
            {
                pips[i].SetEmpty();
            }
        }
    }
}
