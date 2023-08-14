using System.Collections;

namespace Sim;

public class Battle
{
    private List<string> log;
    private Format format;
    public ModdedDex Dex { get; }
    private int gen;
    private RuleTable ruleTable;
    private Field field;
    private Side[] sides;
    private int activePerHalf;
    private Random prng;
    private readonly BattleQueue queue;
    private readonly BattleActions actions;
    private readonly Queue<FaintQueueData> faintQueue;
    private readonly List<string> inputLog;
    private readonly List<string> messageLog;
    private readonly int sentLogPos;
    private readonly bool sentEnd;
    private readonly string requestState;
    private readonly int turn;
    private readonly bool midTurn;
    private readonly bool started;
    private readonly bool ended;
    private Effect effect;
    private EffectState effectState;
    private AnyObject _event;
    private AnyObject events;
    private readonly int eventDepth;
    private ActiveMove activeMove;
    private readonly Pokemon activePokemon;
    private readonly Pokemon activeTarget;
    private ActiveMove lastMove;
    private readonly int lastMoveLine;
    private readonly string lastSuccessfulMoveThisTurn;
    private readonly int lastDamage;
    private readonly int abilityOrder;
    private readonly bool quickClawRoll;
    private readonly HashSet<string> hints;
    private readonly TeamGenerator teamGenerator;

    private string notFail;
    private int hitSubstitute;
    private bool fail;
    private object silentFail;

    private Action<string, string[]> send;

    public Battle(BattleOptions options)
    {
        this.log = new List<string>();
        this.add("t:", DateTime.Now.Millisecond);
        this.format = options.Format ?? DexSingleton.I.Formats.Get(options.Formatid, true);
        this.Dex = DexSingleton.I.ForFormat(this.format);
        this.gen = this.Dex.gen;
        this.ruleTable = this.Dex.Formats.GetRuleTable(this.format);

        this.field = new Field(this);
        this.sides = new Side[2];
        this.activePerHalf = 1;
        this.prng = options.prng ?? (options.seed == null ? new Random() : new Random(options.seed.Value));
        this.queue = new BattleQueue(this);
        this.actions = new BattleActions(this);
        this.faintQueue = new Queue<FaintQueueData>();

        this.inputLog = new List<string>();
        this.messageLog = new List<string>();
        this.sentLogPos = 0;
        this.sentEnd = false;

        this.requestState = "";
        this.turn = 0;
        this.midTurn = false;
        this.started = false;
        this.ended = false;

        this.effect = null;
        this.effectState = null;

        this._event = null;
        this.events = null;
        this.eventDepth = 0;

        this.activeMove = null;
        this.activePokemon = null;
        this.activeTarget = null;

        this.lastMove = null;
        this.lastMoveLine = -1;
        this.lastSuccessfulMoveThisTurn = null;
        this.lastDamage = 0;
        this.abilityOrder = 0;
        this.quickClawRoll = false;

        this.teamGenerator = null;

        this.hints = new HashSet<string>();

        this.notFail = "";
        this.hitSubstitute = 0;
        this.fail = false;
        this.silentFail = null;

        this.send = options.Send ?? ((_, _) => { });
    }

    private void add(params Part[] parts)
    {
        this.log.Add($"|{string.Join('|', parts.ToString())}");
    }

    public void SetPlayer(string slot, PlayerOptions optionsText)
    {
        Side side;
        var didSomething = true;
        var slotNum = int.Parse(slot[1].ToString()) - 1;
        if (this.sides[slotNum] == null)
        {
            var team = this.GetTeam(optionsText);
        }
    }

    private List<PokemonSet> GetTeam(PlayerOptions options)
    {
        var team = Teams.Unpack(options.Team);
        return team;
    }
}

internal class FaintQueueData
{
    public Pokemon Target { get; set; }
    public Pokemon Source { get; set; }
    public Effect Effect { get; set; }
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