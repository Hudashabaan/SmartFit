using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;

using System.Threading.Tasks;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IUserProfileRepository
    {
        System.Threading.Tasks.Task AddAsync(UserProfile profile);
        System.Threading.Tasks.Task SaveChangesAsync();
    }
}