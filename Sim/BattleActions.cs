namespace Sim;

public class BattleActions
{
    private readonly Battle battle;
    private readonly ModdedDex dex;

    public BattleActions(Battle battle)
    {
        this.battle = battle;
        this.dex = battle.Dex;
    }
}