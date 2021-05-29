using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int levelLength;
    [SerializeField] int startPlatformLength = 5;
    [SerializeField] int endPlatformLength = 5;
    [SerializeField] int distanceBetweenPlatforms;
    [SerializeField] Transform platformPrefab;
    [SerializeField] Transform platformParent;
    [SerializeField] Transform monsterPrefab;
    [SerializeField] Transform monsterParent;
    [SerializeField] Transform healthCollectable;
    [SerializeField] Transform healthCollectableParent;
    [SerializeField] float platformPositionMinY = 0f;
    [SerializeField] float platformPositionMaxY = 10f;
    [SerializeField] int platformLengthMin = 1;
    [SerializeField] int platformLengthMax = 4;
    [SerializeField] float chanceForMonsterExistence = 0.25f;
    [SerializeField] float chanceForCollectableExistence = 0.1f;
    [SerializeField] float healthCollectableMinY = 1f;
    [SerializeField] float healthCollectableMaxY = 3f;

    float platformLastPositionX;

    enum PlatformType { None, Flat }


	private void Start()
	{
        GenerateLevel();
	}

	class PlatformPositionInfo
	{
        public PlatformType platformType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

		public PlatformPositionInfo(PlatformType platformType, float posY, bool hasMonster, bool hasCollectable)
		{
            this.platformType = platformType;
            this.positionY = posY;
            this.hasMonster = hasMonster;
            this.hasHealthCollectable = hasCollectable;
        }
	}

    void FillOutPositionInfo(PlatformPositionInfo[] platformsInfo)
	{
        int currentPlatformInfoIndex = 0;
		for (int i = 0; i < startPlatformLength; i++)
		{
            platformsInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformsInfo[currentPlatformInfoIndex].positionY = 0;
            currentPlatformInfoIndex++;
		}

        while(currentPlatformInfoIndex < levelLength - endPlatformLength)
		{
            if(platformsInfo[currentPlatformInfoIndex - 1].platformType != PlatformType.None)
			{
                currentPlatformInfoIndex++;
                continue;
			}

            float platformPositionY = Random.Range(platformPositionMinY, platformPositionMaxY);
            int platformLength = Random.Range(platformLengthMin, platformLengthMax);

			for (int i = 0; i < platformLength; i++)
			{
                bool hasMonster = Random.Range(0f, 1f) < chanceForMonsterExistence;
                bool hasHealthCollectable = Random.Range(0f, 1f) < chanceForCollectableExistence;

                platformsInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformsInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformsInfo[currentPlatformInfoIndex].hasMonster = hasMonster;
                platformsInfo[currentPlatformInfoIndex].hasHealthCollectable = hasHealthCollectable;
                currentPlatformInfoIndex++;

                if(currentPlatformInfoIndex > levelLength - endPlatformLength)
				{
                    currentPlatformInfoIndex = levelLength - endPlatformLength;
                    break;
				}
            }
        }

        for (int i = 0; i < endPlatformLength; i++)
        {
            platformsInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformsInfo[currentPlatformInfoIndex].positionY = 0;
            currentPlatformInfoIndex++;
        }
    }

    void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformsInfo)
	{
		for (int i = 0; i < platformsInfo.Length; i++)
		{
            PlatformPositionInfo positionInfo = platformsInfo[i];
            
            if(positionInfo.platformType == PlatformType.None)
			{
                continue;
			}

            //check if game started
            Vector3 platformPosition = new Vector3(distanceBetweenPlatforms * i, positionInfo.positionY, 0);
            //save platform x position

            Transform createBlock = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            createBlock.parent = platformParent;

			if (positionInfo.hasMonster)
			{
                //create monster
            }

            if (positionInfo.hasHealthCollectable)
			{
                //create collectable
			}

        }
	}

    void GenerateLevel()
	{
        PlatformPositionInfo[] platformsInfo = new PlatformPositionInfo[levelLength];
		for (int i = 0; i < platformsInfo.Length; i++)
		{
            platformsInfo[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
		}

        FillOutPositionInfo(platformsInfo);
        CreatePlatformsFromPositionInfo(platformsInfo);
    }
}
