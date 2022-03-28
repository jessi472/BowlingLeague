using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    public class BowlersViewModel
    {
        public IQueryable<Bowler> Bowlers { get; set; }
        public IQueryable<Team> Teams { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
