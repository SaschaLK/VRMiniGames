using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGeneration : MonoBehaviour {

	public static CaveGeneration instance;

	//Cave Elements
	public GameObject ground;
	public GameObject ceiling;
	public GameObject wall;
	public GameObject stonePrefab;
	public GameObject startStone;
	public GameObject startStoneDust;
	private GameObject caveHolder;

	//Array-Generation Variables
	public int caveRepetition;
	[Range(6, 20)]
	public int caveZLength;
	public float spacing;
	private float zOffset;
	private int caveZLengthOffset;

	//Stone Spawning Variables
	public float spawnTime;
	public float spawnSpeed;
	public float spawnStoppingSpeed;

	private bool oneSpawn;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		zOffset = -caveZLength;
		caveZLengthOffset = 0;
		caveHolder = new GameObject("Cave");
	}

	public void SpawnEmpty() {
		//Instantiate empty ground
		GameObject startGround = Instantiate(ground, Vector3.zero, Quaternion.identity, caveHolder.transform);
		startGround.transform.localScale = new Vector3(startGround.transform.localScale.x * caveZLength * spacing, startGround.transform.localScale.y, startGround.transform.localScale.z * caveZLength * spacing);
	}

	public void PlaceStartStone() {
		GameObject temp = Instantiate(startStone, new Vector3(startStone.transform.position.x, -2, startStone.transform.position.z), Quaternion.identity, caveHolder.transform);
		StartCoroutine(SpawnStartStone(temp));
        startStoneDust.transform.position = startStone.transform.position;
		startStoneDust.SetActive(true);
	}

	IEnumerator SpawnStartStone(GameObject temp) {
		for (float i = 0; i < spawnTime; i += spawnStoppingSpeed) {
			temp.transform.position = new Vector3(temp.transform.position.x, spawnSpeed + temp.transform.position.y, startStone.transform.position.z);
			temp.GetComponent<AudioSource>().enabled = true;
			yield return new WaitForSeconds(spawnSpeed);
		}
		startStoneDust.SetActive(false);
	}

    private void SpawnStone(int i, int j, float zOffset, StoneBehaviour.StoneType stoneType) {
        GameObject temp = Instantiate(stonePrefab, new Vector3(j * spacing, -2, i * spacing + (spacing * zOffset)), new Quaternion(0, Random.Range(0, 360), 0, Random.Range(0, 360)), caveHolder.transform);
        float tempScale = Random.Range(0.8f, 1.2f);
        temp.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
        temp.GetComponentInChildren<StoneBehaviour>().SetStoneType(stoneType);
        StartCoroutine(SpawnRegularStone(temp));
    }

    IEnumerator SpawnRegularStone(GameObject temp) {
        for (float j = 0; j < spawnTime; j += spawnStoppingSpeed) {
	    	temp.transform.position = new Vector3(temp.transform.position.x, spawnSpeed + temp.transform.position.y, temp.transform.position.z);
    		temp.GetComponent<AudioSource>().enabled = true;
		    yield return new WaitForSeconds(spawnSpeed);
        }
	}

	public void BuildCave() {
		if (!oneSpawn) {
			for (int r = 0; r < caveRepetition; r++) {
				zOffset = zOffset + caveZLength;

				//Instantiate Ground and Ceiling
				if (r != 0) {
					SpawnGround(r);
				}
				SpawnCeiling(r);

				//last Ground and Ceiling
				if (r == caveRepetition - 1) {
					SpawnGround(r + 1);
					SpawnCeiling(r + 1);
				}

				//Build Grid for map
				bool[][] grid = new bool[caveZLength + caveZLengthOffset][];
				for (int i = 0; i < grid.Length; i++) {
					grid[i] = new bool[(int)(caveZLength / 2) + 1];
				}

				//Fill Grid
				for (int i = 0; i < grid.Length; i++) {
					for (int j = 0; j < grid[i].Length; j++) {
						//Filling first half
						if (i <= (int)grid.Length / 2) {
							if (i >= j) {
								grid[i][j] = true;
							}
							else {
								grid[i][j] = false;
							}
							//CaveGrid Walls
							if (j == grid[i].Length - 1) {
								grid[i][j] = false;
							}
						}
						//Filling second half
						else {
							if (j + i < grid.Length) {
								grid[i][j] = true;
							}
							else {
								grid[i][j] = false;
							}
						}
					}
				}

				//Instantiate Stones
				for (int i = 0; i < grid.Length; i++) {
					for (int j = 0; j < grid[i].Length; j++) {
						if (grid[i][j] && i <= (int)grid.Length / 2) {
							if (i == (int)grid.Length / 2 || i == ((int)grid.Length / 2) - 1) {
								SpawnStone(i, j, zOffset, StoneBehaviour.StoneType.lcStone);
								if (j != 0) {
									SpawnStone(i, -j, zOffset, StoneBehaviour.StoneType.rcStone);
								}
							}
							else {
								SpawnStone(i, j, zOffset, StoneBehaviour.StoneType.lcrStone);
								if (j != 0) {
									SpawnStone(i, -j, zOffset, StoneBehaviour.StoneType.lcrStone);
								}
							}
						}
						else if (grid[i][j] && i > (int)grid.Length / 2) {
							SpawnStone(i, j, zOffset, StoneBehaviour.StoneType.lcStone);
							if (j != 0) {
								SpawnStone(i, -j, zOffset, StoneBehaviour.StoneType.rcStone);
							}
						}
						else if (!grid[i][j] && (grid[i][j - 1] || grid[i][j - 2])) {
							SpawnWall(i, j, zOffset);
							SpawnWall(i, -j, zOffset);
						}
					}
				}
			}
			oneSpawn = true;
		}
	}

	private void SpawnWall(int i, int j, float zOffset) {
		GameObject temp = Instantiate(wall, new Vector3(j * spacing, wall.transform.localPosition.y, i * spacing + (spacing * zOffset)), wall.transform.rotation, caveHolder.transform);
	}

	private void SpawnGround(int r) {
		GameObject temp = Instantiate(ground, new Vector3(0, 0, r * caveZLength * spacing), Quaternion.identity, caveHolder.transform);
		temp.transform.localScale = new Vector3(temp.transform.localScale.x * caveZLength * spacing, temp.transform.localScale.y, temp.transform.localScale.z * caveZLength * spacing);
	}

	private void SpawnCeiling(int r) {
		GameObject temp = Instantiate(ceiling, new Vector3(0, ceiling.transform.localPosition.y, r * caveZLength * spacing), ceiling.transform.rotation, caveHolder.transform);
		temp.transform.localScale = new Vector3(temp.transform.localScale.x * caveZLength * spacing, temp.transform.localScale.y, temp.transform.localScale.z * caveZLength * spacing);
	}
}
