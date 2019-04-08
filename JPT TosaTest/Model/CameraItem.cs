using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPT_TosaTest.Models 
{
    public enum EnumCamState
    {
        Connected,
        DisConnected,
    }

    public class CameraItem : INotifyPropertyChanged
    {
        private EnumCamState _strCameraState =EnumCamState.DisConnected;
        private string _cameraName = "";
        public EnumCamState StrCameraState
        {
            set
            {
                if (_strCameraState != value)
                {
                    _strCameraState = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StrCameraState"));
                }
            }
            get { return _strCameraState; }
        }
        public string CameraName
        {
            set
            {
                if (_cameraName != value)
                {
                    _cameraName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CameraName"));
                }
            }
            get { return _cameraName; }
        }

        public override string ToString()
        {
            return CameraName;
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
