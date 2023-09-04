using System;
using System.Collections.Generic;
using System.IO;
namespace CSVToJSON
{
    public static class CsvReader
    {
        public static CsvData ParseCsvWithHeader(string csvInput)
        {
            using (var csvReader = new StringReader(csvInput))
            using (var parser = new NotVisualBasic.FileIO.CsvTextFieldParser(csvReader))
            {
                if (parser.EndOfData)
                {
                    return null;
                }
                string[] headerFields = parser.ReadFields();
                List<string>[] data = new List<string>[headerFields.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new List<string>();
                }
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i].Add(fields[i]);
                    }
                }

                return new CsvData(headerFields, data);
            }
        }
    }
}