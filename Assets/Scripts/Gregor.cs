using UnityEngine;
using System.Collections;

enum GrowthStage {
	BABY,
	TEEN,
	ADULT,
	NUM_STAGES
}

enum EvolChoice {
	def,
	strong,
	brains
}

public class Gregor : CharacterScript {
	float healthPoints;
	float attackPower;
	float armor;
	
	int m_defLevel;
	int m_strongLevel;
	int m_brainsLevel;
	
	int m_xLocation;
	int m_yLocation;
	GrowthStage m_growth;
	int m_xp;
	int m_keyCount;
	int m_levelThreshold;
	int m_level;
	public bool returning;
	ArrayList evolChoices;
	
	
	private static Gregor _instance;
    public static Gregor shared { get{ return _instance; } }
	public MapRoom currentRoom { get{ return MapManager.shared.MapRoomAtAddress(m_xLocation, m_yLocation); } }
	public int xLocation { get{ return m_xLocation; } }
	public int yLocation { get{ return m_yLocation; } }
	public bool hasKeys { get{ return (m_keyCount > 0); } }
	public int growthStage { get{ return (int)m_growth; } }
	public int defLevel { get{ return m_defLevel; } }
	public int strongLevel { get{ return m_strongLevel; } }
	public int brainsLevel { get{ return m_brainsLevel; } }
	
	
	// Use this for initialization
	void Awake () {
		if (_instance != null) {
			_instance.returning = true;
			return;
		}
        _instance = this;
        DontDestroyOnLoad(transform.gameObject);
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GregorEvol1"));
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GregorEvol2"));
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GregorEvol3"));
		m_xLocation = 0;
		m_yLocation = 0;
		m_defLevel = 0;
		m_strongLevel = 0;
		m_brainsLevel = 0;
		m_growth = GrowthStage.BABY;
		m_keyCount = 0;
		m_levelThreshold = 10;
		m_level = 0;
		m_xp = 0;
		evolChoices = new ArrayList(1);
	}
	
	// Update is called once per frame
	void Update () {
		if (returning) {
			returning = false;
			updateOverlays();
			unHide();
		}
	}
	
	void hide() {
		GameObject.FindGameObjectWithTag("GregorEvol1").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("GregorEvol2").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("GregorEvol3").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
	}
	
	void unHide() {
		GameObject.FindGameObjectWithTag("GregorEvol1").transform.position = new Vector3(-7.0f, 0.0f, -2.0f);
		GameObject.FindGameObjectWithTag("GregorEvol2").transform.position = new Vector3(-7.0f, 0.0f, -2.0f);
		GameObject.FindGameObjectWithTag("GregorEvol3").transform.position = new Vector3(-7.0f, 0.0f, -2.0f);
		transform.position = new Vector3(-7.0f, 0.0f, -2.0f);
	}
	
	public void moveForward() {
		//Debug.LogWarning("moveForward called on Gregor");
		if (m_xLocation < MapManager.shared.mapWidth - 1)
			m_xLocation++;
		if (m_yLocation < MapManager.shared.mapHeight - 1)
			m_yLocation++;
		Debug.Log(string.Format("New Location {0} {1}", m_xLocation, m_yLocation));
		currentRoom.notify();
	}
	public void moveUp() {
		if (m_xLocation < MapManager.shared.mapWidth - 1)
			m_xLocation++;
		Debug.Log(string.Format("New Location {0} {1}", m_xLocation, m_yLocation));
		currentRoom.notify();
	}
	public void moveDown() {
		if (m_yLocation < MapManager.shared.mapHeight - 1)
			m_yLocation++;
		Debug.Log(string.Format("New Location {0} {1}", m_xLocation, m_yLocation));
		currentRoom.notify();
	}
	
	public void runAway() {
		// go backwards 1-5 squares
		m_xLocation--;
		m_xLocation -= (int)(Random.value * 5.0f) % 5;
		m_yLocation--;
		m_yLocation -= (int)(Random.value * 5.0f) % 5;
		if (m_xLocation < 0) m_xLocation = 0;
		if (m_yLocation < 0) m_yLocation = 0;
		Debug.Log(string.Format("New Location {0} {1}", m_xLocation, m_yLocation));
		currentRoom.notify();
	}
	
	public void giveKey() {
		m_keyCount++;
	}
	
	public bool spendKey() {
		if (m_keyCount > 0) {
			m_keyCount--;
			return true;
		}
		return false;
	}
	
	public void grantHealth(int hp) {
		healthPoints += hp;
	}
	public void grantDef(float def) {
		armor += def;
	}
	public void grantAP(int ap) {
		attackPower += ap;
	}
	
	public void grantXP(int xp) {
		m_xp += xp;
		//Debug.Log(string.Format("Adding {0} xp", xp));
		if (m_xp > m_levelThreshold) {
			//Debug.LogWarning("Leveling Up!");
			hide();
			m_level++;
			m_levelThreshold += 10 * m_level;
			Application.LoadLevel("evolveScene");
		}
	}
	
	public void evolveTrait(string traitTag) {
		Debug.Log (string.Format ("Sprite Tagged: {0}", string.Format("GregorEvol{0}", m_level)));
		
		if (traitTag == "DefSprite") {
			m_defLevel++;
			evolChoices.Add(EvolChoice.def);
		}
		if (traitTag == "StrongSprite") {
			m_strongLevel++;
			evolChoices.Add(EvolChoice.strong);
		}
		if (traitTag == "BrainsSprite") {
			m_brainsLevel++;
			evolChoices.Add(EvolChoice.brains);
		}
		
	}
	
	void updateOverlays() {
		int defCount = 1;
		int strongCount = 1;
		int brainsCount = 1;
		for (int i = 0; i < evolChoices.Count; i++) {
			OTSprite evol = OT.Sprite(string.Format("GregorEvol{0}", i+1));
			if ((EvolChoice)evolChoices[i] == EvolChoice.def) {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-def-{0}", defCount)));
				defCount++;
			}
			if ((EvolChoice)evolChoices[i] == EvolChoice.strong) {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-strong-{0}", strongCount)));
				strongCount++;
			}
			if ((EvolChoice)evolChoices[i] == EvolChoice.brains) {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-brains-{0}", brainsCount)));
				brainsCount++;
			}
		}
	}
}
