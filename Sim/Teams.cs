namespace Sim;

public class Teams
{
    public static string Pack(PokemonSet[] team)
    {
        return null;
    }


    public static PokemonSet[] Generate(string format)
    {
        throw new System.NotImplementedException();
    }

    public static List<PokemonSet> Unpack(string buf)
    {
        if (buf == null) return null;

        if (buf.StartsWith('[') && buf.EndsWith(']'))
        {
            //TODO:
        }

        var team = new List<PokemonSet>();
        int i = 0, j = 0;

        for (var count = 0; count < 24; count++)
        {
            var set = new PokemonSet();
            team.Add(set);

            //name
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.name = buf.Substring(i, j - i);
            i = j + 1;

            //species
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.species = set.name;
            i = j + 1;

            //item
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.item = buf.Substring(i, j - i);
            i = j + 1;

            //ability
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.ability = buf.Substring(i, j - i);
            i = j + 1;

            //moves
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.moves = buf.Substring(i, j - i).Split(',', 24);
            i = j + 1;

            //nature
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.nature = buf.Substring(i, j - i);
            i = j + 1;

            // evs
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            var evs = buf.Substring(i, j - i).Split(',', 6);
            set.evs = new StatTable()
            {
                hp = int.Parse(evs[0]),
                atk = int.Parse(evs[0]),
                def = int.Parse(evs[0]),
                spa = int.Parse(evs[0]),
                spd = int.Parse(evs[0]),
                spe = int.Parse(evs[0]),
            };
            i = j + 1;

            //gender
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.gender = buf.Substring(i, j - i);
            i = j + 1;

            // ivs
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            var ivs = buf.Substring(i, j - i).Split(',', 6);
            set.ivs = new StatTable()
            {
                hp = int.Parse(evs[0]),
                atk = int.Parse(evs[0]),
                def = int.Parse(evs[0]),
                spa = int.Parse(evs[0]),
                spd = int.Parse(evs[0]),
                spe = int.Parse(evs[0]),
            };
            i = j + 1;

            //shiny
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            bool.TryParse(buf.AsSpan(i, j - i), out set.shiny);
            i = j + 1;

            //level
            j = buf.IndexOf('|', i);
            if (j < 0) return null;
            set.level = int.Parse(buf.AsSpan(i, j - i));
            i = j + 1;

            //happiness
            j = buf.IndexOf('|', i);
            if (j < 0) break;
            i = j + 1;
        }

        return team;
    }
}

public class PokemonSet
{
    public string name;
    public string species;
    public string item;
    public string ability;
    public string[] moves;
    public string nature;
    public string gender;
    public StatTable evs;
    public StatTable ivs;
    public bool shiny;
    public int level;
}

public class StatTable
{
    public int hp, atk, def, spa, spd, spe;
}

public class TeamGenerator
{
}