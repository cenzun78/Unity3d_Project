using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBoxController : MonoBehaviour {

    private GameObject newCube01;
    private const float newBoxTime = 4f;
    private float x = 0f, y = 0f, z = 0f;
    private const int cubeCount = 8;
    private const int FirstScreenCube = 12;
    private int range = 11;
    private int rangeNumber = 0;
    public List<int> list;
    public int AllCubeCount = 2;
    public string highCubeName = "Cube_01_";
    private int CubeSum = 0;
    private PlayerMovement playerMovement;

    void Awake()
    {
        newCube01 = GameObject.Find("Cube_01_1");
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        list = new List<int>();
    }

    void OnEnable()
    {
        newSomeBox(FirstScreenCube);
        CubeSum += 12;
    }

    void FixedUpdate () {
        if (CubeSum - playerMovement.getCount() == 4)
        {
            newSomeBox(cubeCount);
            CubeSum += 8;
        }
    }

    void OnDisable()
    {
        CubeSum = 0;
        AllCubeCount = 2;
        x = 0f;
        y = 0f;
        z = 0f;
        list.Clear();
    }

    private void newSomeBox(int count)
    {
        for (int i = 0; i < count; i++)
        {
            newBox();
        }
    }

    private void newBox()
    {
        int tempRangeNumber = 0;
        rangeNumber = Random.Range(1, range);
        list.Add(rangeNumber);
        switch (rangeNumber)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                x += 1;
                range = 11;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                z += 1;
                range = 11;
                break;
            default:
                break;
        }
        GameObject mCube;
        if (rangeNumber == 9 || rangeNumber == 10)
        {
            if (tempRangeNumber < 5)
            {
                x += 1;
            }
            else
            {
                z += 1;
            }

            if (rangeNumber == 9)
            {
                mCube = Instantiate(newCube01, new Vector3(x, y, z), Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;
                mCube.name = highCubeName + AllCubeCount.ToString();
                mCube.tag = "RedCube";
                AllCubeCount++;
                mCube.GetComponent<Renderer>().material.color = Color.red;
                y += 1;
                range = 9;
            }
            else if (rangeNumber == 10)
            {
                mCube = Instantiate(newCube01, new Vector3(x, y + 1, z), Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;
                mCube.name = highCubeName + AllCubeCount.ToString();
                mCube.tag = "BlackCube";
                AllCubeCount++;
                mCube.GetComponent<Renderer>().material.color = Color.black;
                range = 9;
            }
        }
        else
        {
            mCube = Instantiate(newCube01, new Vector3(x, y, z), Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;
            mCube.name = highCubeName + AllCubeCount.ToString();
            mCube.tag = "Cube";
            AllCubeCount++;
            tempRangeNumber = rangeNumber;
        }
    }
}
