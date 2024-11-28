using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class CSVController
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //�� �࿡ ���� ������ �и��� ���� ���۷���Ʈ(�и���)
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";//������ CSV�����͸� ���ʹ����� �������� �и���
    static char[] TRIM_CHARS = { '\"' }; //

    public Dictionary<int, ItemData> ItemCSVRead(string file)
    {
        Dictionary<int, ItemData> list = new Dictionary<int, ItemData>();
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

            ItemData item = new ItemData();
            item.ID = (int)DataTypeCheck(typeHeader[0], values[0]);
            item.Name = (string)DataTypeCheck(typeHeader[1], values[1]);
            item.Type = (string)DataTypeCheck(typeHeader[2], values[2]);
            item.Rairty = (string)DataTypeCheck(typeHeader[3], values[3]);
            item.Descripton = (string)DataTypeCheck(typeHeader[4], values[4]);
            item.Attack = (float)DataTypeCheck(typeHeader[5], values[5]); ;
            item.AttackPercent = (bool)DataTypeCheck(typeHeader[6], values[6]); ;
            item.Defence = (float)DataTypeCheck(typeHeader[7], values[7]);
            item.DefencePercent = (bool)DataTypeCheck(typeHeader[8], values[8]);
            item.Health = (float)DataTypeCheck(typeHeader[9], values[9]);
            item.HealthPercent = (bool)DataTypeCheck(typeHeader[10], values[10]);
            item.CritChance = (float)DataTypeCheck(typeHeader[11], values[11]);
            item.CritChancePercent = (bool)DataTypeCheck(typeHeader[12], values[12]);
            item.CritDamage = (float)DataTypeCheck(typeHeader[13], values[13]);
            item.CritDamagePercent = (bool)DataTypeCheck(typeHeader[14], values[14]);
            item.Effect = (string)DataTypeCheck(typeHeader[15], values[15]);
            item.Cost = (int)DataTypeCheck(typeHeader[16], values[16]);
            item.StackMaxCount = (int)DataTypeCheck(typeHeader[17], values[17]);
            list[item.ID] = item;

        }
        return list;
    }
    public Dictionary<int, EnemyData> EnemyCSVRead(string file)
    {
        Dictionary<int, EnemyData> list = new Dictionary<int, EnemyData>();
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

            EnemyData enemy = new EnemyData();
            enemy.ID = (int)DataTypeCheck(typeHeader[0], values[0]);
            enemy.Name = (string)DataTypeCheck(typeHeader[1], values[1]);
            enemy.Descripton = (string)DataTypeCheck(typeHeader[2], values[2]);
            enemy.DropItemID = (List<int>)DataTypeCheck(typeHeader[3], values[3]);
            enemy.DropGold = (int)DataTypeCheck(typeHeader[4], values[4]);
            enemy.Attack = (float)DataTypeCheck(typeHeader[5], values[5]); ;
            enemy.AttackSpeed = (float)DataTypeCheck(typeHeader[6], values[6]);
            enemy.Defence = (float)DataTypeCheck(typeHeader[7], values[7]);
            enemy.Health = (float)DataTypeCheck(typeHeader[8], values[8]);
            enemy.Health = (float)DataTypeCheck(typeHeader[9], values[9]);
            enemy.CritChance = (float)DataTypeCheck(typeHeader[10], values[10]);
            enemy.CritDamage = (float)DataTypeCheck(typeHeader[11], values[11]);
            list[enemy.ID] = enemy;

        }
        return list;
    }
    public Dictionary<int, StagaData> StageCSVRead(string file)
    {
        Dictionary<int, StagaData> list = new Dictionary<int, StagaData>();
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

            StagaData stage = new StagaData();
            stage.ID = (int)DataTypeCheck(typeHeader[0], values[0]);
            stage.ChapterNum = (int)DataTypeCheck(typeHeader[1], values[1]);
            stage.StageNum = (int)DataTypeCheck(typeHeader[2], values[2]);
            stage.CurStageModifier = (float)DataTypeCheck(typeHeader[3], values[3]);
            stage.StageName = (string)DataTypeCheck(typeHeader[4], values[4]);
            stage.SlayEnemyCount = (int)DataTypeCheck(typeHeader[5], values[5]); ;
            stage.SummonEnemyIDList = (List<int>)DataTypeCheck(typeHeader[6], values[6]);
            list[stage.ID] = stage;

        }
        return list;
    }
    public Dictionary<int, SellItemData> SellItemCSVRead(string file)
    {
        Dictionary<int, SellItemData> list = new Dictionary<int, SellItemData>();
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

            SellItemData sellitem = new SellItemData();
            sellitem.ProductID = (int)DataTypeCheck(typeHeader[0], values[0]);
            sellitem.OriginID = (List<int>)DataTypeCheck(typeHeader[1], values[1]);
            sellitem.ProductName = (string)DataTypeCheck(typeHeader[2], values[2]);
            sellitem.ProductDescription = (string)DataTypeCheck(typeHeader[3], values[3]);
            sellitem.PriceType = (string)DataTypeCheck(typeHeader[4], values[4]);
            sellitem.Price = (int)DataTypeCheck(typeHeader[5], values[5]); ;
            sellitem.IsStack = (bool)DataTypeCheck(typeHeader[6], values[6]); ;
            sellitem.StackCount = (int)DataTypeCheck(typeHeader[7], values[7]);
            list[sellitem.ProductID] = sellitem;

        }
        return list;
    }

    private object DataTypeCheck(string type, string value)
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

        if (type.StartsWith("List<"))
        {
            var itemType = type.Substring(5, type.Length - 6);

            //CSV ������ �� ������ Text�̱⿡ \" ö�ڸ� �����ؾߵȴ�. 
            value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

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
