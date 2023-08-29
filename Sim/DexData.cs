using Lombok.NET;

namespace Sim;

public class DexData
{
    
}

public partial class BasicEffect : EffectData
{
   [Property] private string _id;

   private string _name;
   private string _fullName;
   [Property] private EffectType _effectType;
}