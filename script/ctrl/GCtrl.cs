using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script {
	public class GCtrl : MonoBehaviour {

		public CardCtrl cardCtrlPrefab;
		public Queue<CardCtrl> civilCardCtrls;
		public CardCtrl[] rowCardCtrls;
		int rowCardLimitNum = 3;
		bool over = false;
		Tweener t1;

		void Start () {
			init ();
		}

		void init () {
			print ("---init");

			UICtrl.instance.hideView ();

			string json = File.ReadAllText ("./Assets/testJava/Resources/cardBuild.json", Encoding.UTF8);
			List<CardBuild> cardBuildList = JsonConvert.DeserializeObject<List<CardBuild>> (json);

			List<Card> cardList = new List<Card> ();
			for (int i = 0; i < cardBuildList.Count; i++) {
				cardList.Insert (Random.Range (i, i + 1), cardBuildList[i]);
			}

			civilCardCtrls = new Queue<CardCtrl> ();
			cardList.ForEach (card => {
				CardCtrl newCtrdCtrl = Instantiate<CardCtrl> (cardCtrlPrefab, cardCtrlPrefab.transform.parent);
				newCtrdCtrl.card = card;
				card.cardCtrl = newCtrdCtrl;
				civilCardCtrls.Enqueue (newCtrdCtrl);
			});

			rowCardCtrls = new CardCtrl[rowCardLimitNum];
			// run ();
		}

		CardCtrl getANewCard () {
			if (civilCardCtrls.Count == 0) {
				return null;
			}
			CardCtrl cardCtrl = civilCardCtrls.Dequeue ();
			Card card = cardCtrl.card;
			Text[] texts = cardCtrl.GetComponentsInChildren<Text> ();
			Dictionary<string, Text> textDic = texts.ToDictionary (key => key.name, text => text);
			textDic["cardName"].text = card.cardName;
			if (card is CardBuild) {
				CardBuild cb = (CardBuild) card;
				textDic["costScience"].text = cb.cost.science.ToString ();
			}
			return cardCtrl;
		}
		public void computeCurrentCards () {
			CardCtrl cardCtrl;
			int removeCardNum = 1;
			int index = 0;

			for (int i = 0; i < rowCardLimitNum; i++) {
				cardCtrl = rowCardCtrls[i];
				if (cardCtrl == null) {
					continue;
				}
				if (cardCtrl.card.taked) {
					rowCardCtrls[i] = null;
					continue;
				}
				if (i < removeCardNum) {
					Object.Destroy (cardCtrl.gameObject);
				} else {
					rowCardCtrls[index] = cardCtrl;
					rowCardCtrls[i] = null;
					index++;
				}
			}

			for (int i = index; i < rowCardLimitNum; i++) {
				cardCtrl = getANewCard ();
				if (cardCtrl == null) {
					over = true;
					break;
				}
				rowCardCtrls[i] = cardCtrl;
			}

		}
		public void showCurrentCards () {
			for (int i = 0; i < rowCardCtrls.Length; i++) {
				if (rowCardCtrls[i] == null) {
					break;
				}
				rowCardCtrls[i].card.actionable = false;
				rowCardCtrls[i].card.takeable = true;
				rowCardCtrls[i].transform.DOMove (new Vector3 (Utils.cardWidth / 2 + i * Utils.cardWidth, Screen.height - Utils.cardWidth / 2, 0), Utils.cardMoveSpeed);

				rowCardCtrls[i].card.takeCiv = 1 + i / 5;
			}
		}

		public void run () {
			print ("---run");
			// print (Screen.width);
			// print (Screen.height);

			if (over) {
				print ("---over");
				return;
			}
			computeCurrentCards ();
			showCurrentCards ();

		}
		public void reset () {
			print ("---reset");
			// t1.Rewind (false);

			// while (civilCardCtrls.Count != 0) {
			// 	print ("---civilCardCtrls");
			// 	CardCtrl cardCtrl = civilCardCtrls.Dequeue ();
			// 	Object.Destroy (cardCtrl.gameObject);
			// }

			// for (int i = 0; i < rowCardLimitNum; i++) {
			// 	if (rowCardCtrls[i] == null) {
			// 		continue;
			// 	}
			// 	Object.Destroy (rowCardCtrls[i].gameObject);
			// }
			over = false;
			init ();
		}

		public void test1 () {
			print ("---test1");
			init ();
		}

	}
}