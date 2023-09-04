using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CSVToJSON
{
    public class CsvData
    {
        private string[] headers;
        private List<string>[] data;
        private int Rows => data[0].Count;

        public CsvData(string[] headers, List<string>[] data)
        {
            this.headers = headers;
            this.data = data;
        }

        public string[] Headers
        {
            get => headers;
            set => headers = value;
        }

        public List<string>[] Data
        {
            get => data;
            set => data = value;
        }

        public string ToJson()
        {
            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

            for (int i = 0; i < Rows; i++)
            {
                Dictionary<string, object> rowDict = new Dictionary<string, object>();
                for (int j = 0; j < data.Length; j++)
                {
                    string[] split = headers[j].Split('/');
                    Dictionary<string, object> currentDict = rowDict;

                    for (int k = 0; k < split.Length - 1; k++)
                    {
                        string part = split[k];
                        if (!currentDict.ContainsKey(part))
                        {
                            currentDict[part] = new Dictionary<string, object>();
                        }
                        currentDict = (Dictionary<string, object>)currentDict[part];
                    }

                    currentDict[split[split.Length -  1]] = data[j][i];
                }

                resultList.Add(rowDict);
            }

            return JsonConvert.SerializeObject(resultList, Formatting.Indented); }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < headers.Length; i++)
            {
                stringBuilder.Append(headers[i]);
                stringBuilder.Append("\t|\t");
            }
            stringBuilder.Append("\n");

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    stringBuilder.Append(data[j][i]);
                    stringBuilder.Append("\t|\t");
                }
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

    }
}