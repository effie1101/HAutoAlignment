using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using HalconDotNet;
using FC.MarkLocator;

namespace HAutoAlignment
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // The class HDevelopExport will be defined in the HALCON program
    // exported from HDevelop for 'C# - HALCON/.NET' and the Template
    // Window Export.
   // private FindProductMark HDevExp;
        FC.MarkLocator.ProcessImage ProcessImage = new ProcessImage();
        ViewModel.MainViewModel ViewModel = new ViewModel.MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += delegate
            {
                this.DataContext = ViewModel;
            };
        }

    private void buttonRun_Click(object sender, RoutedEventArgs e)
    {
            string fileName = GetImgFile();
            HTuple WindowID = hWindow.HalconID;
            // HDevExp.RunHalcon(WindowID, fileName);
            ProcessImage.MarkAlignment(fileName,WindowID);
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        private void InitializeEvents()
        {
            this.btnOpenImgFileU.Click += delegate
            {
                
            };

            this.btnOpenImgFileL.Click += delegate
            {
            };

            this.btnFindMark.Click += delegate
            {
                //if (ProcessImage.MarkAlignment(ProcessImage.InputManager.MarkImageFile))
                //{
                //    this.imgMark.Source = new BitmapImage(new Uri(ProcessImage.OutputManager.MarkImgFile));
                //    this.rtxtOutput.AppendText(string.Format("\n[{0},{1},{2}]\n{3}",
                //                                                           ProcessImage.OutputManager.AlignmentX,
                //                                                           ProcessImage.OutputManager.AlignmentY,
                //                                                           ProcessImage.OutputManager.AlignmentSita,
                //                                                           ProcessImage.OutputManager.MarkImgFile));
                //}
                //else
                //{
                //    this.rtxtOutput.AppendText("\r\n靶标识别失败");
                //}


            };
        }


        private string GetImgFile()
        {
            string fileName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            openFileDialog.Filter = " (*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jgp; *.png; *.jpeg; *.bmp";
            if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
            {
                fileName = openFileDialog.FileName;
               // ProcessImage.InputManager.MarkImageFile = openFileDialog.FileName;
            }
            else
            {
                fileName = string.Empty;
            }
            return fileName;
        }
    }
}
