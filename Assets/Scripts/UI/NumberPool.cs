using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPool : MonoBehaviour
{
    [SerializeField]
    private NumberPoolable prefab;

    private int poolSize = 10;
    private List<NumberPoolable> numbers = new List<NumberPoolable>();

    public static NumberPool instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            numbers.Add(Instantiate(prefab, transform));
        }
    }

    public void ShowNumber(int num, Vector3 pos, bool isPlayer = false, bool isDice = false, bool isCrit = false)
    {
        NumberPoolable numPoolable = GetAvailableNumber();
        string str = "";
        if (isCrit)
        {
            str += "Crit!\n";
        }
        str += num;
        numPoolable.SetNumText(str);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);

        if (isPlayer)
        {
            numPoolable.PlayerHit(screenPosition);
        }
        else if(isDice)
        {
            numPoolable.SmallHit(screenPosition);
        }
        else if (num > 15)
        {
            numPoolable.BigHit(screenPosition);
        }
        else if (num > 5)
        {
            numPoolable.Hit(screenPosition);
        }
        else
        {
            numPoolable.SmallHit(screenPosition);
        }
    }

    public void ShowStrengthBuff(int num, Vector3 pos, bool isPlayer = false)
    {
        string str = "+" + num + " Strength";
        NumberPoolable numPoolable = GetAvailableNumber();
        numPoolable.SetNumText(str);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        if (isPlayer)
        {
            numPoolable.PlayerAttackBuff(screenPosition);
        }
        else
        {
            numPoolable.AttackBuff(screenPosition);
        }
    }

    public void ShowDefenseBuff(int num, Vector3 pos, bool isPlayer = false)
    {
        string str = "+" + num + " Defense";
        NumberPoolable numPoolable = GetAvailableNumber();
        numPoolable.SetNumText(str);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        if (isPlayer)
        {
            numPoolable.PlayerDefenseBuff(screenPosition);
        }
        else
        {
            numPoolable.DefenseBuff(screenPosition);
        }
    }

    private NumberPoolable GetAvailableNumber()
    {
        foreach (NumberPoolable num in numbers)
        {
            if (num.available)
            {
                return num;
            }
        }
        Debug.LogError("Error: No poolable number available");
        return null;
    }
}
