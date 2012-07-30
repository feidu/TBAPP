using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class ScheduleEntity
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private long num_iid;

        public long Num_iid
        {
            get { return num_iid; }
            set { num_iid = value; }
        }
        private string nick;

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
    }
}
