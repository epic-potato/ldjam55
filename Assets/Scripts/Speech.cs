using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum SpeechMode {
	Silent,
	Speaking,
	Holding,
};

public class Speech : MonoBehaviour {
	[SerializeField] bool isIntro;
	[SerializeField] bool isEnding;
	[SerializeField] GameObject choose;
	SpeechMode mode = SpeechMode.Silent;
	AudioSource audioSource;
	TMP_Text speech;
	Wait wait;
	string text;
	string saying;
	int sayingIdx;

	string[] sayings = new string[]{
		"Weathermen are quacks. No one can really predict the weather",
		"Ahh castles. I remember when they first invented castles. I always HATED THEM",
		"Do you think this blindfold makes my nose look big?",
		"Some necromancers rise from the grave. I’m more of a rizz from the grave kind of guy",
		"Every sausage you’ve ever eaten has come from somewhere",
		"I can smell what this place looks like",
		"Crazy? I was crazy once...",
		"I’m blind, throw me a bone will ya? ...oh wait",
		"Is that the key to my heart? Hehehehe",
		"Yes, yes, I know. Coming, coming!",
		"For a dog with no stomach, you sure eat a lot of bones",
		"I wonder if Mystic O’Malley’s wonder tonic would make my beard thicker...",
		"I’ve got a bone to pick with you. ...alright even I’m sorry about that one",
		"I’m allergic to arrows. And castles. And effort. And...why are we here again?",
		"Can’t you just give me a ride on your back?",
		"Back in my prime I could’ve summoned an entire undead army and taken over the world...what's with that look?",
		"The truth is, I only summon those I really like.",
		"Allodoxaphobia is the fear of other people’s opinions.",
		"It would take 19 minutes to fall to the center of the Earth. Don’t ask me how I know that",
		"Penicillin was first called “mold juice.”",
		"It’s impossible to hum while holding your nose. Believe me, I’ve tried.",
		"Animals can also be allergic to humans.  Not you Henry, of course",
		"All this furniture and I can’t sit.  Don’t ask.",
		"Why did the skeleton attend the party alone? because he had no body to go with",
		"Do they make undead dog food?",
		"I don’t even have legs under this cloak....just kidding! ...or am I?",
		"Once we get out of here I smell a profitable venture. Hypoallergenic pets for the whole family!",
		"Did you know that dogs sweat through their paws? Well at least living dogs do",
		"Your nose and ears never stop growing.  You’ll have to tie your ears up eventually, or you’ll trip!",
		"I wonder if I left the phylactery running...",
		"738 years old is quite young for a necromancer, you know",
		"Never borrow lotion from a lich",
		"How do you bark with no vocal chords?",
		"I could go for a nice cold ale right about now",
		"Did you hear about the skeleton that got caught in a snowstorm? Truly a bone chilling tale",
		"What’s a skeleton’s favorite food? Spare ribs!",
		"So a zombie, a vampire, and a werewolf walk into a bar...",
		"You know in most games I would be the main character...or at least the villain",
		"What’s a skeleton’s favorite instrument? A trom-BONE",
		"Not even a laugh?  I guess you don’t have a “funny bone” hahaha",
		"What did one skeleton say to the other skeleton? “You’re dead to me.",
		"What do skeletons hate the most about the wind? Nothing. It goes right through them",
		"Quite the conversationalist aren’t we?",
		"What kind of jokes do skeletons tell? Humerus ones.",
		"It’s always castles. Why is it always castles?",
	};

	string[] headSayings = new string[]{
		"What am I, a stool?",
		"Watch the beard!",
		"I make a much better lord of the undead than a ladder",
		"Ouch!",
		"Oof!",
		"I should have worn a helmet instead of this hood...",
	};

	string intro = "This is the story of a blind Necromancer and his faithful undead companion, Henry. The pair embark on a quest to find the Pillar of Sight in order to restore that which has been lost. However, there is always more to lose...";

	string ending = "You have reached the Pillar of Sight.\nYour efforts are commendable...\nGo forth and claim the power for your own. However...you must know.  In order to absorb the power of sight, you must give up something of equal value.  If you take the power for your own, you will lose your necromancy power, and with that, your faithful friend.What matters most to you?";

	// Start is called before the first frame update
	void Start() {
		speech = GetComponentInChildren<TextMeshProUGUI>();
		audioSource = GetComponent<AudioSource>();

		if (isIntro || isEnding) {
			gameObject.SetActive(false);
		} else {
			wait = new Wait(5);
		}
	}

	// Update is called once per frame
	void Update() {
		if (!wait.CheckFinished()) {
			return;
		}

		if (mode == SpeechMode.Silent) {
			int idx = Random.Range(0, sayings.Length - 1);
			saying = sayings[idx];
			if (isIntro) {
				saying = intro;
			}

			if (isEnding) {
				saying = ending;
			}

			mode = SpeechMode.Speaking;
			sayingIdx = 0;
			audioSource.Play(0);
		}

		if (mode == SpeechMode.Speaking && sayingIdx == saying.Length) {
			mode = SpeechMode.Holding;
			audioSource.Stop();
			wait = new Wait(3);
			return;
		}

		if (mode == SpeechMode.Holding) {
			if (isIntro) {
				SceneManager.LoadScene("Tutorial1");
				return;
			}

			if (isEnding) {
				choose.SetActive(true);
				return;
			}

			text = "";
			speech.SetText(text);
			mode = SpeechMode.Silent;
			wait = new Wait(Random.Range(5, 15));
		}

		text += saying[sayingIdx++];
		speech.SetText(text);
		wait = new Wait(0.1f);
	}
}
