using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchResearch
{
    public class PlainTextSearcher
    {
        private readonly string _pathToSearch;
        public PlainTextSearcher( string path )
        {
            _pathToSearch = path;
        }

        public TimeSpan Search( string query )
        {
            Stopwatch stopwatch = new Stopwatch();
            StringBuilder sb = new StringBuilder();
            stopwatch.Start();
            DirectoryInfo directory = new DirectoryInfo(_pathToSearch);
            int matchCount=0;

            foreach( var file in Directory.EnumerateFiles(_pathToSearch, "csw.*", SearchOption.AllDirectories ))
            {
                //Console.WriteLine("{0}",file);
                int lineIndex = 1;
                foreach( var line in File.ReadLines( file ))
                {
                    if (line.Contains(query))
                    {
                        sb.AppendFormat("{0}:{1}:{2}", lineIndex, file, line);
                        matchCount++;
                    }
                    lineIndex++;
                }
            }
            stopwatch.Stop();
            //Console.WriteLine("Result:");
            //Console.WriteLine(sb.ToString());
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine("Search Found: {0}", matchCount);
            return elapsedTime;
        }

        public TimeSpan SearchParallel( string query )
        {
            Stopwatch stopwatch = new Stopwatch();
            StringBuilder sb = new StringBuilder();
            stopwatch.Start();
            DirectoryInfo directory = new DirectoryInfo(_pathToSearch);
            var files = Directory.EnumerateFiles(_pathToSearch, "csw.*", SearchOption.AllDirectories );
            int matchCount=0;
            Parallel.ForEach( files, (currentFile) => 
            {
                //Console.WriteLine("{0}",currentFile);
                int lineIndex = 1;
                foreach( var line in File.ReadLines( currentFile ))
                {
                    if (line.Contains(query))
                    {
                        sb.AppendFormat("{0}:{1}:{2}", lineIndex, currentFile, line);
                        matchCount++;
                    }
                        
                    lineIndex++;
                }
            });
            stopwatch.Stop();
            //Console.WriteLine("Result:");
            //Console.WriteLine(sb.ToString());
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine("Search Parallel Found: {0}", matchCount);
            return elapsedTime;
        }

        public TimeSpan SearchParallelWithFastDirectoryEnumerator( string query )
        {
            Stopwatch stopwatch = new Stopwatch();
            StringBuilder sb = new StringBuilder();
            stopwatch.Start();
            DirectoryInfo directory = new DirectoryInfo(_pathToSearch);
            var files = FastDirectoryEnumerator.EnumerateFiles(_pathToSearch, "csw.*", SearchOption.AllDirectories );
            int matchCount=0;
            Parallel.ForEach( files, (currentFileData) => 
            {
                string currentFile = currentFileData.Path;
                //Console.WriteLine("{0}",currentFile);
                int lineIndex = 1;
                foreach( var line in File.ReadLines( currentFile ))
                {
                    if (line.Contains(query))
                    {
                        sb.AppendFormat("{0}:{1}:{2}", lineIndex, currentFile, line);
                        matchCount++;
                    }
                        
                    lineIndex++;
                }
            });
            stopwatch.Stop();
            //Console.WriteLine("Result:");
            //Console.WriteLine(sb.ToString());
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine("Search Parallel With FastDirectoryEnumrator Found: {0}", matchCount);
            return elapsedTime;
        }

        private IEnumerable<string> GetAllFiles(string rootDirectory,
                                                string filePattern)
        {
            return Directory.EnumerateFiles(rootDirectory, filePattern, SearchOption.AllDirectories);
            //foreach (var directory in Directory.GetDirectories(
            //                                        rootDirectory,
            //                                        filePattern,
            //                                        SearchOption.AllDirectories))
            //{
            //    foreach (var file in Directory.GetFiles(directory))
            //    {
            //        yield return file;
            //    }
            //}
        }
    }
}
