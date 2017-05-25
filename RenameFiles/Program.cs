using System;
using System.IO;

namespace RenameFiles
{
  class Program
  {
    static string _path = string.Empty, 
      _prefix = string.Empty,
      _postfix = string.Empty;

    static void Main(string[] args)
    {
      if (args.Length == 0 || args.Length > 2)
      {
        Console.WriteLine("RenameFiles <path> <prefix|postfix>");
        Console.WriteLine("<path> is optional parameter.");
        Console.WriteLine("Example:");
        Console.WriteLine("d:/temp");
        Console.WriteLine();
        Console.WriteLine("<prefix|postfix> is mandatory parameter.");
        Console.WriteLine("Examples:");
        Console.WriteLine("prefix|postfix");
        Console.WriteLine("prefix - no postfix");
        Console.WriteLine("|postfix - no prefix");
        return;
      }

      SetupParams(args);
      RenameFiles();
    }

    /// <summary>
    /// Renames all files, if directory exists.
    /// </summary>
    static void RenameFiles()
    {
      DirectoryInfo dirInfo = new DirectoryInfo(_path);
      if (!dirInfo.Exists)
      {
        Console.WriteLine("Directory '" + _path + "' not found.");
        return;
      }

      int fileCount = 0;
      foreach (FileInfo file in dirInfo.GetFiles())
      {
        fileCount++;
        file.MoveTo(_path + "/" + _prefix + file.Name.Replace(".", _postfix + "."));
      }

      Console.WriteLine(string.Format("{0} files was renamed in {1} folder", fileCount, _path));
    }

    /// <summary>
    /// Setup private parameters.
    /// </summary>
    /// <param name="args"></param>
    static void SetupParams(string[] args)
    {
      _path = Directory.GetCurrentDirectory();

      if (args.Length == 1)
      {
        _postfix = args[0];
      }

      if (args.Length == 2)
      {
        _path = args[0];
        string[] prepostfix = args[1].Split('|');
        if (prepostfix.Length == 1)
        {
          _prefix = prepostfix[0];
        }
        else
        {
          _prefix = prepostfix[0];
          _postfix = prepostfix[1];
        }
      }
    }
  }
}
