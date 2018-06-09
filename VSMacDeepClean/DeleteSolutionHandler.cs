using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using Xwt;

namespace VSMacDeepClean
{
    public class DeleteSolutionHandler : BaseDeepCleanHandler
    {
        protected override void Update(CommandInfo info)
        {
            info.Enabled = ProjectIsNotBuildingOrRunning() && ProjectOperations.CurrentSelectedSolution != null;
        }

        protected async override void Run()
        {
            using (var monitor = IdeApp.Workbench.ProgressMonitors.GetToolOutputProgressMonitor(false))
            {
                monitor.BeginTask(1);
                var solution = ProjectOperations.CurrentSelectedSolution;
                var solutionFullPath = solution.BaseDirectory.FullPath;
                try
                {
                    var isConfirmed = MessageDialog.Confirm(
                        $"Are you sure you want to delete '{solutionFullPath}'?",
                        "Please note that after performing this operation it might be impossible to restore the solution.",
                        Xwt.Command.Ok);
                    if (isConfirmed)
                    {
                        await IdeApp.Workspace.Close();
                        FileService.DeleteDirectory(solutionFullPath);
                        monitor.ReportSuccess($"Succesfully deleted: `{solutionFullPath}`.");
                    }
                }
                catch (Exception ex)
                {
                    monitor.ReportError($"Failed to delete '{solutionFullPath}' with: {ex}");
                }
                finally
                {
                    monitor.EndTask();
                }
            }
        }
    }
}
