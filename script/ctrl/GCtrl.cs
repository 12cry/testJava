using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script {
	public class GCtrl : MonoBehaviour {

		public CardCtrl cardCtrlPrefab;
		public G g;
		void Start () {
			g = new G ();
			g.ctrl = this;
			g.init ();

			U.g = g;
			this.run ();
		}

		public void run () {
			if (g.over) {
				return;
			}
			g.deal ();

		}
		public void reset () {
			g.over = false;
			g.init ();
		}

		public void test1 () {
			print ("---test1");
		}

	}
}