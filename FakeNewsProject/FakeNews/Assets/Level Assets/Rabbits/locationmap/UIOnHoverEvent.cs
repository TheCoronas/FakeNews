using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class UIOnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Vector3 cachedScale;
    private Text inftext;
    private Vector3 textloc = new Vector3(1000f, 240f, 100f); 

    void Start()
    {
        inftext = GameObject.Find("infoPhys").GetComponent<Text>();
        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inftext.text = ""; 
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        GameObject LandInfo = transform.Find("info").gameObject;
        LandInfo.SetActive(true);
        LandInfo.transform.position = textloc;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inftext.text = "Hover to learn your geography"; 
        transform.localScale = cachedScale;
        transform.Find("info").gameObject.SetActive(false);
        inftext.transform.position = textloc;

    }
}