using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photoShow : MonoBehaviour
{
    public GameObject[] photos;
    [SerializeField] GameObject[] podium;

    public GameObject logic;
    public GameObject results;
    [SerializeField] GameObject photoshow;

    [SerializeField] int[] alreadyShown = new int[3]; // 0 - not shown, 1 - shown for all locations; change the size of the array to the number of locations
    [SerializeField] int index;
    int loop = 0; // change this to the number of locations

    public void Choose()
    {
        if (loop >= 3){
            results.SetActive(true);
            DoPodium();
            return;
        }
        photoshow.SetActive(true);

        index = Random.Range(0, photos.Length);
        while (alreadyShown[index] == 1 && loop < 3) // change the number of locations
        {
            index = Random.Range(0, photos.Length);
        }
        alreadyShown[index] = 1;
        logic.GetComponent<marker>().location = index;
        logic.GetComponent<marker>().ChooseLocation();
        print(index);
        
        for (int i = 0; i < photos.Length; i++)
        {
            photos[i].SetActive(false);
        }

        photos[index].SetActive(true);
        loop++;         
    }   

    void DoPodium()
    {
        
        int[] results = logic.GetComponent<marker>().plscores;
        string[] names = new string[4];
        names[0] = "Player 1"; names[1] = "Player 2"; names[2] = "Player 3"; names[3] = "Player 4";

        Color[] colors = logic.GetComponent<marker>().colors;

        for (int i = 0; i < podium.Length; i++)
        {
            List<int> sortedResults = results.ToList();
            sortedResults.Sort();
            sortedResults.Reverse();

            int podiumIndex = 0;
            for (int j = 0; j < sortedResults.Count && podiumIndex < podium.Length; j++)
            {
                int maxPointsIndex = results.ToList().IndexOf(sortedResults[j]);

                podium[podiumIndex].SetActive(true);
                podium[podiumIndex].transform.GetChild(0).GetComponent<Image>().color = colors[maxPointsIndex];
                podium[podiumIndex].GetComponentInChildren<Text>().text = names[maxPointsIndex] + "\n" + sortedResults[j].ToString() + " points";

                podiumIndex++;
            }
        }
    }
}
