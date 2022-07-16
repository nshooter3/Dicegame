using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI attackNum, defenseNum;

    public void SetAttack(int num)
    {
        attackNum.text = "" + num;
    }

    public void SetDefense(int num)
    {
        defenseNum.text = "" + num;
    }
}

