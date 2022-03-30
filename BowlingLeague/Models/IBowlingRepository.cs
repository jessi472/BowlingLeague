using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public interface IBowlingRepository
    { 

        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        void UpdateBowler(Bowler b);
        void SaveBowler (Bowler b);
        void CreateBowler(Bowler b);
        void DeleteBowler(Bowler b);
    }
}
