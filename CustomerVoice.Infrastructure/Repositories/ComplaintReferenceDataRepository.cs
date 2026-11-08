using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Domain.Entities;
using CustomerVoice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Domain.Entities;
using CustomerVoice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerVoice.Infrastructure.Repositories
{
    public class ComplaintReferenceDataRepository
        : IComplaintReferenceDataRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintReferenceDataRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> IsProjectActiveAsync(
            int projectId,
            CancellationToken cancellationToken = default)
        {
            return _context.Projects.AnyAsync(
                x => x.Id == projectId && x.IsActive,
                cancellationToken);
        }

        public Task<bool> IsServiceTypeActiveAsync(
            int serviceTypeId,
            CancellationToken cancellationToken = default)
        {
            return _context.ServiceTypes.AnyAsync(
                x => x.Id == serviceTypeId && x.IsActive,
                cancellationToken);
        }

        public async Task<List<Project>> GetActiveProjectsAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Where(x => x.IsActive)
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServiceType>> GetActiveServiceTypesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.ServiceTypes
                .Where(x => x.IsActive)
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DissatisfactionReason>>
            GetActiveDissatisfactionReasonsAsync(
                IReadOnlyCollection<int> ids,
                CancellationToken cancellationToken = default)
        {
            return await _context.DissatisfactionReasons
                .Where(x => ids.Contains(x.Id) && x.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DissatisfactionReason>>
            GetAllActiveDissatisfactionReasonsAsync(
                CancellationToken cancellationToken = default)
        {
            return await _context.DissatisfactionReasons
                .Where(x => x.IsActive)
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }
    }
}
