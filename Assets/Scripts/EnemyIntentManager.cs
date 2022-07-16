using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyIntentManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Attack, StrengthBuff, DefenseBuff;
    [SerializeField]
    private TextMeshPro textMesh;


    public void SetAttack(int damage, int numHits)
    {
        HideAll();
        Attack.enabled = true;
        textMesh.enabled = true;
        string text = "" + damage;
        if (numHits > 0)
        {
            text += "x" + numHits;
        }
        textMesh.text = text;
    }

    public void SetStrengthBuff(int amount, bool isAll)
    {
        HideAll();
        StrengthBuff.enabled = true;
        textMesh.enabled = true;
        string text = "+" + amount;
        if (isAll)
        {
            text += " all";
        }
        textMesh.text = text;
    }

    public void SetDefenseBuff(int amount, bool isAll)
    {
        HideAll();
        DefenseBuff.enabled = true;
        textMesh.enabled = true;
        string text = "+" + amount;
        if (isAll)
        {
            text += " all";
        }
        textMesh.text = text;
    }

    public void HideAll()
    {
        Attack.enabled = false;
        StrengthBuff.enabled = false;
        DefenseBuff.enabled = false;
        textMesh.enabled = false;
    }
}
