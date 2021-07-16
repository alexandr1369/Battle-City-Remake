using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlayerInventory : MonoBehaviour
{
    #region Singleton

    public static PlayerInventory Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    #endregion

    [Header("Inventory Data")]
    [SerializeField] private PlayerSkin skinType = PlayerSkin.Skin1;
    [SerializeField] private List<GameObject> playerSkins;

    [Header("UI Settings")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private List<Image> playerSkinsImages;
    [SerializeField] private Text playerNameText;

    private void Start()
    {
        if (Application.isPlaying)
        {
            HideInventoryUI();
        }
    }
    private void Update()
    {
#if UNITY_EDITOR

        SelectPlayerSkinImage();

#endif
    }

    public GameObject GetCurrentSkinPlayer() => playerSkins[(int)skinType];
    private void SelectPlayerSkinImage()
    {
        string skinName = skinType.ToString();
        playerSkinsImages.ForEach(t => t.gameObject.SetActive(false));
        playerSkinsImages[(int)skinType].gameObject.SetActive(true);
        playerNameText.text = "Player " + skinName[skinName.Length - 1];
    }
    private void HideInventoryUI()
    {
        inventoryPanel.SetActive(false);
    }
}