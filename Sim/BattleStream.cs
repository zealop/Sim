using Newtonsoft.Json;
using Sim.Lib;

namespace Sim;

public class BattleStream
{
    private bool debug;
    private Battle battle;

    public static StreamData GetPlayerStreams(BattleStream stream)
    {
        return null;
    }

    public void write(string chunk)
    {
        writeLines(chunk);
    }

    private void writeLines(string chunk)
    {
        var lines = chunk.Split('\n');
        foreach (var line in lines)
        {
            if (!line.StartsWith(">")) continue;

            var temp = line.Substring(1).Split(' ', 2);
            string type = temp[0], message = temp[1];
            writeLine(type, message);
        }
    }

    private void writeLine(string type, string message)
    {
        switch (type)
        {
            case "start":
                var options = JsonConvert.DeserializeObject<BattleOptions>(message);
                options.Send = (t, data) =>
                {
                    var dataString = string.Join("\n", data);
                    pushMessage(t, dataString);
                    //TODO: if (t === 'end' && !this.keepAlive) this.pushEnd();
                };
                options.Debug = this.debug;
                this.battle = new Battle(options);
                break;
            case "player":
                var splits = message.Split(' ', 2);
                string slot = splits[0], optionsText = splits[1];
                this.battle.SetPlayer(slot, JsonConvert.DeserializeObject<PlayerOptions>(optionsText));
                break;
        }
    }

    private void pushMessage(string s, string dataString)
    {
        throw new NotImplementedException();
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