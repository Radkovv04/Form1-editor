using App.Models.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using App.Models.Models;

namespace App.ViewModels
{
    public partial class DynamicListViewModel<T> : ObservableObject where T : class
    {
        [ObservableProperty]
        private ObservableCollection<T> _items = new();
        [ObservableProperty]
        private T _selectedItem;
        public Dictionary<string, string> SearchFilters { get; set; } = new();
        public DynamicListViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            LoadAll();
        }
        public IRelayCommand SearchCommand { get; }
        private void LoadAll()
        {
            using var db = new AppDbContext();
            IQueryable<T> query = db.Set<T>();

            // Task 5: Зареждаме свързаните данни
            if (typeof(T) == typeof(Note)) query = query.Include("Category");
            if (typeof(T) == typeof(Category)) query = query.Include("Notes");

            Items = new ObservableCollection<T>(query.ToList());
        }

        private void ExecuteSearch()
        {
            using var db = new AppDbContext();
            // Промяна: Започваме с IQueryable, за да запазим връзките (Includes)
            IQueryable<T> query = db.Set<T>();

            if (typeof(T) == typeof(Note)) query = query.Include("Category");
            if (typeof(T) == typeof(Category)) query = query.Include("Notes");

            PropertyInfo[] properties = typeof(T).GetProperties();
            var list = query.ToList(); // Прехвърляме в паметта за филтрирането

            foreach (var prop in properties)
            {
                if (prop.GetCustomAttribute<SearchableAttribute>() != null &&
                    SearchFilters.ContainsKey(prop.Name) &&
                    !string.IsNullOrWhiteSpace(SearchFilters[prop.Name]))
                {
                    string searchTerm = SearchFilters[prop.Name].ToLower();
                    list = list.Where(item =>
                    {
                        var val = prop.GetValue(item)?.ToString()?.ToLower();
                        return val != null && val.Contains(searchTerm);
                    }).ToList();
                }
            }
            Items = new ObservableCollection<T>(list);
        }

    }
}