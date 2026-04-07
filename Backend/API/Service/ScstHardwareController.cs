using System.Diagnostics;

public class ScstHardwareController : IDvdHardwareController
{
    public async Task InsertAsync(string isoPath)
    {
        // Set ISO path
        await RunCommand($"echo \"{isoPath}\" > /sys/kernel/scst_tgt/devices/dvd0/filename");

        // Trigger insert (if needed)
        await RunCommand($"echo 1 > /sys/kernel/scst_tgt/devices/dvd0/load");
    }

    public async Task EjectAsync()
    {
        await RunCommand($"echo 1 > /sys/kernel/scst_tgt/devices/dvd0/eject");
    }

    private async Task RunCommand(string command)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        var process = Process.Start(psi);
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new Exception(await process.StandardError.ReadToEndAsync());
        }
    }
}