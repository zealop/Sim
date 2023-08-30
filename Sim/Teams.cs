using Lombok.NET;

namespace Sim;

public class Teams
{
    public static string Pack(PokemonSet[] team)
    {
        return null;
    }


    public static PokemonSet[] Generate(string format)
    {
        throw new NotImplementedException();
    }
}

public partial class PokemonSet
{
    [Property] private string _name;
    [Property] private Species _species;
    [Property] private string _item;
    [Property] private string _ability;
    [Property] private Move[] _moves;
    [Property] private string _nature;
    [Property] private GenderName _gender;
    [Property] private StatTable _evs;
    [Property] private StatTable _ivs;
    [Property] private int _level;
    [Property] private bool _shiny;
    [Property] private int _happiness;
    [Property] private string _pokeball;
    [Property] private string _hpType;
    [Property] private int _dynamaxLevel;
    [Property] private bool _gigantamax;
    [Property] private string _teraType;
}

public class StatTable
{
    public int hp, atk, def, spa, spd, spe;
}

public class TeamGenerator
{
}