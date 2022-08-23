using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopPortLib.Interfaces;

namespace Adam4017.Request
{
    internal class ReadSignalValueReq : IByteStream
    {
        private readonly string _addr = "01";
        public ReadSignalValueReq(string addr)
        {
            this._addr = addr;
        }

        public byte[] ToBytes()
        {
            return Encoding.ASCII.GetBytes(ToString());
        }

        public override string ToString()
        {
            return $"#{_addr}\r";
        }
    }
}
