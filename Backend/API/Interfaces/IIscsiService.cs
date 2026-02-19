public interface IIscsiService
{
    Task<Iqn> CreateTargetAsync(CreateTargetRequest request);
    Task DeleteTargetAsync(Iqn iqn);

    Task ActivateTargetAsync(Iqn iqn);
    Task DeactivateTargetAsync(Iqn iqn);

    Task<IReadOnlyList<IscsiTargetInfo>> ListTargetsAsync();

    Task AddInitiatorAccessAsync(Iqn targetIqn, string initiatorIqn);
    Task RemoveInitiatorAccessAsync(Iqn targetIqn, string initiatorIqn);
}