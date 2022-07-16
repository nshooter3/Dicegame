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

    public void Roll(OnDiceEndRoll callback)
    {
        onDiceEndRoll += callback;
        isRolling = true;
        result = Random.Range(1, 7);
        diceRoller.wantedRoll = new List<int> { result };
        diceRoller.rollDice = true;
    }
}
