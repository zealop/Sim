using Lombok.NET;

namespace Sim;

public partial class ChosenAction
{
    public enum Type
    {
        Move,
        Switch,
        InstaSwitch,
        RevivalBlessing,
        Team,
        Shift,
        Pass
    }

    [Property] private Type _choice;
    [Property] private Pokemon _pokemon;
    [Property] private int _targetLoc;
    [Property] private string _moveId;
    [Property] private ActiveMove _move;
    [Property] private Pokemon _target;
    [Property] private int _index;
    [Property] private Side _side;
    [Property] private bool _mega;
    [Property] private string _zMove;
    [Property] private string _maxMove;
    [Property] private string _terastallize;
    [Property] private int _priority;
}

public partial class Choice
{
    [Property] private bool _cantUndo;
    [Property] private string _error;
    [Property] private ChosenAction[] _actions;
    [Property] private int _forcedSwitchesLeft;
    [Property] private int _forcedPassesLeft;
    [Property] private HashSet<int> _switchIns;
    [Property] private bool _zMove;
    [Property] private bool _mega;
    [Property] private bool _ultra;
    [Property] private bool _dynamax;
    [Property] private bool _terastallize;
}

public partial class Side
{
    [Property] private readonly Battle _battle;
    [Property] private readonly int _id;

    /**
     * Index in `battle.sides`: `battle.sides[side.n] === side`
     */
    private readonly int _n;

    private string _name;
    private string _avatar;
    private Side _foe;
    private Side _allySide;
    private PokemonSet[] _team;
    private Pokemon[] _pokemon;
    private Pokemon[] _active;

    private int _pokemonLeft;
    private bool _zMoveUsed;
    private bool _dynamaxUsed;

    private Pokemon _faintedLastTurn;
    private Pokemon _faintedThisTurn;
    private int _totalFainted;
    private string _lastSelectedMove;

    private Dictionary<string, EffectState> _sideConditions;
    private Dictionary<string, EffectState>[] _slotConditions;

    private AnyObject _activeRequest;
    private Choice _choice;

    private Move _lastMove;

    public Side(int slot, PlayerOptions options, Battle battle)
    {
        this._battle = battle;
        this._id = slot;
        this._name = options.Name;
        this._team = options.Team;
        this._pokemon = this._team.Select((ps, i) => new Pokemon(ps, i, this)).ToArray();
        var slots = countSlots(this._battle.GameType);
        this._active = new Pokemon[slots];
        this._pokemonLeft = this._pokemon.Length;
        this._faintedLastTurn = null;
        this._faintedThisTurn = null;
        this._totalFainted = 0;
        this._zMoveUsed = false;
        this._dynamaxUsed = false;
        this._sideConditions = new Dictionary<string, EffectState>();
        this._slotConditions = new Dictionary<string, EffectState>[slots];
        for (var i = 0; i < slots; i++)
        {
            this._slotConditions[i] = new Dictionary<string, EffectState>();
        }

        this._activeRequest = null;
        this._choice = new Choice()
        {
            CantUndo = false,
            Error = "",
            Actions = new ChosenAction[0],
            ForcedSwitchesLeft = 0,
            ForcedPassesLeft = 0,
            SwitchIns = new HashSet<int>(),
            ZMove = false,
            Mega = false,
            Ultra = false,
            Dynamax = false,
            Terastallize = false
        };

        this._lastMove = null;
    }

    private int countSlots(GameType gameType)
    {
        return gameType switch
        {
            GameType.Doubles => 2,
            GameType.Triples or GameType.Rotation => 3,
            _ => 1
        };
    }
}