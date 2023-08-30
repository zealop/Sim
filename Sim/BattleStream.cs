using Lombok.NET;
using Sim.Lib;

namespace Sim;

public partial class BattleStream
{
    private bool debug;
    [Property] private Battle _battle;

    public void Start(BattleOptions options)
    {
        this._battle = new Battle(options);
    }

    public void Player(int slot, PlayerOptions options)
    {
        this._battle.SetPlayer(slot, options);
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