using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rti.CRUDTool
{
    public static class Extensions
    {
        public static void HookSaveChanges(this DbContext dbContext, EntityFrameworkHook.SaveChangesHookHandler funcDelegate)
        {
            new EntityFrameworkHook(dbContext, funcDelegate);
        }
    }
}
