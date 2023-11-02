using Lombok.NET;

namespace Sim.Pokemons;

public class Pokemon
{
    #region readonly fields

    private Side Side { get; }
    private Battle Battle { get; }

    private PokemonSet Set { get; }
    private string Name { get; }
    private string Fullname { get; }
    private int Level { get; }
    private GenderName Gender { get; }
    private int Happiness { get; }
    private string Pokeball { get; }
    private int DynamaxLevel { get; }
    private bool Gigantamax { get; }

    private string BaseHpType { get; }
    private int BaseHpPower { get; }

    private MoveSlot[] BaseMoveSlots { get; }

    #endregion

    private MoveSlot[] MoveSlots { get; set; }

    private string HpType { get; set; }
    private int HpPower { get; set; }

    private int Position { get; set; }
    private string Details { get; set; }

    private Species BaseSpecies { get; set; }
    private Species Species { get; set; }
    private EffectState SpeciesState { get; set; }

    private string Status { get; set; }
    private EffectState StatusState { get; set; }
    private Dictionary<string, EffectState> Volatiles { get; set; }
    private bool ShowCure { get; set; }

    private StatsTable BaseStoredStats { get; set; }
    private StatsExceptHPTable StoredStats { get; set; }
    private BoostsTable Boosts { get; set; }

    private string BaseAbility { get; set; }
    private string Ability { get; set; }
    private EffectState AbilityState { get; set; }

    private string Item { get; set; }
    private EffectState ItemState { get; set; }
    private string LastItem { get; set; }
    private bool UsedItemThisTurn { get; set; }
    private bool AteBerry { get; set; }

    private bool Trapped { get; set; }
    private bool MaybeTrapped { get; set; }
    private bool MaybeDisabled { get; set; }

    private Pokemon Illusion { get; set; }
    private bool Transformed { get; set; }

    private int MaxHp { get; set; }
    private int BaseMaxHp { get; set; }
    private int Hp { get; set; }
    private bool Fainted { get; set; }
    private bool FaintQueued { get; set; }
    private bool SubFainted { get; set; }

    private string[] Types { get; set; }
    private string AddedType { get; set; }
    private bool KnownType { get; set; }
    private string ApparentType { get; set; }

    private bool SwitchFlag { get; set; }
    private bool ForceSwitchFlag { get; set; }
    private bool SkipBeforeSwitchOutEventFlag { get; set; }
    private int? DraggedIn { get; set; }
    private bool NewlySwitched { get; set; }
    private bool BeingCalledBack { get; set; }

    private ActiveMove LastMove { get; set; }

    private ActiveMove LastMoveEncore { get; set; }
    private ActiveMove LastMoveUsed { get; set; }
    private int LastMoveTargetLoc { get; set; }
    private string MoveThisTurn { get; set; }
    private bool StatsRaisedThisTurn { get; set; }
    private bool StatsLoweredThisTurn { get; set; }

    private bool? MoveLastTurnResult { get; set; }
    private bool? MoveThisTurnResult { get; set; }
    private int? HurtThisTurn { get; set; }
    private int LastDamage { get; set; }
    private List<Attacker> AttackedBy { get; set; }
    private int TimesAttacked { get; set; }

    private bool IsActive { get; set; }
    private int ActiveTurns { get; set; }
    private int ActiveMoveActions { get; set; }
    private int PreviouslySwitchedIn { get; set; }
    private bool TruantTurn { get; set; }

    private bool SwordBoost { get; set; }
    private bool ShieldBoost { get; set; }

    private bool IsStarted { get; set; }
    private bool DuringMove { get; set; }

    private int Weight { get; set; }
    private int Speed { get; set; }
    private int AbilityOrder { get; set; }

    private string CanMegaEvo { get; set; }
    private string CanUltraBurst { get; set; }
    private string CanGigantamax { get; set; }
    private string CanTerastallize { get; set; }
    private string TeraType { get; set; }
    private string[] BaseTypes { get; set; }
    private string Terastallized { get; set; }

    private Staleness? Staleness { get; set; }
    private Staleness? PendingStaleness { get; set; }
    private Staleness? VolatileStaleness { get; set; }

    private StatsExceptHPTable ModifiedStats { get; set; }
    private Action<Pokemon, StatIDExceptHp, int> ModifyStat { get; set; }
    private Action<Pokemon> RecalculateStats { get; set; }

    private M M { get; set; }

