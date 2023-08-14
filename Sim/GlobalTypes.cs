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

public class PlayerOptions
{
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int Rating { get; set; }
    public string Team { get; set; }
}

