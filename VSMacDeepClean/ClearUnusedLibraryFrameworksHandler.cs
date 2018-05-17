using Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Mono.Unix;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace VSMacDeepClean
{
    public class ClearUnusedLibraryFrameworksHandler : BaseDeepCleanHandler
    {
        List<string> libraryFrameworksDirectories = new List<string>
        {
            "/Library/Frameworks/Mono.framework/Versions",
            "/Library/Frameworks/Xamarin.Android.framework/Versions",
            "/Library/Frameworks/Xamarin.iOS.framework/Versions",
            "/Library/Frameworks/Xamarin.Mac.framework/Versions"
        };

        protected override void Run()
        {
            using (var monitor = IdeApp.Workbench.ProgressMonitors.GetToolOutputProgressMonitor(false))
            {
                monitor.BeginTask(1);

                var unusedDirectories = new List<string>();
                libraryFrameworksDirectories.ForEach(dir => unusedDirectories.AddRange(GetUnusedDirectories(dir)));

                if (unusedDirectories.Any())
                {
                    monitor.Log.WriteLine("Unused framework libraries found:");
                    monitor.Log.WriteLine(string.Join(Environment.NewLine, unusedDirectories));

                    var authStatus = DeleteWithPrivileges(unusedDirectories);
                    if (authStatus == AuthorizationStatus.Success)
                    {
                        monitor.ReportSuccess("Succesfully deleted unused framework libraries.");
                    }
                    else
                    {
                        monitor.ReportError(string.Format("Failed with error: {0}", authStatus));
                    }
                }
                else
                {
                    monitor.ReportWarning("No framework libraries were deleted.");
                }

                monitor.EndTask();
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = ProjectIsNotBuildingOrRunning();
        }

        IEnumerable<string> GetUnusedDirectories(string dirPath)
        {
            IEnumerable<string> directories = Enumerable.Empty<string>();

            if (!Directory.Exists(dirPath))
            {
                return directories;
            }

            directories = from consideredDirectory in new DirectoryInfo(dirPath).EnumerateDirectories() select consideredDirectory.FullName;

            // The Xamarin packages installed always refer to a specific version of the framework.
            // This version shouldnt be removed even if Current links to an older or newer version.
            if (GetVersionFromPackage(dirPath) is string versionFromPackage && versionFromPackage != null)
            {
                directories = directories.Where(dir => !dir.Contains(versionFromPackage));
            }

            var excludeDirectories = new List<string>();

            // Symbolic link Current points to the framework used from Visual Studio.
            // Both Current and the targeted directory should be preserved.
            var current = (from dir in directories
                           where dir.Contains("Current")
                           select new UnixSymbolicLinkInfo(dir)).SingleOrDefault();

            if (current.Exists && current.IsSymbolicLink)
            {
                excludeDirectories.Add(current.FullName);
            }

            if (current.HasContents &&
                current.GetContents() is UnixFileSystemInfo currentTarget &&
                currentTarget.Exists &&
                currentTarget.IsDirectory)
            {
                excludeDirectories.Add(currentTarget.FullName);
            }

            directories = directories.Except(excludeDirectories);

            return directories;
        }

        string GetVersionFromPackage(string libraryFrameworksDirectory)
        {
            string pkgName = null;

            if (libraryFrameworksDirectory.Contains("Android"))
            {
                pkgName = "com.xamarin.android.pkg";
            }
            else if (libraryFrameworksDirectory.Contains("iOS"))
            {
                pkgName = "com.xamarin.xamarin.ios.pkg";
            }
            else if (libraryFrameworksDirectory.Contains("Mono"))
            {
                pkgName = "com.xamarin.mono-MDK.pkg";
            }
            else
            {
                return null;
            }

            // iOS package version is always 1 (pkgutil --pkg-info com.xamarin.xamarin.ios.pkg | grep version | cut -d' ' -f2), calculation gets more complex.
            string versionNr = ExecuteBashCommand(string.Format("basename $(pkgutil --only-dirs --files {0} | grep \"/Versions/\" | head -n 1)", pkgName));
            // Filter out whitespaces.
            versionNr = new string(versionNr.Where(c => !char.IsWhiteSpace(c)).ToArray());

            return versionNr;
        }

        string ExecuteBashCommand(string command)
        {
            // Escape double quotes:
            // https://stackoverflow.com/a/15262019/637142
            command = command.Replace("\"", "\"\"");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                }
            };

            proc.Start();
            proc.WaitForExit();

            return proc.StandardOutput.ReadToEnd();
        }

        AuthorizationStatus DeleteWithPrivileges(List<string> files)
        {
            var authFlags = AuthorizationFlags.Defaults;

            using (var auth = Authorization.Create(authFlags))
            {
                files.Insert(0, "-rf");
                var status = (AuthorizationStatus)auth.ExecuteWithPrivileges("/bin/rm", authFlags, files.ToArray());

                return status;
            }
        }
    }
}