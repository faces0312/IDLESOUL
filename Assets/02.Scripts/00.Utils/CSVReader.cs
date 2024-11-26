using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public enum CSVType
{
    
}

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //�� �࿡ ���� ������ �и��� ���� ���۷���Ʈ(�и���)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//������ CSV�����͸� ���ʹ����� �������� �и���
    static char[] TRIM_CHARS = { '\"' }; //

    public List<Dictionary<string, object>> Read(string file)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        //������ �ؽ�Ʈ ����� �ش� �и��ڸ� ���� �и��Ѵ�.
        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE);
        
        //�ش� CSV �����Ϳ� ���(ī�װ�) + �ش� �ڷ����� �Էµ��ִ� �����̹Ƿ� List ��ȯ
        if (lines.Length <= 2) return list;

      
        string[] header = Regex.Split(lines[0], SPLIT_RE);  //�ش� CSV �����Ϳ� ���(ī�װ�)�Է� -> Dictonary�� Ű������ ���
        string[] typeHeader = Regex.Split(lines[1], SPLIT_RE);   //�ش� CSV �����Ϳ� �ڷ��� �Է� -> �ش� �ڷ��� �ڷ��� ������ ����
        for (int i = 2; i < lines.Length; i++)
        {
            string[] values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue; //������ ������ ���� ������ �̵�

            Dictionary<string, object> entry = new Dictionary<string, object>();
            for (int j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                //�������� �� ���� null���ڸ� �����ϰ� �� ������("")�� ��ȯ
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
                throw new Exception($"�������� �ʴ� �ڷ��� Ÿ���Դϴ�.: {type}");
        }
    }
}
