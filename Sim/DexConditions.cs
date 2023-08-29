using OneOf;

namespace Sim;

public class DexConditions
{
    
}

public class PokemonConditionData{}
public class ConditionData : OneOfBase<PokemonConditionData>
{
    protected ConditionData(OneOf<PokemonConditionData> input) : base(input)
    {
    }
}