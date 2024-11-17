using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.implementations
{
    public class CostumerRepository : RepositoryBase<Costumer>, ICostumerRepository
    {
        public CostumerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Costumer?> GetByEmailAsync(string email)
        {
            return await context.Set<Costumer>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
