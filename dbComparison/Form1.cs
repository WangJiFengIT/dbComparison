using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbComparison
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        #region 比对
        private void Btn_submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_conn1.Text) || string.IsNullOrEmpty(txt_conn2.Text))
            {
                MessageBox.Show("连接字符串不能为空", "比对异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    SqlHelper_1.Open(txt_conn1.Text);
                    SqlHelper_2.Open(txt_conn2.Text);
                    DataTable tables_1 = SqlHelper_1.GetTables();
                    DataTable tables_2 = SqlHelper_2.GetTables();
                    OrderedEnumerableRowCollection<DataRow> dataRows_1 = tables_1.AsEnumerable().OrderBy(a => a.Field<string>("name"));
                    OrderedEnumerableRowCollection<DataRow> dataRows_2 = tables_2.AsEnumerable().OrderBy(a => a.Field<string>("name"));
                    //创建Excel文件的对象
                    IWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    #region 设置表头样式
                    //创建样式对象
                    ICellStyle style = book.CreateCellStyle();
                    //创建一个字体样式对象
                    IFont font = book.CreateFont();
                    font.Boldweight = short.MaxValue;
                    font.FontHeightInPoints = 16;
                    //水平居中
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    //垂直居中
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CenterSelection;
                    //将字体样式赋给样式对象
                    style.SetFont(font);
                    #endregion

                    #region 设置内容样式
                    //创建样式对象
                    ICellStyle style2 = book.CreateCellStyle();
                    //水平居中
                    style2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    //垂直居中
                    style2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CenterSelection;
                    #endregion
                    foreach (DataRow row in dataRows_1)
                    {
                        string tablename = row["name"].ToString().Trim();
                        DataTable tableInfo_1 = SqlHelper_1.GetTableInfo(tablename);
                        DataTable tableInfo_2 = SqlHelper_2.GetTableInfo(tablename);
                        if ("Exam_ExamQuestion".Equals(tablename) || "Exam_ExamSheet".Equals(tablename))
                        {
                            IEnumerable<DataRow> query = tableInfo_1.AsEnumerable().Except(tableInfo_2.AsEnumerable(), DataRowComparer.Default);
                            DataTable changesTable = query.CopyToDataTable();
                            if (changesTable != null)
                            {
                                //添加一个sheet
                                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet(tablename);
                                //给sheet1添加第一行的头部标题
                                NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                                int c = 0;
                                foreach (DataColumn item in changesTable.Columns)
                                {
                                    //设置宽度
                                    sheet1.SetColumnWidth(c, 40 * 150);
                                    ICell cell = row1.CreateCell(c);
                                    cell.SetCellValue(item.Caption);
                                    cell.CellStyle = style;
                                    c++;
                                }
                                //将数据逐步写入sheet1各个行
                                int k = 0;
                                foreach (DataRow item in changesTable.Rows)
                                {
                                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(k + 1);
                                    for (int i = 0; i < changesTable.Columns.Count; i++)
                                    {
                                        ICell cell = rowtemp.CreateCell(i);
                                        cell.SetCellValue(item[i].ToString());
                                        cell.CellStyle = style2;
                                    }
                                    k++;
                                }
                            }
                        }
                    }
                    //写入文件
                    string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    FileStream xlsfile = new FileStream(DesktopPath + @"\file" + ".xls", FileMode.Create);
                    book.Write(xlsfile);
                    xlsfile.Dispose();
                    MessageBox.Show("导出成功，文件存放于桌面", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception es)
                {
                    MessageBox.Show("比对出错" + es.Message, "比对异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SqlHelper_1.Close();
                SqlHelper_2.Close();
            }
        }
        #endregion

        #region 连接测试
        private void Btn_cn1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_conn1.Text))
            {
                MessageBox.Show("连接字符串不能为空", "输入异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    MessageBox.Show("连接成功", "连接提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception es)
                {
                    MessageBox.Show("连接失败，" + es.Message, "连接提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Btn_cn2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_conn2.Text))
            {
                MessageBox.Show("连接字符串不能为空", "输入异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    MessageBox.Show("连接成功", "连接提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception es)
                {
                    MessageBox.Show("连接失败，" + es.Message, "连接提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
