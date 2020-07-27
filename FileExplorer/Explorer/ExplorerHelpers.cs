using Shell32;
using System.IO;

namespace FileExplorer.Explorer
{
    public static class ExplorerHelpers
    {
        /// <summary>
        /// Checks if the path is a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFile(this string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        /// <summary>
        /// Checks if a path is a directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectory(this string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Checks if a path is a drive
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDrive(this string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Gets the name of a file within a path
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string GetFileName(this string fullpath)
        {
            return Path.GetFileName(fullpath);
        }

        /// <summary>
        /// Returns the directory path of the directory a file is located in
        /// (e.g, C:\f1\fold2\f3\hello.txt, returns C:\f1\fold2\f3)
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string GetParentDirectory(this string fullpath)
        {
            return Path.GetFileName(fullpath);
        }

        /// <summary>
        /// Checks if the path is a shortcut to a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckPathIsShortcutFile(string path)
        {
            return File.Exists(GetShortcutTargetFolder(path));
        }

        /// <summary>
        /// Gets the root path of a shortcut
        /// </summary>
        /// <param name="shortcutFilename"></param>
        /// <returns></returns>
        public static string GetShortcutTargetFolder(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        }
    }
}
