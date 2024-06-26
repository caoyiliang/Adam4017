﻿using Adam4017.Request;
using Adam4017.Response;
using Communication;
using Communication.Bus.PhysicalPort;
using Communication.Exceptions;
using LogInterface;
using Parser.Parsers;
using ProtocolInterface;
using TopPortLib;
using TopPortLib.Interfaces;
using Utils;

namespace Adam4017;

public class Adam4017 : IAdam4017, IProtocol
{
    private static readonly ILogger _logger = Logs.LogFactory.GetLogger<Adam4017>();
    private readonly ICrowPort _crowPort;
    internal static readonly byte[] Foot = [0x0d];

    private bool _isConnect = false;
    public bool IsConnect => _isConnect;

    /// <inheritdoc/>
    public event DisconnectEventHandler? OnDisconnect { add => _crowPort.OnDisconnect += value; remove => _crowPort.OnDisconnect -= value; }
    /// <inheritdoc/>
    public event ConnectEventHandler? OnConnect { add => _crowPort.OnConnect += value; remove => _crowPort.OnConnect -= value; }

    public Adam4017(SerialPort serialPort, int defaultTimeout = 5000)
    {
        _crowPort = new CrowPort(new TopPort(serialPort, new FootParser(Foot)), defaultTimeout);
        _crowPort.OnSentData += CrowPort_OnSentData;
        _crowPort.OnReceivedData += CrowPort_OnReceivedData;
        _crowPort.OnConnect += CrowPort_OnConnect;
        _crowPort.OnDisconnect += CrowPort_OnDisconnect;
    }

    private async Task CrowPort_OnDisconnect()
    {
        _isConnect = false;
        await Task.CompletedTask;
    }

    private async Task CrowPort_OnConnect()
    {
        _isConnect = true;
        await Task.CompletedTask;
    }

    private async Task CrowPort_OnReceivedData(byte[] data)
    {
        _logger.Trace($"Adam4017 Rec:<-- {StringByteUtils.BytesToString(data)}");
        await Task.CompletedTask;
    }

    private async Task CrowPort_OnSentData(byte[] data)
    {
        _logger.Trace($"Adam4017 Send:--> {StringByteUtils.BytesToString(data)}");
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task OpenAsync() => _crowPort.OpenAsync();

    /// <inheritdoc/>
    public Task CloseAsync() => _crowPort.CloseAsync();

    /// <inheritdoc/>
    public async Task<List<decimal>?> ReadSignalValueAsync(string address = "01", int tryCount = 0, int timeOut = -1, CancellationToken cancelToken = default)
    {
        if (!_isConnect) throw new NotConnectedException();
        Func<Task<ReadSignalValueRsp>> func = () => _crowPort.RequestAsync<ReadSignalValueReq, ReadSignalValueRsp>(new ReadSignalValueReq(address), timeOut);
        return (await func.ReTry(tryCount, cancelToken))?.RecData;
    }
}