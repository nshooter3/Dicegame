[System.Serializable]
public class EnemyAction
{
    public enum EnemyActionType { Attack, StrengthBuff, DefenseBuff };
    public EnemyActionType enemyActionType = EnemyActionType.Attack;

    public int amount = 1;
    public int numTimes = 1;

    public enum EnemyBuffType { Self, Random, All };
    public EnemyBuffType enemyBuffType = EnemyBuffType.Self;
}
