using System.Text;
using TopPortLib.Interfaces;

namespace Adam4017.Request;

internal class ReadSignalValueReq(string addr) : IByteStream
{
    public byte[] ToBytes()
    {
        return Encoding.ASCII.GetBytes(ToString());
    }

    public override string ToString()
    {
        return $"#{addr}\r";
    }
}
