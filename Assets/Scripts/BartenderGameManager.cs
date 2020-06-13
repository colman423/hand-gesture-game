using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BartenderGameManager : MonoBehaviour
{
    // [SerializeField]
    // float fadeSpeed = 5;

    // Image spookeyImageHolder;

    // RectTransform spookyRectTransform;

    // [SerializeField]
    // Sprite openHandSprite;
    // [SerializeField]
    // Sprite closedHandSprite;

    public GameObject beerPrefab;
    public GameObject currentBeer;
    public GameObject[] beerList;
    public GameObject[] laneList;
    public GameObject[] customerList;

    // public 


    public GameObject gameCanvas;

    public ManoEvents manoEvents;

    public int laneStartPosition;

    // Use this for initialization
    void Start()
    {
        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;


    }

    private void Update()
    {
        // randomSpawn

        DEV_CreateBeerFromKeyboard();
    }


    void HandleManoMotionFrameUpdated()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        TrackingInfo tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        // AssignSpookeyFace(gesture, warning);
        // MoveAndScaleSpookey(tracking, warning);
        // HighlightSpookeyImage(warning);


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
        GameObject chess = Instantiate(beerPrefab, initPosition, Quaternion.identity);
        chess.transform.SetParent(gameCanvas.transform, false);
        return chess;
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
        showMsg("releaseCurrentBeer");
        int nearestLaneIdx = -1;
        Vector3 currentBeerPos = currentBeer.transform.localPosition;
        for (int i = 0; i < laneList.Length; i++)
        {
            if (Mathf.Abs(laneList[i].transform.localPosition.y - currentBeerPos.y) < 80)
            {
                nearestLaneIdx = i;
                break;
            }
        }
        showMsg("releaseCurrentBeer " + nearestLaneIdx);

        if (nearestLaneIdx != -1)
        {
            currentBeer.transform.localPosition = getLaneStartPosition(laneList[nearestLaneIdx]);
            currentBeer.GetComponent<BeerInstance>().setSliding(true);
            currentBeer = null;
        }
        else
        {
            GameObject.Destroy(currentBeer);
        }
    }

    Vector3 getLaneStartPosition(GameObject lane)
    {
        Vector3 lanePosition = lane.transform.localPosition;
        return new Vector3(laneStartPosition, lanePosition.y, 0);
    }


    void showMsg(string msg)
    {
        manoEvents.DisplayLogMessage(msg);
    }

    void DEV_CreateBeerFromKeyboard()
    {
        Debug.Log(laneList[0].transform.localPosition);
        Debug.Log(laneList[1].transform.localPosition);
        Debug.Log(laneList[2].transform.localPosition);
        if (Input.GetKey(KeyCode.Q))
        {
            GameObject beer = GrabNewBeer(getLaneStartPosition(laneList[0]));
            beer.GetComponent<BeerInstance>().setSliding(true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            GameObject beer = GrabNewBeer(getLaneStartPosition(laneList[1]));
            beer.GetComponent<BeerInstance>().setSliding(true);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            GameObject beer = GrabNewBeer(getLaneStartPosition(laneList[2]));
            beer.GetComponent<BeerInstance>().setSliding(true);
        }
    }



}
