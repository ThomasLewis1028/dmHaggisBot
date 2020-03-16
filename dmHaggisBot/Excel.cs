using System;
using _Excel = Microsoft.Office.Interop.Excel;

namespace dmHaggisBot
{
    class Excel
    {
        private static Random rand = new Random();
        private string path = "";
        private _Excel._Application excel = new _Excel.Application();
        private _Excel.Workbook wb;
        private _Excel.Worksheet ws;
        
        public Excel(string path)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
        }
        
        public void PickSheet(int sheet)
        {
            ws = (_Excel.Worksheet)wb.Worksheets[sheet];
        }
        
        public string ReadCell()
        {
            return ws.Cells[rand.Next(1, ws.Rows.Count), 1].ToString();
        }
    }
}