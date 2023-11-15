using System.Text;
using Utils;

namespace Adam4017.Response;

internal class ReadSignalValueRsp
{
    public List<decimal> RecData { get; set; } = [];

    public ReadSignalValueRsp(byte[] rspBytes)
    {
        string str = Encoding.ASCII.GetString(rspBytes);
        var result = str.GetAllNum();
        if (result.Count != 8)
        {
            throw new Exception($"数据长度为{result.Count} {str}");
        }
        RecData = result;
    }
}
