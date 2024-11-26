using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataManager : SingletonDDOL<DataManager>
{
    private readonly string csvfilePath = "CSV/TestData";
    private readonly string csvfileNumbering = "1";

    private StringBuilder strBuilder = new StringBuilder();

    public CSVReader CsvReader = new CSVReader();

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

        CsvData = CsvReader.Read(strBuilder.ToString());

        foreach (Dictionary<string, object> listdata  in CsvData)
        {
            foreach(KeyValuePair<string, object> data in listdata)
            {
                Debug.Log($"데이터 키 : {data.Key}  , 데이터 밸류 : {data.Value}");
            }
        }
    }
}
