namespace Sim.Pokemons;

public class MoveSlot
{
    public string Id { get; set; }
    public string Move { get; set; }
    public int Pp { get; set; }
    public int MaxPp { get; set; }
    public MoveTarget Target { get; set; }
    public bool Disabled { get; set; }
    public string DisabledSource { get; set; }
    public bool Used { get; set; }
    public bool Virtual { get; set; }
}