    public Pokemon(PokemonSet set, Side side)
    {
        Side = side;
        Battle = side.Battle;

        M = new M();

        BaseSpecies = set.Species;
        Set = set;

        Species = BaseSpecies;
        SpeciesState = new EffectState { { "id", Species.Id } };
        this.Name = set.Name;
        this.Fullname = $"{Side.Id}: {Name}";

        Level = set.Level;
        Gender = set.Gender;
        Happiness = set.Happiness;
        Pokeball = set.Pokeball;
        DynamaxLevel = set.DynamaxLevel;
        Gigantamax = set.Gigantamax;

        var moves = set.Moves.Length;
        BaseMoveSlots = new MoveSlot[moves];
        MoveSlots = new MoveSlot[moves];
        for (var i = 0; i < set.Moves.Length; i++)
        {
            var move = set.Moves[i];
            var basePp = move.Pp;
            BaseMoveSlots[i] = new MoveSlot()
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

        Position = 0;

        Status = "";
        StatusState = new EffectState();
        Volatiles = new Dictionary<string, EffectState>();
        ShowCure = false;

        (string _types, int power) hpData = ("", 1); //TODO
        HpType = set.HpType;
        HpPower = hpData.power;

        BaseHpType = this.HpType;
        BaseHpPower = this.HpPower;

        BaseStoredStats = null;
        StoredStats = new StatsExceptHPTable
        {
            { StatIDExceptHp.Atk, 0 },
            { StatIDExceptHp.Def, 0 },
            { StatIDExceptHp.Spa, 0 },
            { StatIDExceptHp.Spd, 0 },
            { StatIDExceptHp.Spe, 0 },
        };
        Boosts = new BoostsTable
        {
            { BoostID.Atk, 0 },
            { BoostID.Def, 0 },
            { BoostID.Spa, 0 },
            { BoostID.Spd, 0 },
            { BoostID.Spe, 0 },
            { BoostID.Acc, 0 },
            { BoostID.Eva, 0 },
        };

        BaseAbility = set.Ability;
        Ability = BaseAbility;
        AbilityState = new EffectState { { "id", Ability } };

        Item = set.Item;
        ItemState = new EffectState { { "id", this.Item } };
        LastItem = "";
        UsedItemThisTurn = false;
        AteBerry = false;

        this._trapped = false;
        this._maybeTrapped = false;
        this._maybeDisabled = false;

        this._illusion = null;
        this._transformed = false;

        this.Fainted = false;
        this.FaintQueued = false;
        this.SubFainted = false;

        this.Types = this._baseSpecies.Types;
        this._baseTypes = this.Types;
        this.AddedType = "";
        this.KnownType = true;
        this.ApparentType = string.Join('/', this.Types);
        this._teraType = this.Set.TeraType;

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

        this.ModifiedStats = new StatsExceptHPTable
        {
            { StatIDExceptHp.Atk, 0 },
            { StatIDExceptHp.Def, 0 },
            { StatIDExceptHp.Spa, 0 },
            { StatIDExceptHp.Spd, 0 },
            { StatIDExceptHp.Spe, 0 },
        };

        this.MaxHp = 0;
        this.BaseMaxHp = 0;
        this.Hp = 0;
        this.ClearVolatile();
        this.Hp = this.MaxHp;
    }

    private void ClearVolatile()
    {
        //TODO
    }

    public object GetMoveRequestData()
    {
        string lockedMove = null; //TODO

        var isLastActive = this.IsLastActive();
        var canSwitchIn = this.Battle.CanSwitch(Side) > 0;

        var moves = this.GetMoves(lockedMove, isLastActive);
        return null;
    }

    private MoveDto[] GetMoves(string lockedMove, bool restrictData)
    {
        if (lockedMove != null)
        {
            this._trapped = true;
            if ("recharge".Equals(lockedMove))
            {
                return new MoveDto[] { new() { Move = "Recharge", Id = "recharge" } };
            }

            foreach (var moveSlot in MoveSlots)
            {
                if (!moveSlot.Id.Equals(lockedMove)) continue;
                return new MoveDto[] { new() { Move = moveSlot.Move, Id = moveSlot.Id } };
            }

            return new MoveDto[] { new() { Move = lockedMove, Id = lockedMove } };
        }

        List<MoveDto> moves = new List<MoveDto>();
        var hasValidMove = false;

        foreach (var moveSlot in MoveSlots)
        {
            var moveName = moveSlot.Move;
            var target = moveSlot.Target;
            if ("curse".Equals(moveSlot.Id))
            {
                if (!this.HasType("Ghost"))
                {
                    // target = this.Battle.Dex.Moves
                }
            }
        }

        return null;
    }

    private bool HasType(params string[] ghost)
    {
        // const thisTypes  = this.GetTypes();
        return false;
    }

    private string[] GetTypes(bool excludeAdded = false, bool preTerastallized = false)
    {
        if (!preTerastallized && this._terastallized != null)
        {
            return new[] { this._terastallized };
        }

        var types = this.Battle.RunEvent("Type", this, null, null, this.Types);
        return null;
    }

    private bool IsLastActive()
    {
        if (!this._isActive) return false;
        var allyActive = Side.Active;
        for (var i = this._position + 1; i < allyActive.Length; i++)
        {
            if (allyActive[i] != null && !allyActive[i].Fainted) return false;
        }

        return true;
    }

    public Condition GetStatus()
    {
        return null;
    }
}

public partial class MoveDto
{
    [Property] private string _move;
    [Property] private string _id;
    [Property] private bool _disabled;
    [Property] private string _disabledSource;
    [Property] private string _target;
    [Property] private int _pp;
    [Property] private int _maxPp;
}