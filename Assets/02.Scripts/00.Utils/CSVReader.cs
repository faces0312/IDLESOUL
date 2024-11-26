using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public enum CSVType
{
    
}

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //한 행에 대한 데이터 분리를 위한 세퍼레이트(분리자)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//가져온 CSV데이터를 엔터단위로 끊기위한 분리자
    static char[] TRIM_CHARS = { '\"' }; //

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
            default:
                throw new Exception($"지원하지 않는 자료형 타입입니다.: {type}");
        }
    }
}
