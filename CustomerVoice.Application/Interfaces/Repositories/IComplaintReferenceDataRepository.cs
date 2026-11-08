using CustomerVoice.Domain.Entities;


namespace CustomerVoice.Application.Interfaces.Repositories
{
    public interface IComplaintReferenceDataRepository
    {
        Task<bool> IsProjectActiveAsync(
            int projectId,
            CancellationToken cancellationToken = default);

        Task<bool> IsServiceTypeActiveAsync(
            int serviceTypeId,
            CancellationToken cancellationToken = default);

        Task<List<Project>> GetActiveProjectsAsync(
            CancellationToken cancellationToken = default);

        Task<List<ServiceType>> GetActiveServiceTypesAsync(
            CancellationToken cancellationToken = default);

        Task<List<DissatisfactionReason>> GetActiveDissatisfactionReasonsAsync(
            IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken = default);

        Task<List<DissatisfactionReason>> GetAllActiveDissatisfactionReasonsAsync(
            CancellationToken cancellationToken = default);
    }
}
