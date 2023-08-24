using Lombok.NET;
using Newtonsoft.Json;
using Sim.Lib;

namespace Sim;

public partial class BattleStream
{
    private bool debug;
    [Property] private Battle battle;
    
    public void Start(BattleOptions options)
    {
        this.Battle = new Battle(options);
    }

    public void Player(int slot, PlayerOptions options)
    {
        this.battle.SetPlayer(slot, options);
    }
}

public class StreamData
{
    public ObjectReadWriteStream<string> omniscient { get; set; }
    public ObjectReadStream<string> spectator { get; set; }
    public ObjectReadWriteStream<string> p1 { get; set; }
    public ObjectReadWriteStream<string> p2 { get; set; }
    public ObjectReadWriteStream<string> p3 { get; set; }
    public ObjectReadWriteStream<string> p4 { get; set; }
}