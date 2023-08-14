namespace Sim;

public class BattleQueue
{
    private Battle battle;
    private List<Action> list;
    public BattleQueue(Battle battle)
    {
        this.battle = battle;
        this.list = new List<Action>();
    }
}

public class Action
{
    
}