using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync
{
    class pi
    {
        public pi(int roomid, string roomname, string ip, string sshfingerprint)
        {
            RoomID = roomid;
            RoomName = roomname;
            IP = ip;
            SSHfingerprint = sshfingerprint;
        }

        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string IP { get; set; }
        public string SSHfingerprint { get; set; }

    }
}
