namespace Sim;

public class Dex
{
    public static ModdedDex I = new ModdedDex();
}

public class ModdedDex
{
    public int gen;
    public DexFormats Formats { get; set; }

    public ModdedDex ForFormat(Format format)
    {
        return null;
    }
}