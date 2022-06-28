using Enno.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enno.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;

        public LayoutServices(AppDbContext context)
        {
            this._context = context;
        }
        public Dictionary<string,string> GetSetting()
        {
            return _context.Settings.ToDictionary(x=>x.Key,y=>y.Value);
        }
    }
}
