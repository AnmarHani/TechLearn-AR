using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ModelTargetController : MonoBehaviour
{
    public GameObject modelTargetRoot; // Assign the parent GameObject (children are under this)
    public Button hideButton;


    private bool isManuallyHidden = false;

    void Start()
    {
        if (hideButton != null)
            hideButton.onClick.AddListener(HideModel);

        // Register to Vuforia events
        var observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Initially hidden
        modelTargetRoot.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        // Only when it becomes tracked
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            modelTargetRoot.SetActive(true);
        }
    }

    public void HideModel()
    {
        modelTargetRoot.SetActive(false);
        isManuallyHidden = true;
    }
}
