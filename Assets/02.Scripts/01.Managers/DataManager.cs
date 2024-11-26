using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveData
{
    public readonly string saveFileHeader =  "UID,Name,Datas" ;
    public readonly string saveFileHeaderType = "int,String,List<int>" ;

    public int UID;
    public string Name;
    public List<int> Datas;

    public SaveData(int id,string name,List<int> datas)
    {
        this.UID = id;
        this.Name = name;
        this.Datas = datas;
    }
}

public class DataManager : SingletonDDOL<DataManager>
{
    private readonly string csvfilePath = "CSV/TestData";
    private readonly string csvfileNumbering = "1";

    private readonly string csvSaveFilePath = Application.dataPath + "/SaveData.csv";

    private StringBuilder strBuilder = new StringBuilder();

    public CSVController CsvController = new CSVController();

    private List<Dictionary<string, object>> CsvData;

    protected override void Awake()
    {
        base.Awake();   
    }

    private void Start()
    {
        strBuilder.Clear();
        strBuilder.Append(csvfilePath);
        strBuilder.Append(csvfileNumbering);

        CsvData = CsvController.Read(strBuilder.ToString());

        foreach (Dictionary<string, object> listdata  in CsvData)
        {
            foreach(KeyValuePair<string, object> data in listdata)
            {
                Debug.Log($"데이터 키 : {data.Key}  , 데이터 밸류 : {data.Value}");
            }
        }


        //ToDoCode : 저장할데이터를 쓰는 코드 테스트 부분
        List<int> datas = new List<int>();
        datas.Add(2);
        datas.Add(3);
        datas.Add(5);

        SaveData saveData = new SaveData(154351,"지존감자탕", datas);

        strBuilder.Clear();
        strBuilder.Append(csvSaveFilePath);
        CsvController.Write(strBuilder.ToString(), saveData);
    }
}
