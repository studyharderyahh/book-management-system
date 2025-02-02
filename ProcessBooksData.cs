﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManagementSystem
{
    public class ProcessBooksData
    {
        // Dictionary to store stacks of books based on their category
        private readonly Dictionary<string, Stack<Book>> categorizedBooks;
        private static int filterReleasedYear = 1990;
        private static string[] allowedBookCategory = { "Computer Science", "Networking", "Mathematics", "Software Development" };

        // Constructor
        public ProcessBooksData()
        {
            categorizedBooks = new Dictionary<string, Stack<Book>>()
            {
                //Stack for each kind of book we're keeping
                { "Computer Science", new Stack<Book>() },
                { "Networking", new Stack<Book>() },
                { "Mathematics", new Stack<Book>() },
                { "Software Development", new Stack<Book>() }
            };
        }

        // Separate method for processing books added manually
        public void AddManuallyAddedBooks(Book book)
        {
            if (book.ReleasedYear >= filterReleasedYear && allowedBookCategory.Contains(book.Category))
            {
                categorizedBooks[book.Category].Push(book);
            }
            
        }

        // Separate method for processing books loaded from a file
        public void AddBooksFromLoadedFile(BookDataStructure bookDataStructure)
        {
            foreach (var book in bookDataStructure.books)
            {
                categorizedBooks[book.Category].Push(book);
            }
        }

        // Method to display books of a specific category in a DataGridView
        public void DisplayBooksByCategory(string category, DataGridView dataGridView)
        {
            if (categorizedBooks.ContainsKey(category))
            {
                var books = new List<Book>(categorizedBooks[category].ToArray());
                dataGridView.DataSource = books;
            }
            else
            {
                throw new ArgumentException($"Category '{category}' does not exist.");
            }
        }

        // Method to display all categorized books in a DataGridView
        public void DisplayAllCategories(DataGridView dataGridView)
        {
            var allBooks = categorizedBooks.Values.SelectMany(stack => stack.ToArray()).ToList();
            dataGridView.DataSource = allBooks;

           /* // For Debugging purposes
            // Construct a message string to display all books
            StringBuilder message = new StringBuilder();
            foreach (var book in allBooks)
            {
                message.AppendLine(book.ToString());
            }

            // Display the message in a MessageBox
            MessageBox.Show(message.ToString());
           */

        }

        // Method to delete the top book from a specific category stack
        public void DeleteTopBook(string selectedCategory)
        {
            if (categorizedBooks.ContainsKey(selectedCategory))
            {
                if (categorizedBooks[selectedCategory].Count > 0)
                {
                    categorizedBooks[selectedCategory].Pop();
                }
                else
                {
                    throw new ArgumentException($"No books available in the '{selectedCategory}' category to delete.");
                }
            }
            else
            {
                throw new ArgumentException($"Category '{selectedCategory}' does not exist.");
            }
        }
    }
}
