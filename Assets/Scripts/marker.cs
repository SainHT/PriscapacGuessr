using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marker : MonoBehaviour
{
    public GameObject[] locations;
    public int location;
    public GameObject[] markerPrefs;
    public GameObject[] buttons;
    GameObject[] marks;
    public GameObject show;
    public GameObject markers;
    [SerializeField] GameObject currLocation;

    public GameObject[] linemakers;

    public int players;
    public int i = 0;
    public int[] plscores = new int[4];

    public GameObject[] texts;
    public GameObject[] resTexts;   
    List<GameObject> destroyLines = new List<GameObject>();

    public float delay;

    [SerializeField] bool editMode = false; 
    bool gate = true;

    public Color[] colors;

    void Start()
    {
        //initialize marks array
        marks = new GameObject[markerPrefs.Length];
        for (int i = 0; i < markerPrefs.Length; i++)
        {
            marks[i] = null;
        }

        //initialize colors
        colors = new Color[4];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        colors[3] = Color.magenta;

        //select random location from available ones
        //int index = Random.Range(0, locations.Length);
        //Debug.Log("Selected location: " + locations[index].name);
        //currLocation = locations[index];
    }

    public void ChooseLocation()
    {
        currLocation = locations[location];
        i=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && editMode)
        {
            StartCoroutine(Delay());
            gate = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && editMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print(ray);
            print(gate);

            if (gate)
            {
                if(marks[i] == null)
                {
                    marks[i] = Instantiate(markerPrefs[i], ray.origin, Quaternion.identity);
                    marks[i].transform.parent = markers.transform;
                }
                marks[i].transform.position = ray.origin;
                gate = true;
            }

            gate = true;
        }
    }

    public void Block(){
        editMode = false;
    }

    public void Unblock(){
        editMode = true;
    }

    public void Confirm(){
        Debug.Log("Confirm button pressed");
        if (marks[i] == null) return;
        if (i >= players - 1)
        {
             show.SetActive(true);
             buttons[i].SetActive(false);
             editMode = false;
        }
        else 
        {
            i++;
            editMode = true;
            buttons[i].SetActive(true);
            buttons[i-1].SetActive(false);
        }
    }

    public void ShowLocation(){
        currLocation.SetActive(true);
        float smallest= 1000;
        int index = 0;
        //calculate distance between each marker and the location
        float[] distances = new float[players];
        for (int i = 0; i < players; i++)
        {
            if (marks[i] != null)
            {
                distances[i] = Vector3.Distance(marks[i].transform.position, currLocation.transform.position); //add scale to turn into meters
                // Scoring system
                float maxDistance = 5f; // Define the maximum distance for scoring
                float normalizedDistance = Mathf.Clamp01(distances[i] / maxDistance); // Normalize the distance between 0 and 1
                int maxPoints = 100; // Define the maximum points for scoring
                plscores[i] += Mathf.RoundToInt(maxPoints * (1 - normalizedDistance)); // Calculate the points based on the normalized distance
                texts[i].GetComponent<textt>().amount = plscores[i];
                resTexts[i].GetComponent<textt>().amount = plscores[i];

                Debug.Log("Distance between marker " + i + " and location is " + distances[i]);
            }   
        }
        InstantiateLines(players);
    }

    public void InstantiateLines(int amount)
    {
        for (int i = 1; i <= amount;i++)
        {
            GameObject line = Instantiate(linemakers[i-1],new Vector3(0,0,0),Quaternion.identity);
            destroyLines.Add(line);
            line.GetComponent<LineRenderer>().startColor = colors[i-1];
            line.GetComponent<LineRenderer>().endColor = colors[i-1];
            line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(marks[i-1].transform.position.x, marks[i-1].transform.position.y, 0));
            line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(currLocation.transform.position.x, currLocation.transform.position.y, 0));
        }
    }

    public void HideLocationAndDestroyMarkers(){
        currLocation.SetActive(false);
        editMode = true;
        for (int i = 0; i < players; i++)
        {
            if (marks[i] != null)
            {
                print(marks[i]);
                Destroy(marks[i]);
                marks[i] = null;
                i=0;
            }
        }

        for (int i = 0; i<destroyLines.Count;i++)
        {
            Destroy(destroyLines[i]);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        gate = false;
    }
}