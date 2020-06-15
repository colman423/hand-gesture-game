using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BartenderGameManager : MonoBehaviour
{
    public static BartenderGameManager Instance;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }


    public GameObject beerPrefab;
    public GameObject currentBeer;
    // public GameObject[] beerList;
    // public GameObject[] customerList;


    public GameObject gameCanvas;

    public ManoEvents manoEvents;


    public int score = 0;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;
        updateScore(0);
    }

    private void Update()
    {
        CustomerSpawner.Instance.onFrameUpdate();

        DEV_CreateBeerFromKeyboard();
    }
    void HandleManoMotionFrameUpdated()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        TrackingInfo tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        if (warning != Warning.WARNING_HAND_NOT_FOUND)
        {
            ManoGestureTrigger triggerGesture = gesture.mano_gesture_trigger;
            if (triggerGesture == ManoGestureTrigger.PICK)
            {
                showMsg("PICK");
                currentBeer = GrabNewBeer(ManoUtils.Instance.CalculateScreenPosition(tracking.poi, tracking.depth_estimation));
            }

            if (triggerGesture == ManoGestureTrigger.DROP)
            {
                showMsg("DROP");
                releaseCurrentBeer();
            }

            if (currentBeer)
            {
                updateCurrentBeerPosition(ManoUtils.Instance.CalculateScreenPosition(tracking.poi, tracking.depth_estimation));
            }
        }
    }



    // try to grab new beer from fingers, need to grab in the area
    GameObject GrabNewBeer(Vector3 initPosition)
    {
        showMsg("GrabNewBeer");
        GameObject beer = Instantiate(beerPrefab, initPosition, Quaternion.identity);
        beer.transform.SetParent(gameCanvas.transform, false);
        return beer;
    }

    // update current beer position by finger position
    void updateCurrentBeerPosition(Vector3 position)
    {
        if (currentBeer)
        {
            currentBeer.transform.position = position;
        }
    }

    // when finger do drop gesture, release current beer
    void releaseCurrentBeer()
    {
        Vector3 currentBeerPos = currentBeer.transform.localPosition;
        int nearestLaneIdx = LaneUtils.Instance.getNearestLaneIndex(currentBeerPos);
        showMsg("releaseCurrentBeer " + nearestLaneIdx);

        if (nearestLaneIdx != -1)
        {
            currentBeer.transform.localPosition = LaneUtils.Instance.getLaneStartPosition(nearestLaneIdx);
            currentBeer.GetComponent<BeerInstance>().setSliding(true);
            currentBeer.GetComponent<BeerInstance>().onSlideOutMapAction += onBeerSlideOutMap;
            currentBeer = null;
        }
        else
        {
            GameObject.Destroy(currentBeer);
        }
    }


    public void onBeerSlideOutMap()
    {
        Debug.Log("onBeerSlideOutMap");
        updateScore(-20);
    }

    public void onCustomerTouchBeer(GameObject beer)
    {
        Debug.Log("onCustomerTouchBeer");
        updateScore(10);
        beer.GetComponent<BeerInstance>().onTouchCustomer();
    }


    public void onCustomerTouchFrontWall()
    {
        Debug.Log("onCustomerTouchFrontWall");
        updateScore(-20);
    }

    void updateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score;
        Debug.Log("updateScore");
        Debug.Log(score);
        Debug.Log(scoreText.text);
    }
    void showMsg(string msg)
    {
        manoEvents.DisplayLogMessage(msg);
    }

    void DEV_CreateBeerFromKeyboard()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentBeer = GrabNewBeer(Input.mousePosition);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            releaseCurrentBeer();
        }
        updateCurrentBeerPosition(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject beer = GrabNewBeer(LaneUtils.Instance.getLaneStartPosition(0));
            beer.GetComponent<BeerInstance>().setSliding(true);
            beer.GetComponent<BeerInstance>().onSlideOutMapAction += onBeerSlideOutMap;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject beer = GrabNewBeer(LaneUtils.Instance.getLaneStartPosition(1));
            beer.GetComponent<BeerInstance>().setSliding(true);
            beer.GetComponent<BeerInstance>().onSlideOutMapAction += onBeerSlideOutMap;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject beer = GrabNewBeer(LaneUtils.Instance.getLaneStartPosition(2));
            beer.GetComponent<BeerInstance>().setSliding(true);
            beer.GetComponent<BeerInstance>().onSlideOutMapAction += onBeerSlideOutMap;
        }
    }



}
