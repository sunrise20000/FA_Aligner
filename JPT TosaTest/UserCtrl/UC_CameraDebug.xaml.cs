﻿using JPT_TosaTest.Model;
using JPT_TosaTest.Model.ToolData;
using JPT_TosaTest.ViewModel;
using JPT_TosaTest.Vision;
using JPT_TosaTest.Vision.ProcessStep;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPT_TosaTest.UserCtrl
{
    /// <summary>
    /// UC_CameraDebug.xaml 的交互逻辑
    /// </summary>
    public partial class UC_CameraDebug : System.Windows.Controls.UserControl
    {
        private bool bFirstLoaded;
        public const string HalconWindowHandlePropertyName = "HalconWindowHandle";
        public IntPtr HalconWindowHandle
        {
            get
            {
                return (IntPtr)GetValue(HalconWindowHandleProperty);
            }
            set
            {
                SetValue(HalconWindowHandleProperty, value);
            }
        }
        public static readonly DependencyProperty HalconWindowHandleProperty = DependencyProperty.Register(HalconWindowHandlePropertyName, typeof(IntPtr),typeof(UC_CameraDebug));

        public UC_CameraDebug()
        {
            InitializeComponent();
        }
        private void Cb_Cameras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!bFirstLoaded)
                HalconVision.Instance.AttachCamWIndow(Cb_Cameras.SelectedIndex, "CameraDebug", CamDebug.HalconID);
        }

  
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDelay(2000);
            bFirstLoaded = true;
        }
        private void SetAttachCamWindow(bool bAttach = true)
        {
            if (bAttach)
                HalconVision.Instance.AttachCamWIndow(0, "CameraDebug", CamDebug.HalconWindow);
            else
                HalconVision.Instance.DetachCamWindow(0, "CameraViewCam");
        }
        private async void LoadDelay(int ms)
        {
            await Task.Run(() => {
                if (bFirstLoaded)
                {
                    Task.Delay(ms).Wait();
                    bFirstLoaded = false;
                }
                System.Windows.Application.Current.Dispatcher.Invoke(() => { SetAttachCamWindow(true); HalconWindowHandle = CamDebug.HalconID; });
               
            });
        }
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetAttachCamWindow(Convert.ToBoolean(e.NewValue));
        }
    }
}
