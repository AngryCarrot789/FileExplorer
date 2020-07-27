using FileExplorer.Explorer;
using FileExplorer.Files;
using NamespaceHere;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileExplorer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<FilesControl> FileItems { get; set; }

        public MainViewModel()
        {
            FileItems = new ObservableCollection<FilesControl>();
        }

        #region Navigation

        public void TryNavigateToPath(string path)
        {
            // is a drive
            if (path == string.Empty)
            {
                ClearFiles();

                foreach(FileModel drive in Fetcher.GetDrives())
                {
                    FilesControl fc = CreateFileControl(drive);
                    AddFile(fc);
                }
            }

            else if (path.IsFile())
            {
                // Open the file
                MessageBox.Show($"Opening {path}");
            }

            else if (path.IsDirectory())
            {
                ClearFiles();

                foreach(FileModel dir in Fetcher.GetDirectories(path))
                {
                    FilesControl fc = CreateFileControl(dir);
                    AddFile(fc);
                }

                foreach (FileModel file in Fetcher.GetFiles(path))
                {
                    FilesControl fc = CreateFileControl(file);
                    AddFile(fc);
                }
            }

            else
            {
                // something bad has happened...
            }
        }

        public void NavigateFromModel(FileModel file)
        {
            TryNavigateToPath(file.Path);
        }

        #endregion

        public void AddFile(FilesControl file)
        {
            FileItems.Add(file);
        }

        public void RemoveFile(FilesControl file)
        {
            FileItems.Remove(file);
        }

        public void ClearFiles()
        {
            FileItems.Clear();
        }

        public FilesControl CreateFileControl(FileModel fModel)
        {
            FilesControl fc = new FilesControl(fModel);
            SetupFileControlCallbacks(fc);
            return fc;
        }

        public void SetupFileControlCallbacks(FilesControl fc)
        {
            fc.NavigateToPathCallback = NavigateFromModel;
        }
    }
}
