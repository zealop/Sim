namespace Sim;

public class DexSingleton
{
    public static ModdedDex I = new()
    {
        Formats = new DexFormats()
    };
}

public class ModdedDex
{
    public int gen;
    public DexFormats Formats { get; set; }

    public ModdedDex ForFormat(Format format)
    {
        return this;
    }
}