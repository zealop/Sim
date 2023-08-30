using Lombok.NET;

namespace Sim;

public class DexData
{
}

public abstract partial class BasicEffect : EffectData
{
    [Property] private string _name;
    [Property] private string _fullName;
    public abstract EffectType EffectType { get; }
    
    [Property] private bool _exists;
    [Property] private int _number;
    [Property] private bool _gen;
    [Property] private string _shortDesc;
    [Property] private string _desc;
    [Property] private Nonstandard? _isNonstandard;
    [Property] private int _duration;
    [Property] private bool _noCopy;
    [Property] private bool _affectsFainted;
    [Property] private string _status;
    [Property] private string _weather;
    [Property] private string _sourceEffect;
}