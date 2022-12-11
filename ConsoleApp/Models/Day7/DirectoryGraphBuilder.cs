using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day7
{
    public class DirectoryGraphBuilder
    {
        private const char TerminalReadySymbol = '$';
        private const string ChangeDirectoryCommand = "cd";
        private const string ListContentsCommand = "ls";
        private const string RootSymbol = "/";
        private const string MoveUpSymbol = "..";
        private const string DirectorySymbol = "dir";

        private readonly string[] _terminalOutput;

        private Directory _currentDirectory;
        private readonly Directory _rootDirectory;

        public DirectoryGraphBuilder(string[] terminalOutput)
        {
            _terminalOutput = terminalOutput;
            
            _rootDirectory = new Directory(null, "/");

            _currentDirectory = _rootDirectory;
        }

        public Directory Build()
        {
            for (int lineIndex = 0; lineIndex < _terminalOutput.Length; lineIndex++)
            {
                if (_terminalOutput[lineIndex][0] != TerminalReadySymbol) 
                    continue;

                string[] lineParts = _terminalOutput[lineIndex].Substring(2).Split(' ');
                string command = lineParts[0];

                if (command == ChangeDirectoryCommand)
                {
                    ChangeDirectory(lineParts[1]);
                }
                else if (command == ListContentsCommand)
                {
                    ListContents(lineIndex + 1);
                }
            }

            return _rootDirectory;
        }

        private void ChangeDirectory(string arg)
        {
            if (arg == RootSymbol)
            {
                _currentDirectory = _rootDirectory;
            }
            else if (arg == MoveUpSymbol)
            {
                _currentDirectory = _currentDirectory.Parent;
            }
            else
            {
                _currentDirectory = _currentDirectory.Subdirectories.First(dir => dir.Name == arg);
            }
        }

        private void ListContents(int startLineIndex)
        {
            for (int lineIndex = startLineIndex; lineIndex < _terminalOutput.Length; lineIndex++)
            {
                if (_terminalOutput[lineIndex][0] == TerminalReadySymbol)
                {
                    // We've reached the next command. No more contents to process.
                    break;
                }                    

                string[] lineParts = _terminalOutput[lineIndex].Split(' ');

                if (lineParts[0] == DirectorySymbol)
                {
                    _currentDirectory.Subdirectories.Add(new Directory(_currentDirectory, lineParts[1]));
                }
                else
                {
                    _currentDirectory.Files.Add(new File(lineParts[1], Convert.ToInt32(lineParts[0])));
                }
            }
        }
    }
}
