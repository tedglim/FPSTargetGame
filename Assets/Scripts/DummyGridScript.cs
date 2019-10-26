using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGridScript : DummyScript
{
    public class Point
    {
        public int xIndx;
        public int yIndx;
        public Transform position;

        public Point(int xIndx, int yIndx, Transform position)
        {
            this.xIndx = xIndx;
            this.yIndx = yIndx;
            this.position = position;
        }
    }
    [SerializeField]
    private List<Transform> gridPositions;
    [SerializeField]
    private int xDim;
    [SerializeField]
    private int yDim;
    private List<Point> points;
    private List<Point> pointOptions;
    [SerializeField]
    private float currSpeed;
    private Point currentPosition;
    private Point currentDestination;
    private bool isBegin;
    private bool isBeginCall;
    private bool isFirstRep;
    [SerializeField]
    private bool isMonte;
    [SerializeField]
    private bool isInvis;

    void OnEnable()
    {
        GameEventsScript.hitDummy.AddListener(turnInvis);
        points = new List<Point>();
        pointOptions = new List<Point>();
        foreach(Transform position in gridPositions)
        {
            for (int x = 0; x < xDim; x++)
            {
                for (int y = 0; y < yDim; y++)
                {
                    Point p = new Point(x, y, position);
                    points.Add(p);
                }
            }
        }
        isBegin = true;
        isBeginCall = true;
    }

    void Update()
    {
        if (isBegin)
        {
            FirstGridMove();
        } else {
            GridMove();
        }
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position.position, currSpeed);
    }

    private void FirstGridMove()
    {
        if(isBeginCall)
        {
            currentDestination = points[UnityEngine.Random.Range(0,points.Count)];
            isBeginCall = false;
        }
        if (Vector3.Distance(transform.position, currentDestination.position.position) < 0.5f)
        {
            currentPosition = currentDestination;
            isBegin = false;
            isFirstRep = true;
        }        
    }

    private void GridMove()
    {
        if(isFirstRep)
        {
            int x = currentPosition.xIndx;
            int y = currentPosition.yIndx;
            foreach(Point p in points)
            {
                if(y + 1 < yDim)
                {
                    if (p.xIndx == x && p.yIndx == (y+1))
                    {
                        pointOptions.Add(p);
                    }
                }
                if(x + 1 < xDim)
                {
                    if (p.xIndx == (x+1) && p.yIndx == y)
                    {
                        pointOptions.Add(p);
                    }
                }
                if(y > 0)
                {
                    if(p.xIndx == x && p.yIndx == (y-1))
                    {
                        pointOptions.Add(p);
                    }
                }
                if(x > 0)
                {
                    if(p.xIndx == (x-1) && p.yIndx == y)
                    {
                        pointOptions.Add(p);
                    }
                }
            }
            currentDestination = pointOptions[UnityEngine.Random.Range(0, pointOptions.Count)];
            isFirstRep = false;            
        }

        if (Vector3.Distance(transform.position, currentDestination.position.position) < 0.5f)
        {
            if(isMonte)
            {
                currentPosition = points[UnityEngine.Random.Range(0,points.Count)];
                transform.position = currentPosition.position.position;
            } else
            {
                currentPosition = currentDestination;
            }
            pointOptions.Clear();
            isFirstRep = true;
        }  
    }

    private void turnInvis(DummyHitData data)
    {
        if(isInvis)
        {
            StartCoroutine(tempInvis());
        }
    }

    IEnumerator tempInvis()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
