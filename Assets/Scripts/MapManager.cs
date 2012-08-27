using UnityEngine;
using System.Collections;

public enum BaddyType {
	SLIME,
	GHOST,
	RODENT,
	DRAGON,
	NUM_BADDY_TYPES
}

public enum PrizeType {
	CHEST,
	BOX,
	KEY,
	NUM_PRIZE_TYPES
}

public enum RewardType {
	AP,
	HP,
	DEF,
	TRAP,
	NUM_REWARD_TYPES
}

public class Prize {
	protected int m_cash;
	protected PrizeType m_type;
	protected bool open;
	protected RewardType m_reward;
	
	public bool canCollect { get{ return (open || Gregor.shared.hasKeys); } }
	
	public Prize(PrizeType type) {
		open = true;
		m_type = type;
		if (m_type == PrizeType.CHEST)
			open = false;
		if (m_type != PrizeType.KEY) {
			m_reward = (RewardType)((int)(Random.value * (float)RewardType.NUM_REWARD_TYPES) % (int)RewardType.NUM_REWARD_TYPES);
		}
	}
	
	public void Collect() {
		if (m_type == PrizeType.KEY) {
			Gregor.shared.giveKey();
			Gregor.shared.currentRoom.clearRoom();
		}
		else if (open || Gregor.shared.spendKey()) {
			switch (m_reward) {
			case RewardType.AP:
				Gregor.shared.grantAP(1);
				break;
			case RewardType.DEF:
				Gregor.shared.grantDef(0.1f);
				break;
			case RewardType.HP:
				Gregor.shared.grantHealth(2);
				break;
			case RewardType.TRAP:
				Gregor.shared.grantHealth(-2);
				break;
			default:
				Debug.LogWarning(string.Format("Unimplemented reward {0}", m_reward));
				break;
			}
			Gregor.shared.currentRoom.clearRoom();
		}
		else {
			Debug.LogWarning("You Don't have a key to open that!");
		}
	}
		
	public string textureName() {
		switch (m_type) {
		case PrizeType.KEY:
			return "key";
		case PrizeType.CHEST:
			return "chest";
		case PrizeType.BOX:
			return "box";
		default:
			Debug.LogWarning("Invalid prize type");
			return "chest";
		}
	}
}

public class Monster {
	protected float healthPoints;
	protected float attackPower;
	protected float armor;
	protected float m_xp;
	protected BaddyType m_type;
	
	public float xp { get{ return m_xp; } }
	
	public Monster(BaddyType type) {
		m_type = type;
		switch (type) {
		case BaddyType.SLIME:
			healthPoints = 5.0f;
			attackPower = 1.0f;
			armor = 1.0f;
			m_xp = 1.0f;
			break;
		case BaddyType.GHOST:
			healthPoints = 10.0f;
			attackPower = 2.0f;
			armor = 1.1f;
			m_xp = 1.0f;
			break;
		case BaddyType.RODENT:
			healthPoints = 18.0f;
			attackPower = 5.0f;
			armor = 1.2f;
			m_xp = 2.0f;
			break;
		case BaddyType.DRAGON:
			healthPoints = 50.0f;
			attackPower = 10.0f;
			armor = 1.4f;
			m_xp = 3.0f;
			break;
		default:
			Debug.LogWarning("Invalid monster type");
			break;
		}
	}
		
	public void Hit(int hitPoints) {
		float damage = (float)hitPoints / armor;
		healthPoints -= damage;
		
		if (healthPoints <= 0.0f) {
			Debug.Log(string.Format("Monster of type {0} dead", m_type));
			Gregor.shared.grantXP((int)m_xp);
			Gregor.shared.currentRoom.clearRoom();
		}
	}
	
