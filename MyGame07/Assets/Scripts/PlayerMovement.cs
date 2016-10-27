using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private float speed = 2f;
    private float speedUpDown = 4f;
    private NewBoxController newBoxController;
    public int count = 0;
    private int direction;
    private Vector3 newPosition;
    private Vector3 newPositionMap = Vector3.up;
    private int index;
    private bool IsPositionJudge = false;
    private bool IsMoveUp = false;
    private bool IsGetMapPosition = false;
    private GameObject MapCube = null;
    private bool IsMoveOver = true;
    //private StopButton stopButton;

    void Awake()
    {
        newBoxController = GameObject.FindGameObjectWithTag("GameController").GetComponent<NewBoxController>();
        //stopButton = GameObject.FindGameObjectWithTag("GameController").GetComponent<StopButton>();
    }

	void Update () {
        if (count == 0)
        {
            direction = newBoxController.list[count];
            count++;
        }
        if (transform.position == newPosition && index != 9)
        {
            direction = newBoxController.list[count];
            count++;
        }

        switch (direction)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                newPosition = transform.position + Vector3.right;
                direction = 0;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                newPosition = transform.position + Vector3.forward;
                direction = 0;
                break;
            case 9:
            case 10:
                IsMoveOver = true;
                if (newBoxController.list[count - 1] > 5)
                {
                    newPosition = transform.position + Vector3.right;
                }
                else
                {
                    newPosition = transform.position + Vector3.forward;
                }
                direction = 0;
                break;
            default:
                break;
        }

        if (Input.GetMouseButtonDown(0) && !StopButton.isPointEnter && !IsMoveUp && IsMoveOver)
        {
            index = newBoxController.list[count - 1];
        }


        if (PositionJudge())
        {
            speed = 4f;
            newPosition = transform.position + Vector3.up;
            IsMoveUp = true;
        }
        else if (index != 9 && index != 0 && index != 10 && IsMoveOver)
        {
            GameObject mCube;
            string mCubeName;
            IsMoveUp = true;
            IsMoveOver = false;
            index = 0;
            for (int i = 1; i <= newBoxController.list.Count - count + 1; i++)
            {
                mCubeName = newBoxController.highCubeName + (count + i).ToString();
                mCube = GameObject.Find(mCubeName);
                if (mCube.tag == "RedCube")
                {
                    newPositionMap = mCube.transform.position + Vector3.up;
                    MapCube = mCube;
                    IsGetMapPosition = true;
                    break;
                }
                else if (mCube.tag == "BlackCube")
                {
                    newPositionMap = mCube.transform.position + Vector3.down;
                    MapCube = mCube;
                    IsGetMapPosition = true;
                    break;
                }
            }
        }

        if (IsPositionJudge)
        {
            string mCubeName = newBoxController.highCubeName + (count + 1).ToString();
            GameObject mCube = GameObject.Find(mCubeName);
            mCube.transform.position = Vector3.MoveTowards(mCube.transform.position, mCube.transform.position + Vector3.up, speed * Time.deltaTime);
        }

        if (IsGetMapPosition && newPositionMap != Vector3.up)
        {
            MapCube.transform.position = Vector3.MoveTowards(MapCube.transform.position, newPositionMap, speedUpDown * Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if (MapCube != null)
        {
            if (Vector3.Distance(MapCube.transform.position, newPositionMap) < 0.02f && MapCube.transform.position != newPositionMap)
            {
                MapCube.transform.position = newPositionMap;
                newPositionMap = Vector3.up;
                MapCube = null;
                IsGetMapPosition = false;
                IsMoveUp = false;
            }
        }

        if (Vector3.Distance(transform.position, newPosition) < 0.02f)
        {
            transform.position = newPosition;
            speed = 2f;
            IsPositionJudge = false;
            IsMoveUp = false;
        }
	}

    void OnDisable()
    {
        count = 0;
        IsPositionJudge = false;
        IsMoveUp = false;
        IsGetMapPosition = false;
        MapCube = null;
        IsMoveOver = true;
        transform.position = Vector3.up;
}

    private bool PositionJudge()
    {
        if (transform.position == newPosition && index == 9)
        {
            IsPositionJudge = true;
            index = 0;
            return true;
        }
        return false;
    }

    public int getCount()
    {
        return count;
    }
}
