using Lombok.NET;

namespace Sim;

public partial class MoveSlot
{
    [Property] private string _id;
    [Property] private string _move;
    [Property] private int _pp;
    [Property] private int _maxPp;
    [Property] private MoveTarget _target;
    [Property] private bool _disabled;
    [Property] private string _disabledSource;
    [Property] private bool _used;
    [Property] private bool _virtual;
}

public partial class Attacker
{
    [Property] private Pokemon _source;
    [Property] private int _damage;
    [Property] private bool _thisTurn;
    [Property] private string move;
    [Property] private string _slot;
    [Property] private int? _damageValue;
}

public class Pokemon
{
    private readonly Side _side;
    private readonly Battle _battle;

    private readonly PokemonSet _set;
    private readonly string _name;
    private readonly string _fullName;
    private readonly int _level;
    private readonly GenderName _gender;
    private readonly int _happiness;
    private readonly string _pokeball;
    private readonly int _dynamaxLevel;
    private readonly bool _gigantamax;

    private readonly string _baseHpType;
    private readonly int _baseHpPower;

    private readonly MoveSlot[] _baseMoveSlots;
    private MoveSlot[] _moveSlots;

    private string _hpType;
    private int _hpPower;

    private int _position;
    private string _details;

    private Species _baseSpecies;
    private Species _species;
    private EffectState _speciesState;

    private string _status;
    private EffectState _statusState;
    private Dictionary<string, EffectState> _volatiles;
    private bool _showCure;

    private StatsTable _baseStoredStats;
    private StatsExceptHPTable _storedStats;
    private BoostsTable _boosts;

    private string _baseAbility;
    private string _ability;
    private EffectState _abilityState;

    private string _item;
    private EffectState _itemState;
    private string _lastItem;
    private bool _usedItemThisTurn;
    private bool _ateBerry;

    private bool _trapped;
    private bool _maybeTrapped;
    private bool _maybeDisabled;

    private Pokemon _illusion;
    private bool _transformed;

    private int _maxHp;
    private int _baseMaxHp;
    private int _hp;
    private bool _fainted;
    private bool _faintQueued;
    private bool _subFainted;

    private string[] _types;
    private string _addedType;
    private bool _knownType;
    private string _apparentType;

    private bool _switchFlag;
    private bool _forceSwitchFlag;
    private bool _skipBeforeSwitchOutEventFlag;
    private int? _draggedIn;
    private bool _newlySwitched;
    private bool _beingCalledBack;

    private ActiveMove _lastMove;

    private ActiveMove _lastMoveEncore;
    private ActiveMove _lastMoveUsed;
    private int _lastMoveTargetLoc;
    private string _moveThisTurn;
    private bool _statsRaisedThisTurn;
    private bool _statsLoweredThisTurn;

    private bool? _moveLastTurnResult;
    private bool? _moveThisTurnResult;
    private int? _hurtThisTurn;
    private int _lastDamage;
    private List<Attacker> _attackedBy;
    private int _timesAttacked;

    private bool _isActive;
    private int _activeTurns;
    private int _activeMoveActions;
    private int _previouslySwitchedIn;
    private bool _truantTurn;

    private bool _swordBoost;
    private bool _shieldBoost;

    private bool _isStarted;
    private bool _duringMove;

    private int _weight;
    private int _speed;
    private int _abilityOrder;

    private string _canMegaEvo;
    private string _canUltraBurst;
    private string _canGigantamax;
    private string _canTerastallize;
    private string _teraType;
    private string[] _baseTypes;
    private string _terastallized;

    public enum Staleness
    {
        Internal,
        External
    }

    private Staleness? _staleness;
    private Staleness? _pendingStaleness;
    private Staleness? _volatileStaleness;

    private StatsExceptHPTable _modifiedStats;
    private Action<Pokemon, StatIDExceptHp, int> _modifyStat;
    private Action<Pokemon> _recalculateStats;

    public class M : Dictionary<string, object>
    {
        private bool _gluttonyFlag;
        private string _innate;
        private string _originalSpecies;
    }

    private M _m;

