using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameManager gameManager;

    public float DEBUG_InvokeTiming = 5.0f;

    public GameObject reportingMenu;
    public Button reportMenuButton;
    public Button reportButton;

    public Button bRoomA, bRoomB, bRoomC, bImagen, bDesplazamiento, bDuplicado, bElectrico, bIntruso, bParanormal;

    private string selectedRoom = "";
    private string selectedAnomaly = "";

    public TextMeshProUGUI anomaliesNumberLabel;
    public GameObject panelAnomalyRemoved;
    public GameObject anomalyRemovedLabel, anomalyNotFoundLabel;

    public GameObject panelGameOver;
    public TextMeshProUGUI anomaliesFoundLabel;

    public GameObject panelGanaste;
    public TextMeshProUGUI anomaliesFoundLabel2;

    public TextMeshProUGUI currentRoomNameLabel;

    public GameObject[] imagenesNotFound;

    private void Start()
    {
        string[] roomNames = gameManager.GetRoomNames();
        // foreach(string r in roomNames) { Debug.LogWarning("[DEBUG:GUIMANAGER::START()] " + r); }

        // set buttons' text to the room names
        TMP_Text roomA_name = bRoomA.GetComponentInChildren<TMP_Text>();
        roomA_name.text = roomNames[0];

        TMP_Text roomB_name = bRoomB.GetComponentInChildren<TMP_Text>();
        roomB_name.text = roomNames[1];

        TMP_Text roomC_name = bRoomC.GetComponentInChildren<TMP_Text>();
        roomC_name.text = roomNames[2];

        // TODO: create buttons dynamically based on room count
        bRoomA.onClick.AddListener(delegate () { OnRoomButtonClicked(bRoomA, roomNames[0]); });
        bRoomB.onClick.AddListener(delegate () { OnRoomButtonClicked(bRoomB, roomNames[1]); });
        bRoomC.onClick.AddListener(delegate () { OnRoomButtonClicked(bRoomC, roomNames[2]); });

        bImagen.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bImagen, "Imagen"); });
        bDesplazamiento.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bDesplazamiento, "Desplazamiento"); });
        bDuplicado.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bDuplicado, "Duplicado"); });
        bElectrico.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bElectrico, "Electrica"); });
        bIntruso.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bIntruso, "Intruso"); });
        bParanormal.onClick.AddListener(delegate () { OnAnomalyButtonClicked(bParanormal, "Paranormal"); });

        reportButton.onClick.AddListener(delegate () { Report(); });
    }

    public void ToogleReportingMenu()
    {
        if (reportingMenu.activeInHierarchy)
        {
            reportingMenu.SetActive(false);
        }
        else
        {
            ResetMenu();
        }
    }

    private void Update()
    {
        if ((!selectedAnomaly.Equals("") && !selectedRoom.Equals("")))
        {
            reportButton.interactable = true;
        }
    }

    private void ResetMenu()
    {
        reportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Report";   
        reportButton.interactable = false;
        selectedAnomaly = "";
        selectedRoom = "";
        bRoomA.interactable = true;
        bRoomB.interactable = true;
        bRoomC.interactable = true; 
        bImagen.interactable = true; 
        bDesplazamiento.interactable = true; 
        bDuplicado.interactable = true; 
        bElectrico.interactable = true; 
        bIntruso.interactable = true; 
        bParanormal.interactable = true;
        reportingMenu.SetActive(true);
        reportMenuButton.interactable = true;
    }

    private void OnRoomButtonClicked(Button b, string roomName)
    {
        // Debug.LogWarning("[DEBUG: GUIManager::OnRoomButtonClicked] roomName=" + roomName + " - Button = " + b.name);
        selectedRoom = roomName;
        bRoomA.interactable = true;
        bRoomB.interactable = true;
        bRoomC.interactable = true;
        b.interactable = false;
    }

    private void OnAnomalyButtonClicked(Button b, string anomalyName)
    {
        selectedAnomaly = anomalyName;
        bImagen.interactable = true;
        bDesplazamiento.interactable = true;
        bDuplicado.interactable = true;
        bElectrico.interactable = true;
        bIntruso.interactable = true;
        bParanormal.interactable = true;
        b.interactable = false;
    }

    public void Report()
    {
        bRoomA.interactable = false;
        bRoomB.interactable = false;
        bRoomC.interactable = false;
        bImagen.interactable = false;
        bDesplazamiento.interactable = false;
        bDuplicado.interactable = false;
        bElectrico.interactable = false;
        bIntruso.interactable = false;
        bParanormal.interactable = false;
        reportButton.GetComponentInChildren<TextMeshProUGUI>().text = "REPORTING...";
        reportMenuButton.interactable = false;
        Invoke("ReportToGameManager", DEBUG_InvokeTiming);
    }

    private void ReportToGameManager()
    {
        gameManager.ReportAnomaly(selectedRoom, selectedAnomaly);
    }

    public void UpdateAnomaliesNumber(int n)
    {
        anomaliesNumberLabel.text="Anomalies: "+n;
    }

    public void AnomalyRemoved()
    {
        ResetMenu();
        //reportingMenu.SetActive(false);
        Invoke("ShowAnomalyRemovedPanel", 0);
        Invoke("ShowAnomalyRemovedLabel", 1);
        Invoke("HideAnomalyRemovedPanel", 4);
        Invoke("HideAnomalyRemovedLabel", 4);
    }

    public void AnomalyNotFound()
    {
        ResetMenu();
        //reportingMenu.SetActive(false);
        Invoke("ShowAnomalyRemovedPanel", 0);
        Invoke("ShowAnomalyNotFoundLabel", 1);
        Invoke("HideAnomalyRemovedPanel", 4);
        Invoke("HideAnomalyNotFoundLabel", 4);

        foreach(GameObject g in imagenesNotFound)
        {
            g.SetActive(false);
        }
        imagenesNotFound[Random.Range(0, imagenesNotFound.Length)].SetActive(true);
    }

    private void ShowAnomalyRemovedPanel()
    {
        panelAnomalyRemoved.SetActive(true);
    }
    private void HideAnomalyRemovedPanel()
    {
        panelAnomalyRemoved.SetActive(false);
    }
    private void ShowAnomalyRemovedLabel()
    {
        anomalyRemovedLabel.SetActive(true);
    }
    private void HideAnomalyRemovedLabel()
    {
        anomalyRemovedLabel.SetActive(false);
    }
    private void ShowAnomalyNotFoundLabel()
    {
        anomalyNotFoundLabel.SetActive(true);
    }
    private void HideAnomalyNotFoundLabel()
    {
        anomalyNotFoundLabel.SetActive(false);
    }

    public void ShowGameOver(int aFound)
    {
        panelGameOver.SetActive(true);
        anomaliesFoundLabel.text = aFound + " ANOMALIES FOUND";
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateRoomLabel(string roomName)
    {
        currentRoomNameLabel.text = roomName;
    }

    public void YouWon()
    {
        gameManager.YouWon();
        panelGanaste.SetActive(true);
        anomaliesFoundLabel2.text = gameManager.GetAnomaliesFound() + " ANOMALIES FOUND";
    }

}
