using System;
using System.IO;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace VSMacDeepClean
{
    public class DroidLibCacheHandler : BaseDeepCleanHandler
    {
        static string droidLibCacheDirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.local/share/Xamarin";

        protected override void Run()
        {
            using (var monitor = IdeApp.Workbench.ProgressMonitors.GetToolOutputProgressMonitor(false))
            {
                monitor.BeginTask(1);
                monitor.Log.WriteLine("Cleaning Android library cache..");
                monitor.Log.WriteLine();

                var cacheDirExists = Directory.Exists(droidLibCacheDirPath);
                if (cacheDirExists)
                {
                    var cacheDirs = Directory.GetDirectories(droidLibCacheDirPath, "Xamarin.*");
                    monitor.Log.WriteLine($"{cacheDirs.Length} directories to delete.");

                    foreach (var cacheDir in cacheDirs)
                    {
                        Directory.Delete(cacheDir, true);
                        monitor.Log.WriteLine($"{cacheDir} have been deleted.");
                    }

                    monitor.EndTask();
                    monitor.ReportSuccess("Succesfully cleaned Android library cache.");
                }
                else
                {
                    monitor.EndTask();
                    monitor.ReportError($"Root cache dir '{droidLibCacheDirPath}' does not exist.");
                }
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = ProjectIsNotBuildingOrRunning();
        }
    }
}
