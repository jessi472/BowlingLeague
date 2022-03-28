using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public class EFBowlingRepository : IBowlingRepository
    {
        private BowlingDbContext context { get; set; }
        public EFBowlingRepository(BowlingDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Bowler> Bowlers => context.Bowlers;

        public IQueryable<Team> Teams => context.Teams;

        public void UpdateBowler(Bowler b)
        {
            context.Update(b);
        }
        public void SaveBowler(Bowler b)
        {
            
            context.SaveChanges();

        }
        public void CreateBowler(Bowler b)
        {
            context.Add(b);
            context.SaveChanges();

        }
        public void DeleteBowler(Bowler b)
        {
            context.Remove(b);
            context.SaveChanges();
        }

    }
}
