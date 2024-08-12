using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

namespace BetP11T7
{
    class WordLocation
    {
        public String word;
        public int location;
    }

    class LLIntNode
    {
        public int cargo;
        public LLIntNode next;
    }

    class WordLocationList
    {
        public String word;
        public LLIntNode head;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            ArrayList wordList = new ArrayList();
            ArrayList idxList = new ArrayList();

            // Task 1
            ReadAllFiles(wordList);
            WriteLine("Successfully Read Files!");

            // Task 2
            Sort(wordList);
            WriteLine("Sorted Word list!");

            // Task 4
            BuildIdxList(wordList, idxList);
            WriteLine("Built Index List!");

            // Task 7
            SearchEngine(idxList);
        }

        // Task 1: Read all files and populate wordList
        public void ReadAllFiles(ArrayList listToPopulate)
        {
            for (int i = 1; i <= 101; i++)
            {
                Reader("Verse" + i + ".txt", listToPopulate, i);                
            }
        }

        public void Reader(string filename, ArrayList listToPopulate, int verseLocation)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line.ToLower(), "\\.|,|!|;|\t|-|\\?|:|\"", "").Trim(); //Remove unnecessary characters and trailing space
                string[] data = line.Split(' '); //get each data item in a line in its own place in an array

                //store data in new WordLocation object
                for (int i = 0; i < data.Length; i++)
                {
                    WordLocation location = new WordLocation();
                    location.location = verseLocation;
                    location.word = data[i];

                    //add word to the list 
                    listToPopulate.Add(location);
                }
            }
        }

        // Task 2: Sort the word list using Merge Sort
        public void Sort(ArrayList wordList)
        {
            MergeSort(wordList, 0, wordList.Count - 1);
        }

        public void MergeSort(ArrayList list, int start, int end)
        {
            if (end > start)
            {
                int mid = (start + end) / 2;
                MergeSort(list, start, mid);
                MergeSort(list, mid + 1, end);
                Merge(list, start, mid + 1, end);
            }
        }

        public void Merge(ArrayList list, int start, int mid, int end)
        {
            WordLocation[] temp = new WordLocation[end - start + 1];
            int posA = start;
            int posB = mid;
            int posC = 0;

            while (posA <= mid - 1 && posB <= end)
            {
                string wordA = ((WordLocation)list[posA]).word;
                string wordB = ((WordLocation)list[posB]).word;

                if (wordA.CompareTo(wordB) <= 0)
                {
                    temp[posC] = (WordLocation)list[posA];
                    posA++;
                }
                else
                {
                    temp[posC] = (WordLocation)list[posB];
                    posB++;
                }
                posC++;
            }
            while (posA <= mid - 1)
            {
                temp[posC] = (WordLocation)list[posA];
                posC++;
                posA++;
            }
            while (posB <= end)
            {
                temp[posC] = (WordLocation)list[posB];
                posC++;
                posB++;
            }

            for (int i = 0; i < temp.Length; i++)
            {
                list[start + i] = temp[i];
            }
        }

        // Task 3: Recursive Insert into a sorted linked list
        public LLIntNode InsertSorted(LLIntNode curhead, int value)
        {
            if (curhead == null)
            {
                LLIntNode newNode = new LLIntNode();
                newNode.cargo = value;
                return newNode;
            }

            if (curhead.cargo < value)
            {
                curhead.next = InsertSorted(curhead.next, value);
                return curhead;
            }
            else if (curhead.cargo > value)
            {
                LLIntNode newNode = new LLIntNode();
                newNode.cargo = value;
                newNode.next = curhead;
                return newNode;
            }
            return curhead;
        }

        // Task 4: Build the index list
        public void BuildIdxList(ArrayList wordList, ArrayList idxList)
        {
            foreach (WordLocation location in wordList)
            {
                bool found = false;
                foreach (WordLocationList wordLocList in idxList)
                {
                    if (wordLocList.word == location.word)
                    {
                        wordLocList.head = InsertSorted(wordLocList.head, location.location);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    WordLocationList newWordLocList = new WordLocationList();
                    newWordLocList.word = location.word;
                    newWordLocList.head = InsertSorted(newWordLocList.head, location.location);
                    idxList.Add(newWordLocList);
                }
            }
        }

        // Task 5: Binary Search for the index list
        public WordLocationList BinarySearchRecursive(ArrayList inputArray, String key, int min, int max)
        {
            if (min > max)
            {
                return null;
            }

            int mid = (min + max) / 2;
            WordLocationList midVal = (WordLocationList)inputArray[mid];

            if (midVal.word == key)
            {
                return midVal;
            }
            else if (midVal.word.CompareTo(key) > 0)
            {
                return BinarySearchRecursive(inputArray, key, min, mid - 1);
            }
            else
            {
                return BinarySearchRecursive(inputArray, key, mid + 1, max);
            }
        }

        // Task 6:Recursive Intersection merge of two linked lists
        public LLIntNode IntersectionMerge(LLIntNode A, LLIntNode B)
        {
            if (A == null || B == null) return null;

            if (A.cargo < B.cargo)
            {
                return IntersectionMerge(A.next, B);
            }
            else if (A.cargo > B.cargo)
            {
                return IntersectionMerge(A, B.next);
            }
            else
            {
                LLIntNode result = new LLIntNode();
                result.cargo = A.cargo;
                result.next = IntersectionMerge(A.next, B.next);
                return result;
            }
        }

        // Task 7: Search engine functionality
        public void SearchEngine(ArrayList idxList)
        {
            while (true)
            {
                Console.WriteLine("Type a search term. Press \"Enter\" without typing a term to stop.");
                string term = Console.ReadLine();
                if (string.IsNullOrEmpty(term)) break;

                ArrayList searchTerms = new ArrayList();
                searchTerms.Add(term);

                while (true)
                {
                    Console.WriteLine("Type a search term. Press \"Enter\" without typing a term to stop.");
                    term = Console.ReadLine();
                    if (string.IsNullOrEmpty(term)) break;
                    searchTerms.Add(term);
                }

                LLIntNode finalResult = null;

                foreach (string searchTerm in searchTerms)
                {
                    WordLocationList result = BinarySearchRecursive(idxList, searchTerm, 0, idxList.Count - 1);
                    if (result == null)
                    {
                        finalResult = null;
                        break;
                    }

                    if (finalResult == null)
                    {
                        finalResult = result.head;
                    }
                    else
                    {
                        finalResult = IntersectionMerge(finalResult, result.head);
                    }
                }

                if (finalResult != null)
                {
                    Console.WriteLine("Search Results:");
                    while (finalResult != null)
                    {
                        DisplayVerse(finalResult.cargo);
                        finalResult = finalResult.next;
                    }
                }
                else
                {
                    Console.WriteLine("No results found.");
                }
            }
        }

        // Display the verse from a file
        public void DisplayVerse(int verseNumber)
        {
            string filename = "Verse" + verseNumber + ".txt";
            if (File.Exists(filename))
            {
                string content = File.ReadAllText(filename);
                WriteLine($"Verse {verseNumber}:\n{content}\n");
            }
            else
            {
                WriteLine($"Verse {verseNumber} not found.");
            }
        }
    }
}
