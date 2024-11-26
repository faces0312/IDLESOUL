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
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //�� �࿡ ���� ������ �и��� ���� ���۷���Ʈ(�и���)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//������ CSV�����͸� ���ʹ����� �������� �и���
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
            //default:
            //    throw new Exception($"�������� �ʴ� �ڷ��� Ÿ���Դϴ�.: {type}");
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
