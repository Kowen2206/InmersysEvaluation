using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Multiline][SerializeField] private List<string> _lvl1Instructions, _lvl2Instructions;
    [SerializeField] private TextMeshProUGUI _instructionsTextMesh;
    [SerializeField] private GameObject  _instructionsContainer, _scanInstructions;
    [SerializeField] private Sprite _lvl1ScanImg, _lvl2ScanImg;
    [SerializeField] private Image _scanInstructionsImg;

    private List<string> _currentInstructions;
    private int instructionsIndex = 0;

    void Start()
    {
        LoadInstructions();
    }

    public void LoadInstructions()
    {
        if(GameManager.Instance.CurrentSection == GameSection.Lvl1)
        {
            _currentInstructions = _lvl1Instructions;
            _scanInstructionsImg.sprite = _lvl1ScanImg;
        }
        else
        {
            _currentInstructions = _lvl2Instructions;
            _scanInstructionsImg.sprite = _lvl2ScanImg;
        }
        _instructionsTextMesh.text = _currentInstructions[instructionsIndex];
    }

    public void ActivateScanInstructions(int lvl)
    {
        string lvlId = "Lvl" + lvl;
        if(lvlId != GameManager.Instance.CurrentSection.ToString()) return;
        
        _scanInstructions.SetActive(true);
    }

    public void DesActivateScanInstructions(int lvl)
    {
        string lvlId = "Lvl" + lvl;
        if(lvlId != GameManager.Instance.CurrentSection.ToString()) return;
        
        _scanInstructions.SetActive(false);
    }

    public void OnContinueInstructions()
    {
        instructionsIndex++;
        if(instructionsIndex == _currentInstructions.Count)
        {
            instructionsIndex = 0;
            _instructionsContainer.SetActive(false);
        }
        
        _instructionsTextMesh.text = _currentInstructions[instructionsIndex];
    }

    public void BackButton()
    {
        GameManager.Instance.LoadMainMenuScene();
    }
}
