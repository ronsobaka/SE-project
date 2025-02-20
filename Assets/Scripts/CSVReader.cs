using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour {


    public static string[,] readData(string file, string[,] array){
        string filePath = Path.Combine(Application.dataPath, "csvData", file);

        if (File.Exists(filePath)) {
            string[] csvContent = File.ReadAllLines(filePath);

            int rowCount = csvContent.Length;
            int colCount = csvContent[0].Split(",").Length;                 //Calculate row / column

            array = new string[rowCount, colCount];         //Create empty 2D array

            for (int i = 0; i < rowCount; i++){
                string[] values = csvContent[i].Split(",");

                for (int j = 0; j < colCount; j++) {
                    array[i,j] = values[j];
                }   
            }
        }
        return array;
    }
}
