using System.Diagnostics;
using Sim;

var stopwatch = Stopwatch.StartNew();
var battleOptions = new BattleOptions { FormatId = "gen1customgame" };
var battle = new Battle(battleOptions);

var player1Options = new PlayerOptions
{
    Team = new PokemonSet[]
    {
        new()
        {
            Species = new Species { Name = "bulbasaur", Types = new[] { "grass" } },
            Moves = new Move[] { new() { Name = "tackle" } }
        }
    }
};

var player2Options = new PlayerOptions
{
    Team = new PokemonSet[]
    {
        new()
        {
            Species = new Species { Name = "charmander", Types = new[] { "fire" } },
            Moves = new Move[] { new() { Name = "tackle" } }
        }
    }
};

battle.SetPlayer(0, player1Options);
battle.SetPlayer(1, player2Options);

stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);