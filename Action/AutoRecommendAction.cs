using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class AutoRecommendAction
    {
        public void ResultWrite(tb_RecommendResultEntity rre)
        {
            Transaction t = new Transaction();
            t.AddSaveObject(rre);
            try
            {
                t.Process();
            }
            catch (PlException plex)
            { }
        }
    }
}
