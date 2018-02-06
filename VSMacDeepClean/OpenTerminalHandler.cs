using System.Diagnostics;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;

namespace VSMacDeepClean
{
    public class OpenTerminalHandler : BaseDeepCleanHandler
    {
        protected override void Run()
        {
            var selectedMenuItem = IdeApp.ProjectOperations.CurrentSelectedItem;
            if (!(selectedMenuItem is IFolderItem)) 
            {
                IdeApp.Workbench.StatusBar.ShowWarning("Cannot open terminal at selected item");
                return;   
            }
            var selectedMenuItemDirFullPath = (selectedMenuItem as IFolderItem)?.BaseDirectory.FullPath;
            OpenTerminalApp(selectedMenuItemDirFullPath);
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IsWorkspaceOpen()
                && IsCurrentSelectedItemSolution()
                | IsCurrentSelectedItemSolutionFolder()
                | IsCurrentSelectedItemProject()
                | IsCurrentSelectedItemFolder();
        }

        bool IsCurrentSelectedItemSolution() => ProjectOperations.CurrentSelectedItem is Solution;
        bool IsCurrentSelectedItemSolutionFolder() => ProjectOperations.CurrentSelectedItem is SolutionFolder;
        bool IsCurrentSelectedItemProject() => ProjectOperations.CurrentSelectedItem is Project;
        bool IsCurrentSelectedItemFolder() => ProjectOperations.CurrentSelectedItem is ProjectFolder;


        void OpenTerminalApp(string path)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "open";
            startInfo.Arguments = "-a Terminal.app " + path;
            var proc = Process.Start(startInfo);
        }
    }
}
