
using System;
using System.Drawing;
using System.Collections.Generic;
using HalconDotNet;

namespace FC.MarkLocator
{
    public class ProcessImage
    {
        const double CCD_W = 4.8;
        const double CCD_H = 3.6;
        const int IMG_WIDTH = 1292;
        const int IMG_HEIGHT = 964;
        string logPath = string.Empty;
        string DUTNo = string.Empty;
         
        public MarkLocator.InputManager InputManager { get { return MarkLocator.InputManager.Instance; } }

        public MarkLocator.OutputManager OutputManager { get { return MarkLocator.OutputManager.Instance; } }

        HFindProductMark_0551601 FindProductMark = new HFindProductMark_0551601();


        /// <summary>
        /// 计算对位需要调整的距离，输出参数见OutputManager
        /// </summary>
        /// <param name="imgFile">输入产品的靶标的原始图片</param>
        /// <returns>bool result</returns>
        public bool MarkAlignment(string imgFile, HTuple windowID)
        {
            //初始化
            FindProductMark.InitHalcon();
            HFindProductMark_0551601.hv_ExpDefaultWinHandle = windowID;

            double row, col, angle;
            //识别产品靶标在图像上的位置坐标
            bool result = FindProductMark.FindMark(imgFile, out row, out col, out angle);
            OutputManager.CenterRow = row;
            OutputManager.CenterCol = col;
            OutputManager.Angle = angle;

            //计算相对位移
            this.OutputManager.AlignmentSita = Math.Round(angle + this.InputManager.CCD2ProbeSita, 2);
            this.OutputManager.AlignmentX = Math.Round(0 - this.InputManager.ProbeHeadRotateR * Math.Sin(this.OutputManager.AlignmentSita / 180 * Math.PI) + this.InputManager.CCD2ProbeX + (col / IMG_WIDTH * CCD_W - CCD_W / 2) + this.InputManager.BondpadCenterX, 2);
            this.OutputManager.AlignmentY = Math.Round(this.InputManager.ProbeHeadRotateR * (1 - Math.Cos(this.OutputManager.AlignmentSita / 180 * Math.PI)) + this.InputManager.CCD2ProbeY + (row / IMG_HEIGHT* CCD_H - CCD_H / 2) + this.InputManager.BondpadCenterY, 2);

            return result;
        }


    }
}
