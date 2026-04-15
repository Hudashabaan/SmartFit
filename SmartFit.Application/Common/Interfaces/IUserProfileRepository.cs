using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IUserProfileRepository
    {
        Task AddAsync(UserProfile profile);
        Task SaveChangesAsync();
    }
}