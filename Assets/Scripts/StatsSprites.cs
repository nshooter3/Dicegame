using TMPro;
using UnityEngine;

public class StatsSprites : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro attackNum, defenseNum;

    public void SetAttack(int num)
    {
        attackNum.text = "" + num;
    }

    public void SetDefense(int num)
    {
        defenseNum.text = "" + num;
    }
}
