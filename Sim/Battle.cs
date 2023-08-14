namespace Sim;

public class Battle
{
    private List<string> log;
    private Format format;
    private ModdedDex dex;
    private int gen;
    private RuleTable ruleTable;
    private Field field;
    private Side[] sides;
    private int activePerHalf;
    private Random prng;

    public Battle(BattleOptions options)
    {
        this.log = new List<string>();
        this.add("t:", DateTime.Now.Millisecond);
        this.format = options.Format ?? Dex.I.Formats.Get(options.Formatid, true);
        this.dex = Dex.I.ForFormat(this.format);
        this.gen = this.dex.gen;
        this.ruleTable = this.dex.Formats.GetRuleTable(this.format);

        this.field = new Field(this);
        this.sides = new Side[2];
        this.activePerHalf = 1;
        this.prng = options.prng ?? (options.seed == null ? new Random() : new Random(options.seed.Value));
        Console.WriteLine("a");
    }

    private void add(params Part[] parts)
    {
        this.log.Add($"|{string.Join('|', parts.ToString())}");
    }
}

public class BattleOptions
{
    public Format Format { get; set; }
    public string Formatid { get; set; }
    public Random prng { get; set; }
    public int? seed { get; set; }
    public bool Debug { get; set; }

    public Action<string, string[]> Send;
}