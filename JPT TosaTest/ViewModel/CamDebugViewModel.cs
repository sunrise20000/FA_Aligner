using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HalconDotNet;
using JPT_TosaTest.Classes;
using JPT_TosaTest.Config;
using JPT_TosaTest.Model;
using JPT_TosaTest.Models;
using JPT_TosaTest.UserCtrl;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using VisionLib;
using static VisionLib.VisionDefinitions;

namespace JPT_TosaTest.ViewModel
{


    public class CamDebugViewModel : ViewModelBase
    {
        private ObservableCollection<CameraItem> _cameraCollection = new ObservableCollection<CameraItem>();
        private CancellationTokenSource cts = null;
        private bool _saveImageType = true;
        private Task GrabTask = null;
        public EnumCamSnapState _camSnapState;
        private string PATH_DEFAULT_IMAGEPATH = @"C:\";
        private readonly string PATH_CALIB = @"VisionData\Calib\";
        private readonly string PATH_MODEL = @"VisionData\Model\";
        private HalconVision Vision = HalconVision.Instance;
        public enum EnumRoiModelType : int
        {
            ROI,
            MODEL
        }
        public CamDebugViewModel()
        {
            #region CameraInit
            CameraCollection = new ObservableCollection<CameraItem>();
            int i = 0;
            List<string> CamListSetting = new List<string>();
            foreach (var it in ConfigMgr.Instance.HardwareCfgMgr.Cameras)
                CamListSetting.Add(it.NameForVision);

            var CamListFind = HalconVision.Instance.FindCamera(EnumCamType.GigEVision2, CamListSetting, out List<string> ErrorList);
            foreach (var it in CamListFind)
            {
                bool bOpen = HalconVision.Instance.OpenCam(i++);
                CameraCollection.Add(new CameraItem() { CameraName = it.NameForVision, StrCameraState = bOpen ? EnumCamState.Connected : EnumCamState.DisConnected });
            }

            foreach (var err in ErrorList)
                Messenger.Default.Send<string>(err, "Error");


            //初始化标定窗口的DataTable
            string[] Headers = new string[] { "Name", "CamX", "CamY", "X", "Y" };
            foreach (var header in Headers)
                CalibrationDt.Columns.Add(header);

            #endregion          
        }


        ~CamDebugViewModel()
        {
            HalconVision.Instance.CloseAllCamera();
            Messenger.Default.Unregister<string>("UpdateModelFiles");

        }

        #region Private method
        private void ThreadFunc(int nCamID)
        {
            CamSnapState = EnumCamSnapState.BUSY;
            var CamModel = Vision.CheckCamIDAvilible(nCamID);
            while (!cts.Token.IsCancellationRequested)
            {

                Vision.GrabImage(CamModel, true, true);
                Vision.DisplayImage(CamModel,true);
                Thread.Sleep(30);
            }
            CamSnapState = EnumCamSnapState.IDLE;
        }
        private void UpdateCalibratePoint()
        {
            if (CalibrationDt.Rows.Count == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    var dr = CalibrationDt.NewRow();
                    dr[0] = $"点{i}";
                    dr[1] = dr[2] = dr[3] = dr[4] = 0;
                    CalibrationDt.Rows.Add(dr);
                }      
            }
            else if(CalibrationDt.Rows.Count==9)
            {

            }
                

        }

        #endregion


        #region Properties
      

     

      
        /// <summary>
        /// 相机的状态
        /// </summary>
        public EnumCamSnapState CamSnapState
        {
            set
            {
                if (_camSnapState != value)
                {
                    _camSnapState = value;
                    RaisePropertyChanged();
                }
            }
            get { return _camSnapState; }
        }

        /// <summary>
        /// 保存图像的类型
        /// </summary>
        public bool SaveImageType
        {
            set
            {
                if (_saveImageType != value)
                {
                    _saveImageType = value;
                    RaisePropertyChanged();
                }
            }
            get { return _saveImageType; }
        }

        /// <summary>
        /// 标定的时候需要用到
        /// </summary>
        public DataTable CalibrationDt { get; set; } = new DataTable();
        public ObservableCollection<CameraItem> CameraCollection
        {
            get;
            set;
        }

        /// <summary>
        /// 当前选择的是哪个相机
        /// </summary>
        public int CurrentSelectedCamera
        {
            get;set;
        }
        #endregion

        #region Command

        public RelayCommand GrabContinusCommand
        {
            get { return new RelayCommand(()=> {
                if (CurrentSelectedCamera >= 0 && (GrabTask == null || GrabTask.IsCanceled || GrabTask.IsCompleted))
                {
                    cts = new CancellationTokenSource();
                    GrabTask = new Task(()=>ThreadFunc(CurrentSelectedCamera));
                    GrabTask.Start();
                }
            }); }
        }
        public RelayCommand GrabOnceCommand
        {
            get
            {
                return new RelayCommand (() => {
                    if (CurrentSelectedCamera >= 0)
                    {
                        cts.Cancel();
                        var CamModel = Vision.CheckCamIDAvilible(CurrentSelectedCamera);
                        Vision.GrabImage(CamModel, true, true);
                        Vision.DisplayImage(CamModel, true);
                    }
                    else
                    {
                        //doNothing
                    }
                });
            }
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public RelayCommand StopGrabCommand
        {
            get
            {
                return new RelayCommand(() => {
                    if(cts!=null)
                        cts.Cancel();
                    CamSnapState = EnumCamSnapState.IDLE;
                });
            }
        }

        /// <summary>
        /// 保存图片命令
        /// </summary>
        public RelayCommand<IntPtr> SaveImagerCommand
        {
            get
            {
                return new RelayCommand<IntPtr>(hWindow =>
                {
                    if (CurrentSelectedCamera >= 0)
                    {
                        DateTime now = DateTime.Now;
                        HalconVision.Instance.SaveImage(CurrentSelectedCamera, SaveImageType ? EnumImageType.Image : EnumImageType.Window, FileHelper.GetCurFilePathString() + "ImageSaved\\ImageSaved", $"{now.Month}月{now.Day}日 {now.Hour}时{now.Minute}分{now.Second}秒_Cam{CurrentSelectedCamera}.jpg", hWindow);
                    }
                });
            }
        }

        /// <summary>
        /// 打开图片命令
        /// </summary>
        public RelayCommand<IntPtr> OpenImageCommand
        {
            get
            {
                return new RelayCommand<IntPtr>(hWindow =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "请选择要打开的文件";
                    ofd.Multiselect = false;
                    ofd.InitialDirectory = PATH_DEFAULT_IMAGEPATH;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        PATH_DEFAULT_IMAGEPATH = ofd.FileName;
                        HalconVision.Instance.OpenImageInWindow(PATH_DEFAULT_IMAGEPATH, hWindow);
                    }

                });
            }
        }

        public RelayCommand<string> CommandDoCalibration
        {
            get
            {
                return new RelayCommand<string>(CamName =>
                {
                    
                });
            }
        }
        public RelayCommand CommandGetImagePoint
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }
        public RelayCommand CommandGetMachinePoint
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }


    }
    #endregion


}
