using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DQ.LXJHGL.COMMON
{
    public static class ExcelUtil
    {
        public static List<LXJHGLInstance> ImportTasks(string filename)
        {
            List<LXJHGLInstance> tasks = new List<LXJHGLInstance>();
            using (FileStream fs = File.OpenRead(filename))
            {
                IWorkbook wb = WorkbookFactory.Create(fs);
                var sheet = wb.GetSheetAt(0);
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    ICell cell = row == null ? null : row.GetCell(0);
                    if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
                    {
                        var task = new LXJHGLInstance();
                        task.Id = cell.StringCellValue;
                        task.Name = row.GetCell(1).StringCellValue;
                        task.Version = int.Parse(row.GetCell(2).StringCellValue);
                        task.Releaser = row.GetCell(3).StringCellValue;
                        task.Releasetime = DateTime.ParseExact(row.GetCell(4).StringCellValue, 
                            "yyyy年MM月dd日", CultureInfo.InvariantCulture);
                        task.Type = row.GetCell(5).StringCellValue;
                        task.Creator = row.GetCell(6) == null ? string.Empty : row.GetCell(6).StringCellValue;
                        task.Status = LXJHGLStatus.未分配;
                        task.Taskcreatime = DateTime.Now;
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }
    }
}
