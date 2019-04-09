using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JPT_TosaTest.Model
{
    public class ProductStateModel :ViewModelBase
    {
        private bool _isChecked = true;

        public string ProductName
        {
            get; set;

        }
        public bool IsChecked
        {
            get { return _isChecked; }
            set {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int ProductIndex { get; set; }

        public RelayCommand CommandSetCheckedProduct
        {
            get
            {
                return new RelayCommand (() =>
                {
                    IsChecked = !IsChecked;
                });
            }
        }
    }
}
