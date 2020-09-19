using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RoomCategory = Enums.RoomCategory;
using RoomNormalSubCategory = Enums.RoomNormalSubCategory;
using RoomBossSubCategory = Enums.RoomBossSubCategory;
using RoomSpecialSubCategory = Enums.RoomSpecialSubCategory;
using CategoryType = Enums.CategoryType;
[RequireComponent(typeof(HideableObject))]
public class RoomPropertiesMenu : MonoBehaviour
{
    public static RoomPropertiesMenu Instance;
    private Dictionary<string, ChamberButton> chamberButtons;
    private bool dirty;
    private Button applyButton, backButton;
    public ToggleButton shuffleReinforcementPositionsButton, floorDecoButton, wallDecoButton, lightingButton;
    public Dropdown categoryDropdown,
        normalDropdown,
        specialDropdown,
        bossDropdown;
    public InputField weightField;
    public Transform secretDropdownDummy;
    public List<Transform> subCategoryDropdowns;
    private HideableObject m_hideable;

    private void Awake()
    {
        m_hideable = GetComponent<HideableObject>();
        m_hideable.OnShow += Show;
        applyButton = GetChild<Button>("Apply Button");
        backButton = GetChild<Button>("Back Button");
        var buttonContainer = transform.Find("Chambers Panel").Find("Chamber Button Container");
        chamberButtons = new Dictionary<string, ChamberButton>();
        foreach (var button in buttonContainer.GetComponentsInChildren<ChamberButton>())
        {
            chamberButtons.Add(button.tileset.ToString(), button);
        }
        SetupCategoryDropdowns();
    }

    void Start()
    {
        RoomPropertiesMenu.Instance = this;
        this.gameObject.SetActive(false);
        this.applyButton.interactable = false;
    }

    public void OnCategoryChanged()
    {
        RoomCategory category = Enums.GetEnumValue<RoomCategory>(categoryDropdown.options[categoryDropdown.value].text);
        switch (category)
        {
            case RoomCategory.SPECIAL:
                SetActiveSubcatDropdown(specialDropdown.transform);
                break;
            case RoomCategory.BOSS:
                SetActiveSubcatDropdown(bossDropdown.transform);
                break;
            case RoomCategory.SECRET:
                SetActiveSubcatDropdown(secretDropdownDummy);
                break;
            default:
                SetActiveSubcatDropdown(normalDropdown.transform);
                break;
        }
    }

    private void SetActiveSubcatDropdown(Transform dropdown)
    {
        foreach (var scd in subCategoryDropdowns)
        {
            scd.gameObject.SetActive(scd == dropdown.transform);
        }
    }

    public void OnValueChanged()
    {
        this.dirty = true;
        this.applyButton.interactable = true;
    }

    public void OnBackClicked()
    {
        m_hideable.Hide();
    }

    public void OnApplyClicked()
    {
        this.applyButton.interactable = false;
        RoomProperties properties = Manager.Instance.roomProperties;
        foreach (var button in chamberButtons.Values)
        {
            properties.validTilesets[button.tileset.ToString()] = button.Toggled;
        }
        properties.category = GetCategory<RoomCategory>(categoryDropdown);
        properties.normalSubCategory = GetCategory<RoomNormalSubCategory>(normalDropdown);
        properties.specialSubCategory = GetCategory<RoomSpecialSubCategory>(specialDropdown);
        properties.bossSubCategory = GetCategory<RoomBossSubCategory>(bossDropdown);
        properties.weight = float.Parse(weightField.text);
        properties.shuffleReinforcementPositions = shuffleReinforcementPositionsButton.Toggled;
        properties.doFloorDecoration = floorDecoButton.Toggled;
        properties.doWallDecoration = wallDecoButton.Toggled;
        properties.doLighting = lightingButton.Toggled;
    }

    public void OnWeightChanged()
    {
        float val;
        bool success = float.TryParse(weightField.text, out val);
        if (!success)
            weightField.text = "1";
    }

    private T GetCategory<T>(Dropdown dropdown) where T : Enum
    {
        string val = dropdown.options[dropdown.value].text;
        return Enums.GetEnumValue<T>(val);
    }


    T GetChild<T>(string name)
    {
        return this.transform.Find(name).GetComponent<T>();
    }

    public void Show()
    {
        if (!Manager.Instance || !Manager.Instance.LateStartCompleted) return;
        RoomProperties properties = Manager.Instance.roomProperties;
        foreach (var tileset in properties.validTilesets)
        {
            if (chamberButtons.ContainsKey(tileset.Key))
            {
                chamberButtons[tileset.Key].Toggled = tileset.Value;
            }
        }

        weightField.text = properties.weight.ToString();
        shuffleReinforcementPositionsButton.Toggled = properties.shuffleReinforcementPositions;
        floorDecoButton.Toggled = properties.doFloorDecoration;
        wallDecoButton.Toggled = properties.doWallDecoration;
        lightingButton.Toggled = properties.doLighting;
        InitializeDropdowns();
    }

    public void InitializeDropdowns()
    {
        var props = Manager.Instance.roomProperties;
        categoryDropdown.value = (int)props.category;
        categoryDropdown.RefreshShownValue();

        normalDropdown.value = (int)props.normalSubCategory;
        normalDropdown.RefreshShownValue();

        specialDropdown.value = (int)props.specialSubCategory;
        specialDropdown.RefreshShownValue();

        bossDropdown.value = (int)props.bossSubCategory;
        bossDropdown.RefreshShownValue();
    }

    private void SetupCategoryDropdowns()
    {
        subCategoryDropdowns = new List<Transform>();
        SetupDropdown<RoomCategory>(categoryDropdown);
        SetupDropdown<RoomNormalSubCategory>(normalDropdown);
        SetupDropdown<RoomSpecialSubCategory>(specialDropdown);
        SetupDropdown<RoomBossSubCategory>(bossDropdown);

        subCategoryDropdowns.Add(normalDropdown.transform);
        subCategoryDropdowns.Add(specialDropdown.transform);
        subCategoryDropdowns.Add(bossDropdown.transform);
        subCategoryDropdowns.Add(secretDropdownDummy);
    }


    private void SetupDropdown<T>(Dropdown dropdown) where T : Enum
    {
        List<string> options = new List<string>();
        foreach (var e in Enum.GetValues(typeof(T)))
        {
            options.Add(e.ToString());
        }
        dropdown.AddOptions(options);
    }

}
