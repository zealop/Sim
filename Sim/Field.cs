using Sim.Pokemons;

namespace Sim;

public class Field
{
    private Battle battle;
    private string id;
    private string weather;
    private EffectState weatherState;
    private string terrain;
    private EffectState terrainState;
    private Dictionary<string, EffectState> pseudoWeather;

    public Field(Battle battle)
    {
        this.battle = battle;
        this.id = "";
        this.weather = "";
        this.weatherState = new EffectState() { { "id", "" } };
        this.terrain = "";
        this.terrainState = new EffectState() { { "id", "" } };
        this.pseudoWeather = new Dictionary<string, EffectState>();
    }
}