This program is a practical assignment focused on building a search engine for the poem "Rubaiyat" by Omar Khayyam. It involves several steps to achieve this:

Data Collection:

Read words from 101 separate text files (one for each verse).
Store each word along with its verse number in an ArrayList of WordLocation objects.
Sorting:

Sort the ArrayList of WordLocation objects alphabetically by word using a Merge Sort algorithm.
Index Creation:

Create an index that stores unique words along with their verse locations using a linked list.
The linked list contains unique verse numbers and is sorted.
Search Processing:

Implement a binary search to find words in the index.
Perform an intersection merge on the linked lists to find common verses containing all search terms.
User Interaction:

Continuously prompt the user for search terms.
Display verses that contain all provided search terms by loading and showing the relevant files.
Tasks involve implementing different steps of the process, including reading files, sorting, inserting into linked lists, searching, and merging lists to enable efficient search functionality.
