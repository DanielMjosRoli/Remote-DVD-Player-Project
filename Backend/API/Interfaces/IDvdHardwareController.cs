public interface IDvdHardwareController
{
    Task InsertAsync(string isoPath);
    Task EjectAsync();
}