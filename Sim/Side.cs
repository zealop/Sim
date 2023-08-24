using System.Collections;

namespace Sim;

public class Side
{
    private Battle _battle;
    private int _id;
    private string _name;
    private PokemonSet[] _team;
    private Pokemon[] _pokemon;
    
    public Side(int slot, PlayerOptions options, Battle battle)
    {
        this._battle = battle;
        this._id = slot;
        this._name = options.Name;
        this._team = options.Team;
        this._pokemon = this._team.Select( (ps, i) => new Pokemon(ps, i,this)).ToArray();

    }
}