	public string textureName() {
		switch (m_type) {
		case BaddyType.SLIME:
			return string.Format("slime{0}", Gregor.shared.growthStage+1);
		case BaddyType.GHOST:
			return string.Format("ghost{0}", Gregor.shared.growthStage+1);
		case BaddyType.RODENT:
			return string.Format("rots{0}", Gregor.shared.growthStage+1);
		case BaddyType.DRAGON:
			return "bfd";
		default:
			Debug.LogWarning("Invalid monster type");
			return "slime1";
		}
	}
}

public class MapRoom {
	bool m_hasBaddy;
	bool m_hasPrize;
	int m_badGuy;
	int m_prize;
	Monster m_monster;
	Prize m_realPrize;
	
	public bool hasBaddy { get{ return m_hasBaddy; } }
	public bool hasPrize { get{ return m_hasPrize; } }
	public Monster monster { get{ return m_monster; } }
	public Prize prize { get{ return m_realPrize; } }
	
	public MapRoom(int weight) {
		m_hasBaddy = false;
		m_hasPrize = false;
		float maxWeight = (float)MapManager.shared.mapHeight + (float)MapManager.shared.mapWidth;
		
		float randomizedWeight = weight * (0.5f + (Random.value * 0.9f));
		if (randomizedWeight > maxWeight) randomizedWeight = maxWeight;
		
		float r = Random.value;
		if (r < 0.4f) {
			m_hasBaddy = true;
			m_badGuy = (int)(((float)randomizedWeight / maxWeight) * (float)BaddyType.NUM_BADDY_TYPES) % (int)BaddyType.NUM_BADDY_TYPES;
			m_monster = new Monster((BaddyType)m_badGuy);
		}
		else if (r < 0.8f) {
			m_hasPrize = true;
			m_prize = (int)(Random.value * (float)PrizeType.NUM_PRIZE_TYPES) % (int)PrizeType.NUM_PRIZE_TYPES;
			m_realPrize = new Prize((PrizeType)m_prize);
		}
		if (weight == 0) {
			m_hasBaddy = false;
			m_hasPrize = false;
		}
	}
	
	public void notify() {
		if (m_hasBaddy) {
			NotificationManager.shared.QueueNote(new Notification((BaddyType)m_badGuy));
			ButtonManager.shared.showRightButton("fight");
			ButtonManager.shared.showLeftButton("run");
			return;
		}
		if (m_hasPrize) {
			NotificationManager.shared.QueueNote(new Notification((PrizeType)m_prize));
			ButtonManager.shared.showRightButton("ok");
			ButtonManager.shared.hideButton(GameObject.FindGameObjectWithTag("LeftButton"));
			return;
		}
		if (!m_hasBaddy && !m_hasPrize) {
			NotificationManager.shared.DismissNote();
			ButtonManager.shared.hideButton(GameObject.FindGameObjectWithTag("RightButton"));
			ButtonManager.shared.hideButton(GameObject.FindGameObjectWithTag("LeftButton"));
			return;
		}
	}
	
	public void clearRoom() {
		m_hasBaddy = false;
		m_hasPrize = false;
		notify();
	}
}

public class MapManager : MonoBehaviour {
	int m_mapWidth = 30;
	int m_mapHeight = 30;
	ArrayList map;
	
	private static MapManager _instance;
    public static MapManager shared { get{ return _instance; } }
	
	public int mapWidth { get{ return m_mapWidth; } }
	public int mapHeight { get{ return m_mapHeight; } }
	
	void Awake() {
        _instance = this;
		
		map = new ArrayList(m_mapWidth);
		
		for (int i = 0; i < m_mapWidth; i++) {
			ArrayList column = new ArrayList(m_mapHeight);
			for (int j = 0; j < m_mapHeight; j++) {
				column.Add(new MapRoom(i+j));
			}
			map.Add(column);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public MapRoom MapRoomAtAddress(int x, int y) {
		x = x % m_mapWidth;
		y = y % m_mapHeight;
		
		//Debug.Log(string.Format("trying to access {0}, {1}", x, y));
		ArrayList column = (ArrayList)map[x];
		
		return (MapRoom)column[y];
	}
}
