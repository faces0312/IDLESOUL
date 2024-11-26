using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //�� �࿡ ���� ������ �и��� ���� ���۷���Ʈ(�и���)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//������ CSV�����͸� ���ʹ����� �������� �и���
    static char[] TRIM_CHARS = { '\"' }; //

    public static List<Dictionary<string, object>> Read(string file)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        //������ �ؽ�Ʈ ����� �ش� �и��ڸ� ���� �и��Ѵ�.
        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE);

        //�ش� CSV �����Ϳ� ���(ī�װ�)�� �Էµ��ִ� �����̹Ƿ� List ��ȯ
        if (lines.Length <= 1) return list;

        //�ش� CSV �����Ϳ� ���(ī�װ�)�Է� -> Dictonary�� Ű������ ���
        string[] header = Regex.Split(lines[0], SPLIT_RE);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue; //������ ������ ���� ������ �̵�

            Dictionary<string, object> entry = new Dictionary<string, object>();
            for (int j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                //�������� �� ���� null���ڸ� �����ϰ� �� ������("")�� ��ȯ
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;

                //�ش� Ÿ�Կ� �°� �����ͺ�ȯ (�ڽ��۾�)
                int n;
                float f;
                bool b;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                else if (bool.TryParse(value, out b))
                {
                    finalvalue = b;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
