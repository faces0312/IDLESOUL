using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine.Analytics;

public enum CSVEnumType
{
    Bullet = 0,
    Enemy = 1,

}

public class CSVController
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //한 행에 대한 데이터 분리를 위한 세퍼레이트(분리자)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//가져온 CSV데이터를 엔터단위로 끊기위한 분리자
    static char[] TRIM_CHARS = { '\"' }; //

    private StringBuilder strBuilder = new StringBuilder();

    public void Write(string filePath, SaveData saveData)
    {
        TextWriter tw = new StreamWriter(filePath, false, Encoding.UTF8 );

        tw.WriteLine(saveData.saveFileHeader);
        tw.WriteLine(saveData.saveFileHeaderType);

        strBuilder.Clear();
        strBuilder.Append(saveData.UID);
        strBuilder.Append(",");
        strBuilder.Append(saveData.Name);
        strBuilder.Append(",");

        strBuilder.Append('\"');
        for (int i = 0 ; i < saveData.Datas.Count; i++)
        {
            strBuilder.Append(saveData.Datas[i].ToString() + ",");
        }
        strBuilder.Append('\"');
        tw.WriteLine(strBuilder.ToString());
        tw.Close();

    }

    public List<Dictionary<string, object>> Read(string file)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        //가져온 텍스트 덩어리를 해당 분리자를 통해 분리한다.
        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE);
        
        //해당 CSV 데이터에 헤더(카테고리) + 해당 자료형만 입력되있는 상태이므로 List 반환
        if (lines.Length <= 2) return list;

      
        string[] header = Regex.Split(lines[0], SPLIT_RE);  //해당 CSV 데이터에 헤더(카테고리)입력 -> Dictonary의 키값으로 사용
        string[] typeHeader = Regex.Split(lines[1], SPLIT_RE);   //해당 CSV 데이터에 자료형 입력 -> 해당 자료의 자료형 선정시 사용됨
        for (int i = 2; i < lines.Length; i++)
        {
            string[] values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue; //정보가 없으면 다음 행으로 이동

            Dictionary<string, object> entry = new Dictionary<string, object>();
            for (int j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                //데이터의 앞 뒤의 null문자를 제거하고 빈 데이터("")로 전환
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                var finalvalue = DataTypeCheck(typeHeader[j] , value);

                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    private object DataTypeCheck(string type ,string value)
    {
        switch (type)
        {
            case "int":
                return int.Parse(value);  //int
            case "float":
                return float.Parse(value); //float
            case "double":
                return double.Parse(value);  //double
            case "bool":
                return bool.Parse(value);  //bool
            case "string":
                return value;  //string
            //default:
            //    throw new Exception($"지원하지 않는 자료형 타입입니다.: {type}");
        }

        if(type.StartsWith("List<Enum"))
        {

        }
        else if (type.StartsWith("List<"))
        {
            var itemType = type.Substring(5, type.Length - 6);

            switch (itemType)
            {
                case "int":
                    return value.Split(',').Select(int.Parse).ToList();  //List<int>
                case "float":
                    return value.Split(',').Select(float.Parse).ToList(); //List<float>
                case "double":
                    return value.Split(',').Select(double.Parse).ToList(); //List<double>
                case "bool":
                    return value.Split(',').Select(bool.Parse).ToList();  //List<bool>
                case "string":
                    return value.Split(',').Select(v => v.Trim()).ToList();  //List<string>
                    
            }
        }

        return null;
    }
}
