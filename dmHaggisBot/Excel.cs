using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace dmHaggisBot
{
    class Excel
    {
        private static Random rand = new Random();
        private string path = "";
        
        public Excel (string path)
        {
            
        }

        public DataSet ReaderReturn(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    return result;
                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}