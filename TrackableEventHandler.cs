using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    public bool isTracked = false;
    public GameObject aug_Model, model_3;

    protected virtual void Awake()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        aug_Model.SetActive(false);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            trackedBehaviour();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            trackLostBehaviour();
        }
        else
        {
            trackLostBehaviour();
        }
    }

    private void trackedBehaviour()
    {
        isTracked = true;

        if (!isBothTargetsTracked())
            aug_Model.SetActive(true);
        else
            model_3.SetActive(true);
    }

    private void trackLostBehaviour()
    {
        isTracked = false;
        aug_Model.SetActive(false);
        model_3.SetActive(false);
    }

    private bool isBothTargetsTracked()
    {
        bool value = true;

        foreach (TrackableBehaviour m in FindObjectsOfType<TrackableBehaviour>())
        {
            if (!m)
                value = false;
        }
        return value;
    }
}
