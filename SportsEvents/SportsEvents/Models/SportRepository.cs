using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEvents.Models
{
    public class SportRepository : ISportRepository
    {
        private ApplicationDbContext context;
        public SportRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Sport> Sport => context.Sport;
        public IQueryable<Admin> Admin => context.Admin;
        public IQueryable<Membership> Membership => context.Membership;
        public IQueryable<Member> Member => context.Member;
    }
}
