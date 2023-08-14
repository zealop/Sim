using System;

namespace Sim.Lib;

public class Stream
{
    
}

public class ObjectReadWriteStream<T>
{
    public ObjectReadWriteStream(ObjectReadWriteStreamOptions<T> options)
    {
        
    }

    public void write(string s)
    {
        throw new NotImplementedException();
    }
}

public class ObjectReadStream<T>
{
  
}

public class ObjectReadWriteStreamOptions<T>
{
    public static readonly ObjectReadWriteStreamOptions<T> Empty = new();
    public Action<ObjectReadStream<T>> Read { get; set; }
    public Action<ObjectReadStream<T>> Pause { get; set; }
    public Action<ObjectReadStream<T>> Destroy { get; set; }
    public Action<ObjectReadStream<T>, T> Write { get; set; }
}