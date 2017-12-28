using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace VSMacDeepClean
{
    public class DeleteBinObjDirsHandler : BaseDeepCleanHandler
    {
        protected override void Run()
        {
            if (!IsItSafeToExecuteTheCommand())
                return;

            IdeApp.Workbench.StatusBar.BeginProgress("Deleting /bin & /obj directories");

            var solutionItems = ProjectOperations.CurrentSelectedSolution.Items;

            foreach (var item in solutionItems)
            {
                var binPath = item.BaseDirectory.FullPath + "/bin";

                if (FileService.IsValidPath(binPath) && FileService.IsDirectory(binPath))
                    FileService.DeleteDirectory(binPath);

                var objPath = item.BaseDirectory.FullPath + "/obj";

                if (FileService.IsValidPath(objPath) && FileService.IsDirectory(objPath))
                    FileService.DeleteDirectory(objPath);
            }

            IdeApp.Workbench.StatusBar.EndProgress();
            IdeApp.Workbench.StatusBar.ShowMessage("Deleted /bin & /obj directories successfully");
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IsCommandEnabled();
        }
    }
}
