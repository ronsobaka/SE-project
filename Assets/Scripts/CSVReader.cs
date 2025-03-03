using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour {

    // Seperated pot luck and opportunity knocks to make it easier to read data

    public static string[] potLuckData;
    public static string[] opportunityKnocksData;

    // Line 18 is where the Second set starts in card data

    void Start() {
        GameController.boardData = readData("PropertyTycoonBoardData.csv", GameController.boardData);
    }

    string[,] readData(string file, string[,] array){
        string filePath = Path.Combine(Application.dataPath, "Files", file);

        if (File.Exists(filePath)) {
            string[] csvContent = File.ReadAllLines(filePath);

            int rowCount = csvContent.Length;
            int colCount = csvContent[0].Split(",").Length;                 //Calculate row / column

            array = new string[rowCount, colCount + 2];         //Create empty 2D array

            potLuckData = new string[17];
            opportunityKnocksData = new string[17];

            for (int i = 0; i < rowCount; i++){
                string[] values = csvContent[i].Split(",");

                for (int j = 0; j < colCount; j++) {
                    array[i,j] = values[j];
                }

                // Takes data for the pot luck from line 1 to 17
                if (i >= 0 && i <= 16) {
                    potLuckData[i] = csvContent[i];
                }

                // Takes data for the opportunity knocks from line 18 to 33
                if (i >= 17 && i <= 33) {
                    opportunityKnocksData[i - 17] = csvContent[i];
                }   
            }
        }
        return array;
    }
}
