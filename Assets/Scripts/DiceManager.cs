using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private DeterministicDiceRoller diceRoller;

    public bool isRolling;
    public int result;

    public delegate void OnDiceEndRoll(int result);
    public OnDiceEndRoll onDiceEndRoll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling && !diceRoller.simulationDone)
        {
            isRolling = false;
            onDiceEndRoll(result);
            onDiceEndRoll = null;
        }
    }

    public void Roll(OnDiceEndRoll callback, DeterministicDice.diceTypeList type, out bool wasCrit)
    {
        diceRoller.ActivateDie(type);
        onDiceEndRoll += callback;
        isRolling = true;
        int sides = GetSides(type);
        Debug.Log("D" + sides + " selected!");
        result = Random.Range(1, sides + 1);
        wasCrit = result == sides;
        diceRoller.wantedRoll = new List<int> { result };
        diceRoller.rollDice = true;
    }

    public int GetSides(DeterministicDice.diceTypeList type)
    {
        switch (type)
        {
            case DeterministicDice.diceTypeList.D4:
                return 4;
            case DeterministicDice.diceTypeList.D6:
                return 6;
            case DeterministicDice.diceTypeList.D8:
                return 8;
            case DeterministicDice.diceTypeList.D10:
                return 10;
            case DeterministicDice.diceTypeList.D12:
                return 12;
            case DeterministicDice.diceTypeList.D20:
                return 20;
        }
        Debug.LogError("Error: dice type " + type + " is invalid, unable to determine number of sides.");
        return -1;
    }

    public DeterministicDice.diceTypeList GetRandomDieType()
    {
        int num = Random.Range(0, 6);
        switch (num)
        {
            case 0:
                return DeterministicDice.diceTypeList.D4;
            case 1:
                return DeterministicDice.diceTypeList.D6;
            case 2:
                return DeterministicDice.diceTypeList.D8;
            case 3:
                return DeterministicDice.diceTypeList.D10;
            case 4:
                return DeterministicDice.diceTypeList.D12;
            case 5:
                return DeterministicDice.diceTypeList.D20;
        }
        Debug.LogError("Error: Invalid random dice outcome.");
        return DeterministicDice.diceTypeList.D6;
    }
}
