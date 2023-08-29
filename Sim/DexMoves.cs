using Lombok.NET;

namespace Sim;

public enum MoveTarget
{
    AdjacentAlly,
    AdjacentAllyOrSelf,
    AdjacentFoe,
    All,
    AllAdjacent,
    AllAdjacentFoes,
    Allies,
    AllySide,
    AllyTeam,
    Any,
    FoeSide,
    Normal,
    RandomNormal,
    Scripted,
    Self
}

public partial class MoveFlags
{
    [Property] private bool _allyAnim;
    [Property] private bool _bypassSub;
    [Property] private bool _bite;
    [Property] private bool _bullet;
    [Property] private bool _charge;
    [Property] private bool _contact;
    [Property] private bool _dance;
    [Property] private bool _defrost;
    [Property] private bool _distance;
    [Property] private bool _failCopycat;
    [Property] private bool _failEncore;
    [Property] private bool _failInstruct;
    [Property] private bool _failMeFirst;
    [Property] private bool _failMimic;
    [Property] private bool _futureMove;
    [Property] private bool _gravity;
    [Property] private bool _heal;
    [Property] private bool _mirror;
    [Property] private bool _mustPressure;
    [Property] private bool _noAssist;
    [Property] private bool _nonSky;
    [Property] private bool _noParentalBond;
    [Property] private bool _noSleepTalk;
    [Property] private bool _pledgeCombo;
    [Property] private bool _powder;
    [Property] private bool _protect;
    [Property] private bool _pulse;
    [Property] private bool _punch;
    [Property] private bool _recharge;
    [Property] private bool _reflectable;
    [Property] private bool _slicing;
    [Property] private bool _snatch;
    [Property] private bool _sound;
    [Property] private bool _wind;
}

public class SecondaryEffect
{
}

public partial class MoveData : EffectData
{
    [Property] private string _name;
    [Property] private int _num;
    [Property] private ConditionData _condition;
    [Property] private int _basePower;
    [Property] private int _accuracy;
    [Property] private int _pp;

    [Property] private CategoryType _category;
    [Property] private string _type;
    [Property] private int _priority;
    [Property] private MoveTarget _target;
    [Property] private MoveFlags _flags;
    [Property] private string _realMove;
    [Property] private int _damage;
    [Property] private string _contestType;
    [Property] private bool _noPpBoosts;

    [Property] private bool _isZ;
    [Property] private ZMoveData _zMove;
    [Property] private bool _isMax;
    [Property] private MaxMoveData _maxMove;

    [Property] private bool _ohko;
    [Property] private bool _thawTarget;
    [Property] private int[] _heal;
    [Property] private bool _forceSwitch;
    [Property] private bool _selfSwitch;
    [Property] private BoostsTable _selfBoost;
    [Property] private bool _selfDestruct;
    [Property] private bool _breaksProtect;
    [Property] private int[] _recoil;
    [Property] private int[] _drain;
    [Property] private bool _mindBlownRecoil;
    [Property] private bool _stealsBoosts;
    [Property] private bool _struggleRecoil;
    [Property] private SecondaryEffect _secondary;
    [Property] private SecondaryEffect[] _secondaries;
    [Property] private SecondaryEffect _self;
    [Property] private bool _hasSheerForce;

    [Property] private bool _alwaysHit;
    [Property] private string _baseMoveType;
    [Property] private int _basePowerModifier;
    [Property] private int _critModifier;
    [Property] private int _critRatio;

    [Property] private string _overrideOffensivePokemon;
    [Property] private StatIDExceptHp _overrideOffensiveStat;
    [Property] private string _overrideDefensivePokemon;
    [Property] private StatIDExceptHp _overrideDefensiveStat;

    [Property] private bool _forceSTAB;
    [Property] private bool _ignoreAbility;
    [Property] private bool _ignoreAccuracy;
    [Property] private bool _ignoreDefensive;
    [Property] private bool _ignoreEvasion;
    [Property] private bool _ignoreImmunity;
    [Property] private bool _ignoreNegativeOffensive;
    [Property] private bool _ignoreOffensive;
    [Property] private bool _ignorePositiveDefensive;
    [Property] private bool _ignorePositiveEvasion;
    [Property] private bool _multiAccuracy;
    [Property] private int _multiHit;
    [Property] private string _multiHitType;
    [Property] private bool _noDamageVariance;
    [Property] private string _nonGhostTarget;
    [Property] private string _pressureTarget;
    [Property] private int _spreadModifier;
    [Property] private bool _sleepUsable;
    [Property] private bool _smartTarget;
    [Property] private bool _tracksTarget;
    [Property] private bool _willCrit;

    [Property] private bool _hasCrashDamage;
    [Property] private bool _isConfusionSelfHit;
    [Property] private string[] _noMetronome;
    [Property] private bool _noSketch;
    [Property] private bool _stallingMove;
    [Property] private string _baseMove;

    public enum CategoryType
    {
        Physical,
        Special,
        Status
    }

    public class ZMoveData
    {
        private int _basePower;
        private string _effect;
        private BoostsTable _boost;
    }

    public class MaxMoveData
    {
        private int _basePower;
    }
}

public class Move : MoveData
{
}