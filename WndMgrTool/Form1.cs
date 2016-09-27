using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mqd.Win32.API;
using Mqd.Win32.DataDef;

namespace WndMgrTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = IntPtr.Zero;
            string input = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    hWnd = (IntPtr)Convert.ToInt32(input);
                }
                catch (Exception)
                {
                    try
                    {
                        hWnd = (IntPtr)Convert.ToInt32(input, 16);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("没有找到指定的窗口！", "WndMgrTool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                IntPtr pData = Marshal.AllocHGlobal(Marshal.SizeOf(hWnd));
                Win32API.SystemParametersInfo(SysParaInfo.SPI_GETWORKAREA, 0, pData, 0);
                Rect data = (Rect)Marshal.PtrToStructure(pData, typeof(Rect));
                Marshal.FreeHGlobal(pData);
                bool result = Win32API.MoveWindow(hWnd, 0, 0, data.right, data.bottom, true);
                if (result)
                {
                    MessageBox.Show("调整成功！", "WndMgrTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
