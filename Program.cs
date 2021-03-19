using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ryden_Christopher_Lab3
{
    class Program
    {



        static void Main(string[] args)
        {
            #region lists processed by methods
            List<string> unsortedcomics = GetstuffFromInput("inputFile.csv");//method to retireve list of comic books
           
            List<string> bubblesorted = BubbleSortTitles(unsortedcomics);
            
            List<string> mergeSorted = MergeSortTitles(unsortedcomics);
            #endregion

            #region menuElements
            string[] options = new string[] { "1: bubble sort", "2: merge sort", "3: binary search", "4: exit" };
            string prompt = "\tWelcome! Make a selection";
            string exitPrompt = "Press any key to terminate";
            string menuaccent = ":------unsorted----------------------------sorted--------------:";
            string baseBoard = ":--------------------------------------------------------------:";
            string userInput = string.Empty;
            int refinedInput;

            int sx = 1;
            int sy = 3;
            Console.WriteLine(prompt);
            #endregion

            #region menuLoop
            bool exit = false;
            while(!exit)
            { 
                ReadChoice(userInput, options, out refinedInput); 
                switch(refinedInput)
                {
                    case 1://bubble sort
                        {
                            Console.Clear();
                            Console.WriteLine(menuaccent);
                           foreach (string item in unsortedcomics) 
                           {
                                Console.SetCursorPosition(sx, sy++);
                                Console.Write(item);
                               
                           }
                            sy = sy - 84;
                           foreach(string nitem in bubblesorted)
                            {
                                Console.SetCursorPosition(sx + 50, sy ++); 
                                Console.Write(nitem);
                                
                            }
                            sy = sy -84;
                            Console.SetCursorPosition(20, 90);
                            Console.Write("press any key to return to menu");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case 2:// merge sort
                        {
                            Console.Clear();
                            Console.WriteLine(menuaccent);
                            foreach (string item in unsortedcomics)
                            {
                                Console.SetCursorPosition(sx, sy++);
                                Console.Write(item);
                            }
                           sy = sy - 1;
                            foreach (string nItem in mergeSorted)
                            {
                                Console.SetCursorPosition(sx + 50, sy--);
                                Console.Write(nItem);
                            }

                            sy = sy + 1;
                            Console.SetCursorPosition(20, 90);
                            Console.Write("press any key to return to menu");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case 3:// binary search
                        {
                            Console.Clear();

                            Console.WriteLine(baseBoard);
                           

                            foreach(string kItem in bubblesorted)
                            {
                                Console.SetCursorPosition(sx + 70, sy++);
                                Console.Write("Binary Search: " +binarySearch(bubblesorted, kItem)); 
                            }
                            sy = sy - 1;
                            foreach (string nItem in mergeSorted)
                            {
                                Console.SetCursorPosition(sx, sy--);
                                Console.Write(nItem);
                            }
                            sy = sy + 1;
                            foreach (int index in GetIndexesofTitles(bubblesorted))
                            {
                                Console.SetCursorPosition(sx+ 50, sy++);
                                Console.Write("Index: " + index);
                                
                            }
                            sy = sy - 84;
                            Console.SetCursorPosition(20, 90);
                            Console.Write("press any key to return to menu");
                            Console.ReadKey();
                            Console.Clear();


                        }

                        break;
                    case 4:// exit
                        {
                            Console.Clear();
                            exit = true;
                            break;
                        }
                }


            }
            #endregion
            Console.WriteLine(exitPrompt);

            Console.ReadKey();
            
        }

        #region sorting/Menu_Methods
        static public List<string> GetstuffFromInput(string fileName)// method to pull contents from .csv file
        {
            FileInfo path = new FileInfo(fileName);// get file path
            string fullpath = path.FullName;//converts filepath to a string
            string[] comicsUNS = File.ReadAllLines(fullpath);//remnant of earlier attempts to get out the list of strings

            List<string> lComicTitles = new List<string>();// initialization of a list to pull from loop

            string testPrompt = $"|file path for csv|: {fullpath}";
            Console.WriteLine(testPrompt);// just to show if path returned correctly
            
            StreamReader sr = new StreamReader(fullpath);//stream reading black magic

            while (!sr.EndOfStream)//while it's not at the end of stream
            {
                string line = sr.ReadLine();//reads contents of file into a string
                if (!String.IsNullOrWhiteSpace(line))//translates to if the piece of file is empty or a whitespace
                { string[] comicTitles = line.Split(',');//split by delimiters into a temp array
                    List<string> comics = new List<string>(comicTitles);//create new list from array
                    lComicTitles = comics;// copy contents of 'comics' into 'lComicTitles' in method so it can be returned outside loop

                }
            }
            return lComicTitles;


        }
        static public List<string> BubbleSortTitles(List<string> comics)// method to sort list via bubbleSort
        {
            bool swap;
            string pH;//parameters of method bool swap and pH(short for Place holder as a temp string)
            List<string> sortedTitles = new List<string>();
            foreach (string items in comics)
            { sortedTitles.Add(items); }
            do
            {
                swap = false;

                for (int i = 0; i < sortedTitles.Count - 1; i++)// reduced overal .count by one since list index is zero based
                {
                    if (string.Compare(sortedTitles[i],sortedTitles[i + 1]) > 0) //compare index of the next index of current title and the 
                    {//next title in the index
                        pH = sortedTitles[i];
                        sortedTitles[i] = sortedTitles[i + 1];
                        sortedTitles[i + 1] = pH;
                        swap = true;//swap procedure if index is greater than zero, list should be alphabetized from top to bottom
                    }
                    
                }
            } while (swap == true);
         
            return sortedTitles;// returns the list now sorted
         
        }
        static public List<string> MergeSortTitles(List<string> comics)// method to sort list via mergeSort in conjunction with combineLR
        {
            List<string> comicsL = new List<string>();//Lefty comes first since I AM BACKWARDS HANDED DANGIT
            List<string> comicsR = new List<string>();
            if (comics.Count<= 1) 
             return comics;
            int mid = comics.Count / 2;

            for (int i = 0; i < mid; i++)
            {
                comicsL.Add(comics[i]);
            }
            for(int ii = mid; ii < comics.Count; ii++)
            {
                comicsR.Add(comics[ii]);
            }
            comicsL = MergeSortTitles(comicsL);
            comicsR = MergeSortTitles(comicsR);

            return CombineLR(comicsL, comicsR);
        }
        public static List<string> CombineLR(List<string> left, List<string> right) // BEWARE: BOOLEAN SHENANIGANS AHEAD
        {
            List<string> combined = new List<string>();

            while (left.Count > 0 || right.Count > 0)//condition to check if splitted lists are not empty
            { 
                if (left.Count > 0 && right.Count > 0) //IF they are NOT empty procedure as follows
                {
                    if (String.Compare(left.First(),right.First())>0 )// if the .compare reurns something less than a 0 then Left is added
                    {
                        combined.Add(left.First());
                        left.Remove(left.First());
                    }
                    else //otherwise add right
                    {
                        combined.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)//baisically copies for exit conditions
                {
                    combined.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    combined.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return combined;
        }
        static public List<int> GetIndexesofTitles(List<string> comics) //method to return list of indexes to compare w binary search
        {
          
            List<int> indexes= new List<int>();// list of indexes that will return from brinary search
            
            foreach (string item in comics)
            {
                indexes.Add(comics.IndexOf(item));
            }


            return indexes;
        }
        static int binarySearch(List<string> sorted, String x)
        {
            
            int left = 0, right = sorted.Count - 1;
            while (left <= right)
                
            {
                int mid = left + (right - left) / 2;

                int res = x.CompareTo(sorted[mid]);

                  
                if (res == 0)// Check if x is present at mid
                    return mid;

                 
                if (res > 0)// If x greater, ignore left half  
                    left = mid + 1;
                
                // If x is smaller, ignore right half  
                else
                    right = mid - 1;
            }

            return -1 ;
        }//binary search method
        static void ReadChoice(string userInput, string[] options, out int selection) //this in tandem with ReadInt is just menu stuff
        {
           
            foreach (string menuItem in options)
            {
                Console.WriteLine(menuItem);
            }

            selection = ReadInteger(userInput, 1, options.Length);

        }
        static int ReadInteger(string input, int min, int max)
        {
            string userInput = input;
            bool success = false;
            int num;

            do
            {
               
                string Input = Console.ReadLine();

                if (int.TryParse(Input, out num))
                {
                    if (num > max || num < min)
                    {
                        Console.WriteLine("INVALID INPUT: OUT OF MENU RANGE");
                        continue;
                    }
                    else
                    {
                        success = true;
                    }

                }


                else
                {
                    Console.WriteLine("INVALID INPUT: NOT A NUMBER");
                }



            } while (success != true);

            return num;

        }
        #endregion
    }


}
