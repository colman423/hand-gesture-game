using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GomokuGameManager : MonoBehaviour
{
    // [SerializeField]
    // float fadeSpeed = 5;

    // Image spookeyImageHolder;

    // RectTransform spookyRectTransform;

    // [SerializeField]
    // Sprite openHandSprite;
    // [SerializeField]
    // Sprite closedHandSprite;

    public GameObject nowActiveChess;

    public GameObject chessPrefab;

    public GameObject chessCanvas;

    // Use this for initialization
    void Start()
    {
        // spookeyImageHolder = this.GetComponent<Image>();
        // spookeyImageHolder.preserveAspect = true;
        // spookyRectTransform = this.GetComponent<RectTransform>();
        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;

        // createChess(new Vector3(0, 0, 0));

    }


    /// <summary>
    /// Handles the information from the processed frame in order to use the gesture and tracking information to illustrate Spooky.
    /// </summary>
    void HandleManoMotionFrameUpdated()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        TrackingInfo tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        // AssignSpookeyFace(gesture, warning);
        // MoveAndScaleSpookey(tracking, warning);
        // HighlightSpookeyImage(warning);
        MoveChess(gesture, tracking, warning);


    }

    void MoveChess(GestureInfo gesture, TrackingInfo trackingInfo, Warning warning)
    {

        if (warning != Warning.WARNING_HAND_NOT_FOUND)
        {
            ManoGestureTrigger triggerGesture = gesture.mano_gesture_trigger;
            if (triggerGesture == ManoGestureTrigger.PICK)
            {
                nowActiveChess = createChess(ManoUtils.Instance.CalculateScreenPosition(trackingInfo.poi, trackingInfo.depth_estimation));
            }

            if (triggerGesture == ManoGestureTrigger.DROP)
            {
                // Destroy(nowActiveChess);
				nowActiveChess = null;
            }

            if (nowActiveChess)
            {
                nowActiveChess.transform.position = ManoUtils.Instance.CalculateScreenPosition(trackingInfo.poi, trackingInfo.depth_estimation);
            }

        }
    }

    GameObject createChess(Vector3 initPosition)
    {
        GameObject chess = Instantiate(chessPrefab, initPosition, Quaternion.identity);
        chess.transform.SetParent(chessCanvas.transform, false);
        return chess;
    }

    // /// <summary>
    // /// Places the visualization of the ghost into the center of the bounding box and updates its position based on the detection.
    // ///  The scale of the image will depend based on the width or height (depends which one is bigger) of the bounding box. The Image is set to preserve aspect in order to prevent any disform
    // /// </summary>
    // /// <param name="trackingInfo">Tracking info.</param>
    // /// <param name="warning">Warning.</param>
    // void MoveAndScaleSpookey(TrackingInfo trackingInfo, Warning warning)
    // {
    //     if (warning != Warning.WARNING_HAND_NOT_FOUND)
    //     {
    //         // spookyRectTransform.position = Camera.main.ViewportToScreenPoint(trackingInfo.palm_center);
    //         spookyRectTransform.position = ManoUtils.Instance.CalculateScreenPosition(trackingInfo.poi, trackingInfo.depth_estimation);



    //         float width = Screen.width * trackingInfo.bounding_box.width;
    //         float height = Screen.height * trackingInfo.bounding_box.height;

    //         float size = Mathf.Min(width, height);
    //         spookyRectTransform.sizeDelta = new Vector2(size, size);

    //     }
    // }



}
