namespace Sim.Pokemons;

public class Attacker
{
    public Pokemon Source { get; set; }
    public int Damage { get; set; }
    public bool ThisTurn { get; set; }
    public string Move { get; set; }
    public string Slot { get; set; }
    public int? DamageValue { get; set; }
}