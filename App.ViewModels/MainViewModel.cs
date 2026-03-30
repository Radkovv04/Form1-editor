using App.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace App.ViewModels
{
    public class ActionLogItem
    {
        public string ActionName { get; set; } = "";
        public string TextSnapshot { get; set; } = "";
        public override string ToString() => ActionName;
    }

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string _currentText = "";
        [ObservableProperty] private string _currentTitle = "";
        [ObservableProperty] private int _currentNoteId = 0;
        [ObservableProperty] private ObservableCollection<Category> _availableCategories = new();
        [ObservableProperty] private Category? _selectedCategory;
        [ObservableProperty] private BindingList<ActionLogItem> _sessionLogs = new();

        private readonly Stack<string> _undoStack = new();
        private readonly Stack<string> _redoStack = new();
        public MainViewModel() => LoadCategories();

        // --- UNDO / REDO ENGINE ---
        public void RegisterChange(string textToStore)
        {
            _undoStack.Push(textToStore);
            _redoStack.Clear();
            LogAction("Typing...");
        }
        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;
        public string Undo(string currentTextOnScreen)
        {
            _redoStack.Push(currentTextOnScreen);
            return _undoStack.Pop();
        }
        public string Redo(string currentTextOnScreen)
        {
            _undoStack.Push(currentTextOnScreen);
            return _redoStack.Pop();
        }
        public void LogAction(string actionText)
        {
            if (actionText == "Typing..." && SessionLogs.Count > 0 && SessionLogs[0].ActionName.Contains("Typing..."))
            {
                SessionLogs[0].TextSnapshot = CurrentText;
                return;
            }

            SessionLogs.Insert(0, new ActionLogItem
            {
                ActionName = $"[{DateTime.Now:HH:mm:ss}] {actionText}",
                TextSnapshot = CurrentText
            });
        }
        // --- Name Checker ---
        public bool DoesTitleExist(string title)
        {
            using var db = new AppDbContext();
            return db.Notes.Any(n => n.Title.ToLower() == title.ToLower());
        }
        // --- DATABASE LOGIC ---
        public void SaveNote()
        {
            using (var db = new AppDbContext())
            {
                var note = new Note
                {
                    Title = CurrentTitle,
                    Content = CurrentText,
                    LastModified = DateTime.Now,
                    CategoryId = SelectedCategory?.Id
                };

                db.Notes.Add(note);
                db.SaveChanges();
                CurrentNoteId = note.Id;
                db.NoteRevisions.Add(new NoteRevision
                {
                    NoteId = note.Id,
                    Content = CurrentText,
                    SavedAt = DateTime.Now
                });
                db.SaveChanges();
            }
        }
        public bool DeleteNoteByName(string title)
        {
            using (var db = new AppDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.Title.ToLower() == title.ToLower());
                if (note != null)
                {
                    db.Notes.Remove(note);
                    db.SaveChanges();
                    LogAction($"DELETED: {note.Title}");
                    return true;
                }
            }
            return false;
        }
        public void LoadCategories()
        {
            using var db = new AppDbContext();
            if (!db.Categories.Any())
            {
                db.Categories.Add(new Category { Name = "General", Description = "Default" });
                db.SaveChanges();
            }
            AvailableCategories = new ObservableCollection<Category>(db.Categories.ToList());
            SelectedCategory = AvailableCategories.FirstOrDefault();
        }
    }
}