using Lombok.NET;
using OneOf;

namespace Sim;

public class Part : OneOfBase<string, int, bool>
{
    private Part(OneOf<string, int, bool> _) : base(_)
    {
    }

    public static implicit operator Part(string _) => new Part(_);
    public static implicit operator Part(int _) => new Part(_);
}

public class Effect : OneOfBase<Format>
{
    private Effect(OneOf<Format> input) : base(input)
    {
    }
}

public class AnyObject : Dictionary<string, object>
{
}

[With]
public partial class PlayerOptions
{
    [Property] private string _name;
    [Property] private string _avatar;
    [Property] private int _rating;
    [Property] private PokemonSet[] _team;
}

public enum GameType
{
    Singles,
    Doubles,
    Triples,
    Rotation,
    Multi,
    FreeForAll
}

public enum GenderName
{
    Male,
    Female,
    Genderless,
    None
}

public enum StatID
{
    Atk,
    Def,
    Spa,
    Spd,
    Spe,
    Hp
}

public enum StatIDExceptHp
{
    Atk,
    Def,
    Spa,
    Spd,
    Spe
}

public class StatsTable : Dictionary<StatID, int>
{
}

public class StatsExceptHPTable : Dictionary<StatIDExceptHp, int>
{
}

public enum BoostID
{
    Atk,
    Def,
    Spa,
    Spd,
    Spe,
    Acc,
    Eva
}

public class BoostsTable : Dictionary<BoostID, int>
{
}

public partial class EffectData
{
    [Property] private string _id;
}

public enum EffectType
{
    Condition,
    Pokemon,
    Move,
    Item,
    Ability,
    Format,
    Nature,
    Ruleset,
    Weather,
    Status,
    Terastal,
    Rule,
    ValidatorRule
}