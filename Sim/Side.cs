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
    [Property] private List<ChosenAction> _actions;
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

    [Property] private string _name;
    [Property] private string _avatar;
    [Property] private Side _foe;
    [Property] private Side _allySide;
    [Property] private PokemonSet[] _team;
    [Property] private Pokemon[] _pokemon;
    [Property] private Pokemon[] _active;

    [Property] private int _pokemonLeft;
    [Property] private bool _zMoveUsed;
    [Property] private bool _dynamaxUsed;

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
            Actions = new List<ChosenAction>(),
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

    public bool Choose(string choiceType, string data, int? targetLoc = null, string moveEvent = "")
    {
        this.ClearChoice();

        switch (choiceType)
        {
            case "move":
                // this.ChooseMove(data, targetLoc, moveEvent);
                break;
            default:
                throw new SystemException();
        }

        return false;
    }

    public void ChooseMove(int moveIndex, int targetLoc = 0, string moveEvent = null)
    {
        int index = this.GetChoiceIndex();

        var autoChoose = false;
        var pokemon = this._active[index];

        var request = pokemon.GetMoveRequestData();
    }

    private int GetChoiceIndex(bool isPass = false)
    {
        var index = this._choice.Actions.Count;

        if (!isPass)
        {
            switch (this.RequestState)
            {
                case RequestState.Move:
                    while (index < this._active.Length && (this._active[index].Fainted ||
                                                           this._active[index].Volatiles.ContainsKey("commanding"))
                          )
                    {
                        this.ChoosePass();
                        index++;
                    }

                    break;
                case RequestState.Switch:
                    break;
            }
        }

        return index;
    }

    private void ChoosePass()
    {
    }

    private RequestState RequestState
    {
        get
        {
            if (this._activeRequest == null) return RequestState.Blank;
            if (this._activeRequest.ContainsKey("wait")) return RequestState.Blank;
            if (this._activeRequest.ContainsKey("teamPreview")) return RequestState.TeamPreview;
            if (this._activeRequest.ContainsKey("forceSwitch")) return RequestState.Switch;
            return RequestState.Move;
        }
    }

    private void ClearChoice()
    {
        var forceSwitches = 0;
        var forcedPasses = 0;
        if (this._battle.RequestState == RequestState.Switch)
        {
            var canSwitchOut = this._active.Count(p => p?.SwitchFlag == true);
            var canSwitchIn = this._pokemon.Take(this._active.Length).Count(p => p != null && !p.Fainted);

            forceSwitches = Math.Max(canSwitchIn, canSwitchOut);
            forcedPasses = canSwitchOut - forceSwitches;
        }

        this._choice = new Choice
        {
            CantUndo = false,
            Error = "",
            Actions = new List<ChosenAction>(),
            ForcedSwitchesLeft = forceSwitches,
            ForcedPassesLeft = forcedPasses,
            SwitchIns = new HashSet<int>(),
            ZMove = false,
            Mega = false,
            Ultra = false,
            Terastallize = false
        };
    }
}