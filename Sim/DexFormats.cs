using System;
using System.Collections.Generic;

namespace Sim;

public class DexFormats
{
    private Dictionary<string, Format> rulesetCache = new()
    {
        { "gen1randombattle", Format.Gen1 }
    };

    public Format Get(string name, bool isTrusted = false)
    {
        var id = name;
        if (!name.Contains("@@@"))
        {
            var found = this.rulesetCache.TryGetValue(id, out var ruleSet);
            if (found) return ruleSet;
        }

        return null;
    }

    public RuleTable GetRuleTable(Format format)
    {
        return null;
    }
}

public class RuleTable
{
}

public class Format
{
    public static readonly Format Gen1 = new Format();

    public string Mod { get; set; }

    public string Team { get; set; }

    //TODO: public FormatEffectType EffectType { get; set; }
    public bool Debug { get; set; }
    public bool Rated { get; set; }
    public string[] Ruleset { get; set; }
    public string[] BaseRuleset { get; set; }
    public string[] BanList { get; set; }
    public string[] Restricted { get; set; }
    public string[] UnbanList { get; set; }

    public string[] CustomRules { get; set; }

    //TODO: public RuleTable Ruletable { get; set; }
    public Action<Battle> OnBegin { get; set; }
    public bool NoLog { get; set; }
}