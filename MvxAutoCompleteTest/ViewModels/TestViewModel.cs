using Cirrious.MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Cirrious.CrossCore.Platform;

namespace MvxAutoCompleteTest.ViewModels
{
    public class TestViewModel
      : MvxViewModel
    {
        public void Init()
        {
            ObservableCollection<TestItem> t = new ObservableCollection<TestItem>();
            TestItem i = new TestItem() { Articles = "Bed, Double" };
            t.Add(i);
            i = new TestItem() { Articles = "Sofa, Sleeper" };
            t.Add(i);
            i = new TestItem() { Articles = "Bedroom" };
            t.Add(i);
            i = new TestItem() { Articles = "" };
            t.Add(i);
            i = new TestItem() { Articles = null };
            t.Add(i);

            Items = t;
        }

        private ObservableCollection<TestItem> _Items;
        public ObservableCollection<TestItem> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                RaisePropertyChanged(() => Items);
            }
        }

    }

    public class TestItem : INotifyPropertyChanged
    {

        static TestItem()
        {
            LoadAutoCompleteList();
        }

        private static ObservableCollection<string> AutoCompleteList;

        private static void LoadAutoCompleteList()
        {
            AutoCompleteList = new ObservableCollection<string>();
            AutoCompleteList.Add("Bed, Double");
            AutoCompleteList.Add("Bed, Single");
            AutoCompleteList.Add("Bed, Queen");
            AutoCompleteList.Add("Bed, King");
            AutoCompleteList.Add("Sofa, Loveseat");
            AutoCompleteList.Add("Sofa, Couch");
            AutoCompleteList.Add("Sofa, Sleeper");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Articles;
        public string Articles
        {
            get
            {
                return _Articles;
            }
            set
            {
                _Articles = value;
                this.RaisePropertyChanged("Articles");
            }
        }

        private object _SelectedObject;
        public object SelectedObject
        {
            get { return _SelectedObject; }
            private set
            {
                _SelectedObject = value;
                RaisePropertyChanged("SelectedObject");
            }
        }

        private string _currentTextHint;
        public string CurrentTextHint
        {
            get
            { return _currentTextHint; }
            set
            {
                MvxTrace.Trace("Partial Text Value Sent {0}", value);
                if (value == "")
                {
                    _currentTextHint = null;
                    SetSuggestionsEmpty();
                    return;
                }
                else
                {
                    _currentTextHint = value;
                }

                if (_currentTextHint.Trim().Length < 2)
                {
                    SetSuggestionsEmpty();
                    return;
                }

                var list = AutoCompleteList.Where(i => (i ?? "").ToUpper().Contains(_currentTextHint.ToUpper()));
                if (list.Count() > 0)
                {
                    AutoCompleteSuggestions = list.ToList();
                }
                else
                {
                    SetSuggestionsEmpty();
                }
            }
        }

        private void SetSuggestionsEmpty()
        {
            AutoCompleteSuggestions = new List<string>();
        }

        private List<string> _autoCompleteSuggestions = new List<string>();
        public List<string> AutoCompleteSuggestions
        {
            get
            {
                if (_autoCompleteSuggestions == null)
                {
                    _autoCompleteSuggestions = new List<string>();
                }
                return _autoCompleteSuggestions;
            }
            set { _autoCompleteSuggestions = value; RaisePropertyChanged("AutoCompleteSuggestions"); }
        }
    }
}