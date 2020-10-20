using System;
using System.IO;

namespace FileSearchResearch
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            string pathToSearch = @"/Users/jul/Downloads/201119_162904_C-95_Error643_144_Drawer_open";
            PlainTextSearcher searcher = new PlainTextSearcher(pathToSearch);
            var elapsedTime01 = searcher.Search("PostPlate");
            var elapsedTime02 = searcher.SearchParallel("PostPlate");
            var elapsedTime03 = searcher.SearchParallelWithFastDirectoryEnumerator("PostPlate");
            Console.WriteLine("Search Time : {0}", elapsedTime01);
            Console.WriteLine("Search Parallel Time : {0}", elapsedTime02);
            Console.WriteLine("Search Parallel with FastDirectoryEnumrator Time : {0}", elapsedTime03);
            Console.ReadLine();
        }
    }
}
