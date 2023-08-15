using Lombok.NET;
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

public class Effect : OneOfBase<Format>
{
    private Effect(OneOf<Format> input) : base(input)
    {
    }
}

public class AnyObject : Dictionary<string, object>
{
}

public partial class PlayerOptions
{
    [Property] private string _name;
    [Property] private string _avatar;
    [Property] private int _rating;
    [Property] private string _team;
}