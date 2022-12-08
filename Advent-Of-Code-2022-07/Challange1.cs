using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day07
{
    /// <summary>1
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static ulong DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            Dictionary<string, FolderItem> folderNodes = new();

            string currentPath = "/";

            folderNodes.Add(currentPath, new() { ItemType = FolderItem.ItemTypes.Directory });

            foreach (string line in inputData)
            {
                string[] command = line.Split(' ');

                if (command[0] == "$")
                {
                    if (command[1] == "cd")
                    {
                        switch (command[2])
                        {
                            case "/": currentPath = "/"; break;
                            case "..":
                                currentPath = GetParentPath(currentPath);
                                break;
                            default:
                                if (currentPath.EndsWith('/')) currentPath += command[2];
                                else currentPath += "/" + command[2];
                                break;
                        }
                    }
                }
                else
                {
                    if (command[0] == "dir")
                    {
                        string newPath = currentPath;
                        if (!newPath.EndsWith('/'))
                            newPath += '/';
                        newPath += command[1];

                        FolderItem item = new()
                        {
                            ItemType = FolderItem.ItemTypes.Directory
                        };
                        folderNodes[currentPath].SubItems.Add(item);

                        folderNodes.Add(newPath, item);
                    }
                    else
                    {
                        string newPath = currentPath;
                        if (!newPath.EndsWith('/'))
                            newPath += '/';
                        newPath += command[1];

                        FolderItem item = new()
                        {
                            ItemType = FolderItem.ItemTypes.File,
                            Size = int.Parse(command[0])
                        };
                        folderNodes[currentPath].SubItems.Add(item);
                    }
                }
            }

            var orderedFolders = folderNodes.OrderBy(node => node.Value.Size);

            ulong size = 0;

            foreach (KeyValuePair<string, FolderItem> folderInfo in orderedFolders)
            {
                var folder = folderInfo.Value;
                if (folder.Size <= 100000)
                {
                    size += (ulong)folder.Size;
                }
            }

            return size;
        }

        public static string GetParentPath(string path)
        {
            string parentPath = path;
            if (parentPath == "/")
                return "/";
            if (parentPath.EndsWith("/"))
            {
                parentPath = parentPath[..^1];
            }
            parentPath = parentPath.Substring(0,parentPath.LastIndexOf('/'));
            if (parentPath == "") parentPath = "/";
            return parentPath;
        }

        public class FolderItem
        {
            private int _size = -1;
            public enum ItemTypes { Directory, File}
            public ItemTypes ItemType;
            public List<FolderItem> SubItems = new();
            public int Size
            {
                get { return GetSize(); }
                set { SetSize(value); }
            }
            
            private int GetSize()
            { 
                if (ItemType == ItemTypes.File)
                {
                    return _size;
                }
                else
                {
                    int size = 0;
                    for (int i = 0; i < SubItems.Count; i++)
                    {
                        size += SubItems[i].Size;
                    }
                    return size;
                }
            }

            private void SetSize(int value)
            {
                if (ItemType == ItemTypes.File)
                {
                    _size = value;
                }
            }
        }
    }
}