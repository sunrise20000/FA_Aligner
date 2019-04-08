﻿using JPT_TosaTest.ViewModel;
using JPT_TosaTest.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPT_TosaTest.UserCtrl
{
    /// <summary>
    /// UC_HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class UC_HomeView : UserControl
    {
        private bool bFirstLoaded = false;
        public UC_HomeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDelay(2000);
            bFirstLoaded = true;
        }
        private void SetAttachCamWindow( bool bAttach = true)
        {
            if (bAttach)
            {
                HalconVision.Instance.AttachCamWIndow(0, "Cam1", Cam1.HalconWindow);
                HalconVision.Instance.AttachCamWIndow(0, "Cam2", Cam2.HalconWindow);
                HalconVision.Instance.AttachCamWIndow(0, "Cam3", Cam2.HalconWindow);

            }
            else
            {
                HalconVision.Instance.DetachCamWindow(0, "Cam1");
                HalconVision.Instance.DetachCamWindow(0, "Cam2");
                HalconVision.Instance.DetachCamWindow(0, "Cam3");
            }
        }
        private async void LoadDelay(int ms)
        {
            await Task.Run(() => {
                if (bFirstLoaded)
                {
                    Task.Delay(ms).Wait();
                    bFirstLoaded = false;
                }
                System.Windows.Application.Current.Dispatcher.Invoke(() => SetAttachCamWindow(true));
            });
        }


        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetAttachCamWindow(Convert.ToBoolean(e.NewValue));
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).MoveToPtCommand.Execute(DatagridPos.CurrentCell);
        }

        private void UpdatePtMenu_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).UpdatePtCommand.Execute(DatagridPos.CurrentCell);
        }
    }
}
