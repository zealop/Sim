using Lombok.NET;

namespace Sim;

public partial class SpeciesAbility
{
    [Property] private string _first;
    [Property] private string _second;
    [Property] private string _hidden;
    [Property] private string _special;
}

public enum SpeciesTag
{
    Mythical,
    Restricted_Legendary,
    Sub_Legendary,
    Paradox
}

public partial class Species : BasicEffect
{
    public override EffectType EffectType => EffectType.Pokemon;

    [Property] private  string _baseSpecies;
    [Property] private  string _forme;
    [Property] private  string _baseForme;
    [Property] private  string[] _cosmeticFormes;
    [Property] private  string[] _otherFormes;
    [Property] private  string[] _formeOrder;
    [Property] private  string _spriteId;
    [Property] private  SpeciesAbility _abilities;
    [Property] private  string[] _types;
    [Property] private  string _addedType;
    [Property] private  string _prevo;
    [Property] private  string[] _evos;
    [Property] private  EvoType _evoType;
    [Property] private  string _evoCondition;
    [Property] private  string _evoItem;
    [Property] private  string _evoMove;
    [Property] private  string _evoRegion;
    [Property] private  int _evoLevel;
    [Property] private  bool _nfe;
    [Property] private  string[] _eggGroups;
    [Property] private  bool _canHatch;
    [Property] private  GenderName _gender;
    [Property] private  (int m, int f) _genderRatio;
    [Property] private StatsTable _baseStats;
    [Property] private  int _maxHp;
    [Property] private  int _bst;
    [Property] private  int _weightKg;
    [Property] private  int _weightHg;
    [Property] private  int _heightM;
    [Property] private  string _color;
    [Property] private  SpeciesTag[] _tags;
    [Property] private  bool _unreleasedHidden;
    [Property] private  bool _maleOnlyHidden;
    [Property] private  bool _isMega;
    [Property] private  bool _isPrimal;
    [Property] private  string _canGigantamax;
    [Property] private  bool _gmaxUnreleased;
    [Property] private  bool _cannotDynamax;
    [Property] private  string[] _battleOnly;
    [Property] private  string _requiredItem;
    [Property] private  string _requiredMove;
    [Property] private  string _requiredAbility;
    [Property] private  string[] _requiredItems;
    [Property] private  string _changesFrom;
    [Property] private  string[] _pokemonGoData;
    [Property] private  TierTypes _tier;
    [Property] private  TierTypes _doublesTier;
    [Property] private  TierTypes _natDexTier;
}

public enum EvoType
{
    Trade,
    UseItem,
    LevelMove,
    LevelExtra,
    LevelFriendship,
    LevelHold,
    Other
}