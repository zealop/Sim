using OneOf;

namespace Sim;

public class Part : OneOfBase<string, int, bool>
{
    private Part(OneOf<string, int, bool> _) : base(_)
    {
    }
    
    public static implicit operator Part(string _) => new Part(_);
    public static implicit operator Part(int _) => new Part(_);
}