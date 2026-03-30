using App.Models;
using App.Models.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

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
            var allData = db.Set<T>().ToList();
            Items = new ObservableCollection<T>(allData);
        }

        private void ExecuteSearch()
        {
            using var db = new AppDbContext();
            var query = db.Set<T>().AsEnumerable();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (prop.GetCustomAttribute<SearchableAttribute>() != null &&
                    SearchFilters.ContainsKey(prop.Name) &&
                    !string.IsNullOrWhiteSpace(SearchFilters[prop.Name]))
                {
                    string searchTerm = SearchFilters[prop.Name].ToLower();
                    query = query.Where(item =>
                    {
                        var val = prop.GetValue(item)?.ToString()?.ToLower();
                        return val != null && val.Contains(searchTerm);
                    });
                }
            }
            Items = new ObservableCollection<T>(query.ToList());
        }
    }
}