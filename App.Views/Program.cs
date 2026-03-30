using App.Models;

namespace App.Views
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            // To show Notes:
            var noteForm = new DynamicListForm<Note>();
            noteForm.Show();

            // To show Categories:
            var catForm = new DynamicListForm<Category>();
            catForm.Show();
        }
    }
}