using UnityEngine;
using System.Collections;

public enum NoteType {
	ENCOUNTER,
	DISCOVERY,
	LEVEL,
	NUM_NOTE_TYPES
}

public class Notification {
	public Notification(NoteType p_noteType) {
		noteType = p_noteType;
	}
	public Notification(BaddyType p_monster) {
		noteType = NoteType.ENCOUNTER;
		monster = p_monster;
	}
	public Notification(PrizeType p_prize) {
		noteType = NoteType.DISCOVERY;
		prize = p_prize;
	}
	
	public NoteType noteType;
	public BaddyType monster;
	public PrizeType prize;
	public int number;
}

public class NotificationManager : MonoBehaviour {
	
	Notification nextNote;
	GameObject noteOverlay;
	GameObject note;
	
	private static NotificationManager _instance;
    public static NotificationManager shared { get{ return _instance; } }
	
	void Awake () {
		_instance = this;
		note = GameObject.FindGameObjectWithTag("Notification");
		noteOverlay = GameObject.FindGameObjectWithTag("NoteOverlay");
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void ShowNote()
    {
		note.transform.position = new Vector3(0.0f, -4.0f, -1.0f);
		if (nextNote.noteType == NoteType.DISCOVERY || nextNote.noteType == NoteType.ENCOUNTER)
			noteOverlay.transform.position = new Vector3(0.0f, -4.0f, -2.0f);
		else 
			noteOverlay.transform.position = new Vector3(1000.0f, -4.0f, -2.0f);
    }
    
    public void DismissNote()
    {
		note.transform.position = new Vector3(1000.0f, -4.0f, -1.0f);
		noteOverlay.transform.position = new Vector3(1000.0f, -4.0f, -2.0f);
    }
	
	public void QueueNote(Notification in_note) {
		nextNote = in_note;
		OTSprite noteSprite = OT.Sprite("Notification");
		OTSprite overlaySprite = OT.Sprite("NoteOverlay");
		
		noteSprite.image = (Texture)Resources.Load(string.Format("Notes/{0}", string.Format("note-{0}",textureFor(in_note.noteType))));
		if (in_note.noteType == NoteType.DISCOVERY)
			overlaySprite.image = (Texture)Resources.Load(string.Format("Notes/{0}", string.Format("note-{0}",textureFor(in_note.prize))));
		if (in_note.noteType == NoteType.ENCOUNTER)
			overlaySprite.image = (Texture)Resources.Load(string.Format("Notes/{0}", string.Format("note-{0}",textureFor(in_note.monster))));
		ShowNote();
	}
	
	string textureFor(NoteType nType) {
		if (nType == NoteType.ENCOUNTER)
			return "encounter";
		//if (nType == NoteType.DISCOVERY)
			return "discovery";
	}
	string textureFor(BaddyType bType) {
		if (bType == BaddyType.SLIME) return "slime";
		if (bType == BaddyType.RODENT) return "rots";
		if (bType == BaddyType.GHOST) return "ghost";
		if (bType == BaddyType.DRAGON) return "bfd";
		return "slime";
	}
	string textureFor(PrizeType pType) {
		if (pType == PrizeType.CHEST) return "chest";
		if (pType == PrizeType.BOX) return "box";
		if (pType == PrizeType.KEY) return "key";
		return "chest";
	}
}