    public Pokemon(PokemonSet set, int position, Side side)
    {
        this._side = side;
        this._battle = side.Battle;

        this._m = new M();

        this._baseSpecies = set.Species;
        this._set = set;

        this._species = this._baseSpecies;
        this._speciesState = new EffectState { { "id", this._species.Id } };
        this._name = set.Name;
        this._fullName = $"{this._side.Id}: {this._name}";

        this._level = set.Level;
        this._gender = set.Gender;
        this._happiness = set.Happiness;
        this._pokeball = set.Pokeball;
        this._dynamaxLevel = set.DynamaxLevel;
        this._gigantamax = set.Gigantamax;

        var moves = set.Moves.Length;
        this._baseMoveSlots = new MoveSlot[moves];
        this._moveSlots = new MoveSlot[moves];
        for (var i = 0; i < set.Moves.Length; i++)
        {
            var move = set.Moves[i];
            var basePp = move.Pp;
            this._baseMoveSlots[i] = new MoveSlot()
            {
                Move = move.Name,
                Id = move.Id,
                MaxPp = basePp,
                Target = move.Target,
                Disabled = false,
                DisabledSource = "",
                Used = false
            };
        }

        this._position = 0;

        this._status = "";
        this._statusState = new EffectState();
        this._volatiles = new Dictionary<string, EffectState>();
        this._showCure = false;

        (string _types, int power) hpData = ("", 1); //TODO
        this._hpType = set.HpType;
        this._hpPower = hpData.power;

        this._baseHpType = this._hpType;
        this._baseHpPower = this._hpPower;

        this._baseStoredStats = null;
        this._storedStats = new StatsExceptHPTable
        {
            { StatIDExceptHp.Atk, 0 },
            { StatIDExceptHp.Def, 0 },
            { StatIDExceptHp.Spa, 0 },
            { StatIDExceptHp.Spd, 0 },
            { StatIDExceptHp.Spe, 0 },
        };
        this._boosts = new BoostsTable
        {
            { BoostID.Atk, 0 },
            { BoostID.Def, 0 },
            { BoostID.Spa, 0 },
            { BoostID.Spd, 0 },
            { BoostID.Spe, 0 },
            { BoostID.Acc, 0 },
            { BoostID.Eva, 0 },
        };

        this._baseAbility = set.Ability;
        this._ability = this._baseAbility;
        this._abilityState = new EffectState { { "id", this._ability } };

        this._item = set.Item;
        this._itemState = new EffectState { { "id", this._item } };
        this._lastItem = "";
        this._usedItemThisTurn = false;
        this._ateBerry = false;

        this._trapped = false;
        this._maybeTrapped = false;
        this._maybeDisabled = false;

        this._illusion = null;
        this._transformed = false;

        this._fainted = false;
        this._faintQueued = false;
        this._subFainted = false;

        this._types = this._baseSpecies.Types;
        this._baseTypes = this._types;
        this._addedType = "";
        this._knownType = true;
        this._apparentType = string.Join('/', this._types);
        this._teraType = this._set.TeraType;

        this._switchFlag = false;
        this._forceSwitchFlag = false;
        this._skipBeforeSwitchOutEventFlag = false;
        this._draggedIn = null;
        this._newlySwitched = false;
        this._beingCalledBack = false;

        this._lastMove = null;
        this._lastMoveEncore = null;
        this._lastMoveUsed = null;
        this._moveThisTurn = "";
        this._statsRaisedThisTurn = false;
        this._statsLoweredThisTurn = false;
        this._hurtThisTurn = null;
        this._lastDamage = 0;
        this._attackedBy = new List<Attacker>();
        this._timesAttacked = 0;

        this._isActive = false;
        this._activeTurns = 0;
        this._activeMoveActions = 0;
        this._previouslySwitchedIn = 0;
        this._truantTurn = false;
        this._swordBoost = false;
        this._shieldBoost = false;
        this._isStarted = false;
        this._duringMove = false;

        this._weight = 1;
        this._speed = 0;
        this._abilityOrder = 0;

        this._canMegaEvo = null; //TODO
        this._canUltraBurst = null; //TODO
        this._canGigantamax = null; //TODO
        this._canTerastallize = null; //TODO

        this._modifiedStats = new StatsExceptHPTable
        {
            { StatIDExceptHp.Atk, 0 },
            { StatIDExceptHp.Def, 0 },
            { StatIDExceptHp.Spa, 0 },
            { StatIDExceptHp.Spd, 0 },
            { StatIDExceptHp.Spe, 0 },
        };

        this._maxHp = 0;
        this._baseMaxHp = 0;
        this._hp = 0;
        this.ClearVolatile();
        this._hp = this._maxHp;
    }

    private void ClearVolatile()
    {
        //TODO
    }
}