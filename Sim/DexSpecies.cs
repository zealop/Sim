using Lombok.NET;

namespace Sim;

public class DexSpecies
{
    
}

public partial class Species : BasicEffect
{
    this.EffectType = EffectType.Pokemon;
    [Property] private string[] _types;
}