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
            string selectedMenuItemDirFullPath = null;
            if (selectedMenuItem is Solution)
                selectedMenuItemDirFullPath = (selectedMenuItem as Solution).BaseDirectory.FullPath;
            else if (selectedMenuItem is Project)
                selectedMenuItemDirFullPath = (selectedMenuItem as Project).BaseDirectory.FullPath;
            else if (selectedMenuItem is ProjectFolder)
                selectedMenuItemDirFullPath = (selectedMenuItem as ProjectFolder).Path.FullPath;

            OpenTerminalApp(selectedMenuItemDirFullPath);
        }

        protected override void Run(object dataItem)
        {
            base.Run(dataItem);
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IsWorkspaceOpen()
                && IsCurrentSelectedItemSolution()
                | IsCurrentSelectedItemProject()
                | IsCurrentSelectedItemFolder();
        }

        bool IsCurrentSelectedItemSolution() => ProjectOperations.CurrentSelectedItem is Solution;
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
