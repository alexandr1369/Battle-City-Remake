using System.Collections;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [Header("Render Settings")]
    [SerializeField] private Vector2 topLeftCornerPosition;
    [SerializeField] float stepValue;

    private void Awake()
    {
        LoadSceneData();
    }

    private void LoadSceneData()
    {
        #region Scene Settings

        int matrixLength = 28;
        string locationPattern =
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
            "B**************************B" +
            "BE***********************E*B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bbTTbb**bb**bb**B" +
            "B**bb**bb**bbTTbb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**********bb**bb**B" +
            "B**bb**bb**********bb**bb**B" +
            "B**********bb**bb**********B" +
            "B**********bb**bb**********B" +
            "Bbb**bbbb**********bbbb**bbB" +
            "BTT**bbbb**********bbbb**TTB" +
            "B**********bb**bb**********B" +
            "B**********bbbbbb**********B" +
            "B**bb**bb**bbbbbb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**bb**bb**bb**bb**B" +
            "B**bb**bb**********bb**bb**B" +
            "B**bb**bb**********bb**bb**B" +
            "B**bb**bb***bbbb***bb**bb**B" +
            "B***********b**b***********B" +
            "B********P**b**b***********B" +
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBB";

        #endregion

        for (int i = 0; i < matrixLength; i++)
        {
            for (int j = 0; j < matrixLength; j++)
            {
                BlockType blockType;
                GameObject locationPart;
                switch (locationPattern[i * matrixLength + j])
                {
                    case 'P': blockType = BlockType.Player; break;
                    case 'E': blockType = BlockType.Enemy; break;
                    case 'b': blockType = BlockType.Brick; break;
                    case 'B': blockType = BlockType.BrickTile; break;
                    case 'T': blockType = BlockType.Tile; break;
                    default: blockType = BlockType.None; break;
                }

                locationPart = BlockFabrick.Instance.GetBlock(blockType);
                if (locationPart)
                {
                    locationPart.transform.position = new Vector3(topLeftCornerPosition.x + j * stepValue, topLeftCornerPosition.y - i * stepValue);
                    if(blockType == BlockType.Player || blockType == BlockType.Enemy)
                    {
                        locationPart.transform.position += new Vector3(stepValue / 2, stepValue / 2);
                    }
                }
            }
        }
    }
}
