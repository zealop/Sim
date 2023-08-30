using Lombok.NET;
using OneOf;

namespace Sim;

public enum RequestState
{
    TeamPreview,
    Move,
    Switch,
    Blank
}

public partial class Battle
{
    [Property] private string _id;
    [Property] private bool _debugMode;
    [Property] private bool _forceRandomChance;
    [Property] private bool _deserialized;
    [Property] private bool _strictChoices;
    [Property] private Format _format;
    [Property] private EffectState _formatData;
    [Property] private GameType _gameType;
    [Property] private int _activePerHalf;
    [Property] private Field _field;
    [Property] private Side[] _sides;
    [Property] private int _prngSeed;
    [Property] private ModdedDex _dex;
    [Property] private int _gen;
    [Property] private RuleTable _ruleTable;
    [Property] private Random _prng;
    [Property] private bool _rated;
    [Property] private bool _reportExactHp;
    [Property] private bool _reportPercentages;
    [Property] private bool _supportCancel;

    [Property] private BattleActions _actions;
    [Property] private BattleQueue _queue;
    [Property] private Queue<FaintQueueData> _faintQueue;

    [Property] private List<string> _log;
    [Property] private List<string> _inputLog;
    [Property] private List<string> _messageLog;
    [Property] private int _sentLogPos;
    [Property] private bool _sentEnd;

    [Property] private RequestState _requestState;
    [Property] private int _turn;
    [Property] private bool _midTurn;
    [Property] private bool _started;
    [Property] private bool _ended;
    [Property] private string _winner;

    [Property] private Effect _effect;
    [Property] private EffectState _effectState;

    [Property] private AnyObject _event;
    [Property] private AnyObject _events;
    [Property] private int _eventDepth;

    [Property] private ActiveMove _activeMove;
    [Property] private Pokemon _activePokemon;
    [Property] private Pokemon _activeTarget;

    [Property] private ActiveMove _lastMove;
    [Property] private string _lastSuccessfulMoveThisTurn;
    [Property] private int _lastMoveLine;
    [Property] private int _lastDamage;
    [Property] private int _abilityOrder;

    [Property] private bool _quickClawRoll;

    [Property] private TeamGenerator _teamGenerator;

    [Property] private HashSet<string> _hints;

    [Property] private string _notFail;
    [Property] private int _hitSubstitute;
    [Property] private bool _fail;
    [Property] private object _silentFail;

    [Property] private Action<string, string[]> _send;

    public Battle(BattleOptions options)
    {
        this._log = new List<string>();
        this.add("t:", DateTime.Now.Millisecond);
        this._format = options.Format ?? DexSingleton.I.Formats.Get(options.FormatId, true);
        this.Dex = DexSingleton.I.ForFormat(this._format);
        this._gen = this.Dex.gen;
        this._ruleTable = this.Dex.Formats.GetRuleTable(this._format);

        this._field = new Field(this);
        this._sides = new Side[2];
        this._activePerHalf = 1;
        this._prng = options.Prng ?? (options.Seed == null ? new Random() : new Random(options.Seed.Value));
        this._queue = new BattleQueue(this);
        this._actions = new BattleActions(this);
        this._faintQueue = new Queue<FaintQueueData>();

        this._inputLog = new List<string>();
        this._messageLog = new List<string>();
        this._sentLogPos = 0;
        this._sentEnd = false;

        this._requestState = RequestState.Blank;
        this._turn = 0;
        this._midTurn = false;
        this._started = false;
        this._ended = false;

        this._effect = null;
        this._effectState = null;

        this._event = null;
        this._events = null;
        this._eventDepth = 0;

        this._activeMove = null;
        this._activePokemon = null;
        this._activeTarget = null;

        this._lastMove = null;
        this._lastMoveLine = -1;
        this._lastSuccessfulMoveThisTurn = null;
        this._lastDamage = 0;
        this._abilityOrder = 0;
        this._quickClawRoll = false;

        this._teamGenerator = null;

        this._hints = new HashSet<string>();

        this._notFail = "";
        this._hitSubstitute = 0;
        this._fail = false;
        this._silentFail = null;

        this._send = options.Send ?? ((_, _) => { });
    }

    private void add(params Part[] parts)
    {
        this._log.Add($"|{string.Join('|', parts.ToString())}");
    }

    public void SetPlayer(int slot, PlayerOptions options)
    {
        Side side;
        var didSomething = true;
        if (this.sides[slot] == null)
        {
            side = new Side(slot, options, this);
        }
    }

    public int CanSwitch(Side side)
    {
        return this.PossibleSwitches(side).Count;
    }

    private List<Pokemon> PossibleSwitches(Side side)
    {
        if (side.PokemonLeft == 0) return new List<Pokemon>();

        List<Pokemon> canSwitchIn = new List<Pokemon>();
        for (var i = side.Active.Length; i < side.Pokemon.Length; i++)
        {
            var pokemon = side.Pokemon[i];
            if (!pokemon.Fainted)
            {
                canSwitchIn.Add(pokemon);
            }
        }

        return canSwitchIn;
    }
}

internal class FaintQueueData
{
    public Pokemon Target { get; set; }
    public Pokemon Source { get; set; }
    public Effect Effect { get; set; }
}

public partial class BattleOptions
{
    [Property] private Format _format;
    [Property] private string _formatId;

    /** Output callback */
    [Property] private Action<string, string[]> _send;

    [Property] private Random _prng; // PRNG override (you usually don't need this, just pass a seed)
    [Property] private int? _seed; // PRNG seed
    [Property] private OneOf<bool, string>? _rated; // Rated string
    [Property] private PlayerOptions _p1; // Player 1 data
    [Property] private PlayerOptions _p2; // Player 2 data
    [Property] private PlayerOptions _p3; // Player 3 data
    [Property] private PlayerOptions _p4; // Player 4 data
    [Property] private bool _debug; // show debug mode option

    [Property]
    private bool _forceRandomChance; // force Battle#randomChance to always return true or false (used in some tests)

    [Property] private bool _deserialized;
    [Property] private bool _strictChoices; // whether invalid choices should throw
}