using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEvents.Models
{
    public interface ISportRepository
    {
        IQueryable<Sport> Sport { get; }
        IQueryable<Admin> Admin { get; }
        IQueryable<Membership> Membership { get; }
        IQueryable<Member> Member { get; }

    }
}
