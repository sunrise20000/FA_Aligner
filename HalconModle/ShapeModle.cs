using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using ViewROI;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using FileOperate;
using HalWindow;

namespace HalconModle
{
    public partial class ShapeModle : UserControl
    {
        private HObject m_image;
        private HTuple m_modleHandle;
        ShapeModleParameterTrain m_paramTrain;
        ShapeModelParameterFind m_paramFind;
        public ShapeModle()
        {
            InitializeComponent();
            cmB_TrainOptimization.DataSource = Enum.GetValues(typeof(EnumOptimization));
            cmB_TrainMetric.DataSource = Enum.GetValues(typeof(EnumMetric));
            cmB_TrainContrast.DataSource = Enum.GetValues(typeof(EnumContrast));
            cmB_FindSubpixel.DataSource = Enum.GetValues(typeof(EnumSubpixel));
            m_paramTrain = new ShapeModleParameterTrain();
            m_paramFind = new ShapeModelParameterFind();
            AddEvent();
        }
        private void AddEvent()
        {
            nmUD_TrainAngleStart.ValueChanged += UpdateParam;
            nmUD_TrainAngleExtent.ValueChanged += UpdateParam;
            nmUD_TrainAngleStep.ValueChanged += UpdateParam;
            nmUD_TrainNumLevels.ValueChanged += UpdateParam;
            cmB_TrainOptimization.TextChanged += UpdateParam;
            cmB_TrainMetric.TextChanged += UpdateParam;
            cmB_TrainContrast.TextChanged += UpdateParam;
            cmB_TrainMinContrast.TextChanged += UpdateParam;

            nmUD_FindAngleStart.ValueChanged += UpdateParam;
            nmUD_FindAngleExtent.ValueChanged += UpdateParam;
            nmUD_FindAngleStep.ValueChanged += UpdateParam;
            nmUD_FindMinScore.ValueChanged += UpdateParam;
            nmUD_FindNumMachs.ValueChanged += UpdateParam;
            nmUD_FindMaxOverLap.ValueChanged += UpdateParam;
            nmUD_FindNumLevels.ValueChanged += UpdateParam;
            nmUD_FindGreediness.ValueChanged += UpdateParam;
            cmB_FindSubpixel.TextChanged += UpdateParam;
        }
        private void RemoveRvent()
        {
            nmUD_TrainAngleStart.ValueChanged -= UpdateParam;
            nmUD_TrainAngleExtent.ValueChanged -= UpdateParam;
            nmUD_TrainAngleStep.ValueChanged -= UpdateParam;
            nmUD_TrainNumLevels.ValueChanged -= UpdateParam;
            cmB_TrainOptimization.TextChanged -= UpdateParam;
            cmB_TrainMetric.TextChanged -= UpdateParam;
            cmB_TrainContrast.TextChanged -= UpdateParam;
            cmB_TrainMinContrast.TextChanged -= UpdateParam;

            nmUD_FindAngleStart.ValueChanged -= UpdateParam;
            nmUD_FindAngleExtent.ValueChanged -= UpdateParam;
            nmUD_FindAngleStep.ValueChanged -= UpdateParam;
            nmUD_FindMinScore.ValueChanged -= UpdateParam;
            nmUD_FindNumMachs.ValueChanged -= UpdateParam;
            nmUD_FindMaxOverLap.ValueChanged -= UpdateParam;
            nmUD_FindNumLevels.ValueChanged -= UpdateParam;
            nmUD_FindGreediness.ValueChanged -= UpdateParam;
            cmB_FindSubpixel.TextChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            m_paramTrain.AngleStart = (int)nmUD_TrainAngleStart.Value;
            m_paramTrain.AngleExtent = (int)nmUD_TrainAngleExtent.Value;
            m_paramTrain.AngleStep = (double)nmUD_FindAngleStep.Value;
            m_paramTrain.NumLevels = (int)nmUD_TrainNumLevels.Value;
            m_paramTrain.Optimization = cmB_TrainOptimization.Text;
            m_paramTrain.Metric = cmB_TrainMetric.Text;
            m_paramTrain.Contrast = cmB_TrainContrast.Text;//此处可能手动设置为整形，需修改
            m_paramTrain.MinContrast = cmB_TrainMinContrast.Text;//此处可能手动设置为整形，需修改

            m_paramFind.AngleStart = (int)nmUD_FindAngleStart.Value;
            m_paramFind.AngleExtent = (int)nmUD_FindAngleExtent.Value;
            m_paramFind.AngleStep = (double)nmUD_FindAngleStep.Value;
            m_paramFind.MinScore = (double)nmUD_FindMinScore.Value;
            m_paramFind.NumMatch = (int)nmUD_FindNumMachs.Value;
            m_paramFind.MaxOverLap = (double)nmUD_FindMaxOverLap.Value;
            m_paramFind.NumLevels = (int)nmUD_FindNumLevels.Value;
            m_paramFind.Greediness = (double)nmUD_FindGreediness.Value;
            m_paramFind.SubPixel = cmB_FindSubpixel.Text;                   
        }
        public HObject BackImage
        {          
            set
            {
                if (value.IsInitialized())
                {
                    m_image = value.Clone();
                    hDisplay1.HImageX = m_image;
                }
            }
        }
        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "(.bmp)|*.bmp|(.png)|*.png|(.jpeg)|*.jpeg|(.tif)|*.tif|(.jpg)|*.jpg";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string _path = ofd.FileName;
                    HObject _iamge = new HObject();
                    HOperatorSet.ReadImage(out _iamge, _path);
                    BackImage = _iamge;
                }
            }
        }

        private void btn_AddModleRegion_Click(object sender, EventArgs e)
        {
            if (hDisplay1.GetTrainRegions().Count == 0)
            {
                hDisplay1.AddRegion("矩形", false);
                return;
            }
            MessageBox.Show("区域已存在","提示");
        }

        private void btn_TrainModle_Click(object sender, EventArgs e)
        {
            if (hDisplay1.GetTrainRegions().Count == 0)
            {
                MessageBox.Show("请先设置模板区域","提示");
                return;                   
            }
            try
            {
                HObject _imagereduced = new HObject();
                HOperatorSet.ReduceDomain(m_image, hDisplay1.GetTrainRegions().ElementAt(0), out _imagereduced);
                HOperatorSet.CreateShapeModel(_imagereduced, m_paramTrain.NumLevels, m_paramTrain.AngleStart, m_paramTrain.AngleExtent,
                    /*m_paramTrain.AngleStep*/"auto", m_paramTrain.Optimization, m_paramTrain.Metric, m_paramTrain.Contrast, m_paramTrain.MinContrast, out m_modleHandle);

                HTuple _column = new HTuple(), _angle = new HTuple(), _score = new HTuple();

                HOperatorSet.FindShapeModel(_imagereduced, m_modleHandle, m_paramFind.AngleStart, m_paramFind.AngleExtent, m_paramFind.MinScore,
                    m_paramFind.NumMatch, m_paramFind.MaxOverLap, m_paramFind.SubPixel, m_paramFind.NumLevels, m_paramFind.Greediness, out HTuple _row, out _column, out _angle, out _score);
               // HOperatorSet.WriteShapeModel(m_modleHandle, AppDomain.CurrentDomain.BaseDirectory + "123.shm");
                if (_row.TupleLength() > 0)
                {
                    HObject _modlecountor = new HObject();
                    HOperatorSet.GetShapeModelContours(out _modlecountor, m_modleHandle, 1);
                    HTuple _hommat2d;
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, _row.D, _column.D, _angle.D, out _hommat2d);
                    HObject _transcountor;
                    HOperatorSet.AffineTransContourXld(_modlecountor, out _transcountor, _hommat2d);
                    HalWindow.RegionX regionX = new HalWindow.RegionX(_transcountor, "green");
                    hDisplay1.HRegionXList = new List<RegionX>() { regionX };


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常");
            }
        }

        private void btn_GrabTraget_Click(object sender, EventArgs e)
        {
            if (m_modleHandle != null)
            {
                try
                {
                    HTuple _column = new HTuple(), _angle = new HTuple(), _score = new HTuple();

                    HOperatorSet.FindShapeModel(m_image, m_modleHandle, m_paramFind.AngleStart, m_paramFind.AngleExtent, m_paramFind.MinScore,
                        m_paramFind.NumMatch, m_paramFind.MaxOverLap, m_paramFind.SubPixel, m_paramFind.NumLevels, m_paramFind.Greediness, out HTuple _row, out _column, out _angle, out _score);
                    hDisplay1.HRegionXList = null;
                    if (_row.TupleLength() > 0)
                    {
                        List<RegionX> lr = new List<RegionX>();
                        for (int i=0;i< _row.TupleLength();i++)
                        {

                            HObject _modlecountor = new HObject();
                            HOperatorSet.GetShapeModelContours(out _modlecountor, m_modleHandle, 1);
                            HTuple _hommat2d;
                            HOperatorSet.VectorAngleToRigid(0, 0, 0, _row.DArr[i], _column.DArr[i], _angle.DArr[i], out _hommat2d);
                            HObject _transcountor;
                            HOperatorSet.AffineTransContourXld(_modlecountor, out _transcountor, _hommat2d);
                            HalWindow.RegionX regionX = new HalWindow.RegionX(_transcountor, "green");
                            lr.Add(regionX);
                        }
                        hDisplay1.HImageX = m_image;
                        hDisplay1.HRegionXList = lr;

                    }
                    else
                    {
                        hDisplay1.HImageX = m_image;
                        hDisplay1.HRegionXList = null;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常");
                }
            }
        }

        private void btn_SaveModleInfo_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择要保存文件的文件夹";
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {

                    string _filename = Path.GetFileNameWithoutExtension(folderBrowserDialog.SelectedPath);

                    HOperatorSet.WriteShapeModel(m_modleHandle, folderBrowserDialog.SelectedPath + "\\" + _filename + ".shm");
                    INI ini = new INI();

                    PropertyInfo[] infotrain = m_paramTrain.GetType().GetProperties();
                    foreach (var item in infotrain)
                    {
                        string name = item.Name;
                        object value = item.GetValue(m_paramTrain, null);
                        ini.IniWrite("ParamTrain", name, value.ToString(), folderBrowserDialog.SelectedPath + "\\" + _filename + ".ini");
                    }
                    PropertyInfo[] infofind = m_paramFind.GetType().GetProperties();
                    foreach (var item in infofind)
                    {
                        string name = item.Name;
                        object value = item.GetValue(m_paramFind, null);
                        ini.IniWrite("ParamFind", name, value.ToString(), folderBrowserDialog.SelectedPath + "\\" + _filename + ".ini");
                    }
                    using (Stream stream = new FileStream(folderBrowserDialog.SelectedPath + "\\" + _filename + ".MP", FileMode.Create, FileAccess.Write))
                    {
                        List<object> datas = new List<object>();
                        datas.Add(m_paramTrain);
                        datas.Add(m_paramFind);
                        datas.Add(hDisplay1.GetROIList());
                        //XmlSerializer xmlSerializer = new XmlSerializer(datas.GetType());
                        //xmlSerializer.Serialize(stream, datas);
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(stream, datas);
                    }

                }
            }
            
        }

        private void btn_LoadModleIInfo_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择模板文件夹";

                folderBrowserDialog.ShowNewFolderButton = false;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    var files = Directory.GetFiles(folderBrowserDialog.SelectedPath, ".");
                    foreach (var file in files)
                    {
                        string extension = Path.GetExtension(file);
                        if (Path.GetExtension(file) == ".shm")
                        {
                            HOperatorSet.ReadShapeModel(file, out m_modleHandle);
                        }
                        else if (Path.GetExtension(file) == ".MP")
                        {
                            try
                            {
                                using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                                {
                                    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<object>));
                                    //List<object> datas = xmlSerializer.Deserialize(stream) as List<object>;
                                    BinaryFormatter bf = new BinaryFormatter();
                                    List<object> datas = bf.Deserialize(stream) as List<object>;

                                    m_paramTrain = (ShapeModleParameterTrain)datas[0];
                                    m_paramFind = (ShapeModelParameterFind)datas[1];
                                   // System.Collections.ArrayList regionXes = datas[2] as System.Collections.ArrayList;
                                    hDisplay1.SetROIList(datas[2] as System.Collections.ArrayList);
                                   

                                    RemoveRvent();

                                    nmUD_TrainAngleStart.Value = m_paramTrain.AngleStart;
                                    nmUD_TrainAngleExtent.Value = m_paramTrain.AngleExtent;
                                    nmUD_FindAngleStep.Value= (decimal)m_paramTrain.AngleStep;
                                    nmUD_TrainNumLevels.Value = m_paramTrain.NumLevels;
                                    cmB_TrainOptimization.Text= m_paramTrain.Optimization;
                                    cmB_TrainMetric.Text= m_paramTrain.Metric;
                                    cmB_TrainContrast.Text= m_paramTrain.Contrast;
                                    cmB_TrainMinContrast.Text= m_paramTrain.MinContrast;//此处可能手动设置为整形，需修改

                                    nmUD_FindAngleStart.Value= m_paramFind.AngleStart;
                                    nmUD_FindAngleExtent.Value= m_paramFind.AngleExtent;
                                    nmUD_FindAngleStep.Value= (decimal)m_paramFind.AngleStep;
                                    nmUD_FindMinScore.Value=(decimal)m_paramFind.MinScore;
                                    nmUD_FindNumMachs.Value= m_paramFind.NumMatch;
                                    nmUD_FindMaxOverLap.Value= (decimal)m_paramFind.MaxOverLap;
                                   nmUD_FindNumLevels.Value = m_paramFind.NumLevels;
                                    nmUD_FindGreediness.Value= (decimal)m_paramFind.Greediness;
                                    cmB_FindSubpixel.Text= m_paramFind.SubPixel;

                                    AddEvent();

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "异常");
                            }


                        }

                    }
                }
            }
        }
        public void LoadModle(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception("未能找到指定路径");
            }
            var files = Directory.GetFiles(directoryPath, ".");
            bool _findFile = false;
            foreach (var member in files)
            {
                string extension = Path.GetExtension(member);
                if (extension == ".shm")
                {
                    HOperatorSet.ReadShapeModel(member, out m_modleHandle);
                    _findFile = true;
                    break;
                }
            }
            if (!_findFile)
            {
                throw new Exception("指定路径中未能找到模板文件（.shm）");

            }
            foreach (var member in files)
            {
                string extension = Path.GetExtension(member);
                if (extension == ".MP")
                {
                    try
                    {
                        using (Stream stream = new FileStream(member, FileMode.Open, FileAccess.Read))
                        {
                            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<object>));
                            //List<object> datas = xmlSerializer.Deserialize(stream) as List<object>;
                            BinaryFormatter bf = new BinaryFormatter();
                            List<object> datas = bf.Deserialize(stream) as List<object>;

                            m_paramTrain = (ShapeModleParameterTrain)datas[0];
                            m_paramFind = (ShapeModelParameterFind)datas[1];
                            hDisplay1.SetROIList(datas[2] as System.Collections.ArrayList);
                            RemoveRvent();

                            nmUD_TrainAngleStart.Value = m_paramTrain.AngleStart;
                            nmUD_TrainAngleExtent.Value = m_paramTrain.AngleExtent;
                            nmUD_FindAngleStep.Value = (decimal)m_paramTrain.AngleStep;
                            nmUD_TrainNumLevels.Value = m_paramTrain.NumLevels;
                            cmB_TrainOptimization.Text = m_paramTrain.Optimization;
                            cmB_TrainMetric.Text = m_paramTrain.Metric;
                            cmB_TrainContrast.Text = m_paramTrain.Contrast;
                            cmB_TrainMinContrast.Text = m_paramTrain.MinContrast;//此处可能手动设置为整形，需修改

                            nmUD_FindAngleStart.Value = m_paramFind.AngleStart;
                            nmUD_FindAngleExtent.Value = m_paramFind.AngleExtent;
                            nmUD_FindAngleStep.Value = (decimal)m_paramFind.AngleStep;
                            nmUD_FindMinScore.Value = (decimal)m_paramFind.MinScore;
                            nmUD_FindNumMachs.Value = m_paramFind.NumMatch;
                            nmUD_FindMaxOverLap.Value = (decimal)m_paramFind.MaxOverLap;
                            nmUD_FindNumLevels.Value = m_paramFind.NumLevels;
                            nmUD_FindGreediness.Value = (decimal)m_paramFind.Greediness;
                            cmB_FindSubpixel.Text = m_paramFind.SubPixel;

                            AddEvent();
                        }
                        _findFile = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            if (!_findFile)
            {
                throw new Exception("指定路径中未能找到模板参数文件（.MP）");
            }
        }
        public ModleFindResult FindSimple()
        {
            ModleFindResult result = new ModleFindResult();
            if (m_modleHandle != null)
            {
                try
                {
                    HTuple _column = new HTuple(), _angle = new HTuple(), _score = new HTuple();

                    HOperatorSet.FindShapeModel(m_image, m_modleHandle, m_paramFind.AngleStart, m_paramFind.AngleExtent, m_paramFind.MinScore,
                        1, m_paramFind.MaxOverLap, m_paramFind.SubPixel, m_paramFind.NumLevels, m_paramFind.Greediness, out HTuple _row, out _column, out _angle, out _score);

                    if (_row.TupleLength() > 0)
                    {
                        HObject _modlecountor = new HObject();
                        HOperatorSet.GetShapeModelContours(out _modlecountor, m_modleHandle, 1);
                        HTuple _hommat2d;
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, _row.D, _column.D, _angle.D, out _hommat2d);
                        HObject _transcountor;
                        HOperatorSet.AffineTransContourXld(_modlecountor, out _transcountor, _hommat2d);
                        result.Row = _row.D;
                        result.Column = _column.D;
                        result.Score = _score.D;
                        result.Angle = _angle.D;
                        result.ModleRegion = _modlecountor;
                        return result;

                    }
                    else
                    {
                        return null;                          
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;

        }
        public List<ModleFindResult> FindMuiltple(uint numfind)
        {
            List<ModleFindResult> resultlist = new List<ModleFindResult>();
            if (m_modleHandle != null)
            {
                try
                {
                    HTuple _column = new HTuple(), _angle = new HTuple(), _score = new HTuple();

                    HOperatorSet.FindShapeModel(m_image, m_modleHandle, m_paramFind.AngleStart, m_paramFind.AngleExtent, m_paramFind.MinScore,
                        numfind, m_paramFind.MaxOverLap, m_paramFind.SubPixel, m_paramFind.NumLevels, m_paramFind.Greediness, out HTuple _row, out _column, out _angle, out _score);

                    if (_row.TupleLength() > 0)
                    {
                        for (int i = 0; i < _row.Length; i++)
                        {
                            HObject _modlecountor = new HObject();
                            HOperatorSet.GetShapeModelContours(out _modlecountor, m_modleHandle, 1);
                            HTuple _hommat2d;
                            HOperatorSet.VectorAngleToRigid(0, 0, 0, _row[i], _column[i], _angle[i], out _hommat2d);
                            HObject _transcountor;
                            HOperatorSet.AffineTransContourXld(_modlecountor, out _transcountor, _hommat2d);
                            ModleFindResult result = new ModleFindResult();
                            result.Row = _row.DArr[i];
                            result.Column = _column.DArr[i];
                            result.Score = _score.DArr[i];
                            result.Angle = _angle.DArr[i];
                            result.ModleRegion = _modlecountor;
                            resultlist.Add(result);
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return resultlist;

        }
    }
    [Serializable]
    public class ModleFindResult
    {
        public  double  Score { get; set; }
        public double Row { get; set; }
        public double Column { get; set; }
        public double Angle { get; set; }
        public double Scale { get; set; }
        public HObject ModleRegion { get; set; }
    }
    [Serializable]
    public class ShapeModleParameterTrain
    {
        public int NumLevels { get; set; }
        public int AngleStart { get; set; }
        public int AngleExtent { get; set; }
        public double AngleStep { get; set; }
        public string Optimization { get; set; }
        public string Metric { get; set; }
        public HTuple Contrast { get; set; }
        public HTuple MinContrast { get; set; }

    }
    [Serializable]
    public class ShapeModelParameterFind
    {
        public int NumLevels { get; set; }
        public int AngleStart { get; set; }
        public int AngleExtent { get; set; }
        public double  AngleStep { get; set; }
        public double MinScore { get; set; }
        public int NumMatch { get; set; }
        public double MaxOverLap { get; set; }
        public double Greediness { get; set; }
        public string SubPixel { get; set; }
    }
    public enum EnumOptimization
    {
        auto,
        no_pregeneration,
        none,
        point_reduction_high,
        point_reduction_low,
        point_reduction_medium,
        pregeneration
    }
    public enum EnumMetric
    {
        use_polarity,
        ignore_color_polarity, 
        ignore_global_polarity,
        ignore_local_polarity,       
    }
    public enum EnumContrast
    {
        auto, 
        auto_contrast,
        auto_contrast_hyst,
        auto_min_size
    }
    public enum EnumSubpixel
    {
        none,
        interpolation, 
        least_squares,
        least_squares_high,
        least_squares_very_high,      
    }
}
