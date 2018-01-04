using System;
using System.Collections.Generic;
using System.IO;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace VSMacDeepClean
{
    public class ClearNugetCacheHandler : BaseDeepCleanHandler
    {

        List<string> nugetCacheDirectories = new List<string>
        { 
            $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nuget/packages",
            $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.local/share/NuGet" 
        };

        protected override void Run()
        {
            IdeApp.Workbench.StatusBar.BeginProgress($"Cleaning nuget cache..");
            nugetCacheDirectories.ForEach(cd =>
            {
                IdeApp.Workbench.StatusBar.ShowMessage($"Deleting {cd}..");
                ClearCacheDirectory(cd);
            });

            IdeApp.Workbench.StatusBar.EndProgress();
            IdeApp.Workbench.StatusBar.ShowMessage("Succesfully deleted ~/.nuget/packages & ~/.local/share/NuGet directories.");
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = ProjectIsNotBuildingOrRunning();
        }

        void ClearCacheDirectory(string cacheDirPath)
        {
            var cacheDirExists = Directory.Exists(cacheDirPath);
            if (!cacheDirExists) return;

            Directory.Delete(cacheDirPath, true);
        }
    }
}