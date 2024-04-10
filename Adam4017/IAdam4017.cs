using ProtocolInterface;

namespace Adam4017;

public interface IAdam4017 : IProtocol
{
    /// <summary>
    /// 读信号量
    /// </summary>
    /// <param name="address">地址</param>
    /// <param name="tryCount">重试次数</param>
    /// <param name="timeOut">超时时间(-1则使用构造传入超时)</param>
    /// <param name="cancelToken">取消</param>
    /// <returns>信号量值</returns>
    Task<List<decimal>?> ReadSignalValueAsync(string address = "01", int tryCount = 0, int timeOut = -1, CancellationToken cancelToken = default);
}
