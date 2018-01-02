using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace VSMacDeepClean
{
    public class DeletePackagesDirHandler : BaseDeepCleanHandler
    {
        protected override void Run()
        {
            if (!IsItSafeToExecuteTheCommand())
                return;

            IdeApp.Workbench.StatusBar.BeginProgress("Deleting /packages directory");

            var baseDir = ProjectOperations.CurrentSelectedSolution.RootFolder.BaseDirectory;

            var packagesPath = baseDir.FullPath + "/packages";

            if (FileService.IsValidPath(packagesPath) && FileService.IsDirectory(packagesPath))
                FileService.DeleteDirectory(packagesPath);

            IdeApp.Workbench.StatusBar.EndProgress();
            IdeApp.Workbench.StatusBar.ShowWarning("Deleted /packages directory successfully, please restore nuget packages");
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IsWorkspaceOpen();
        }
    }
}