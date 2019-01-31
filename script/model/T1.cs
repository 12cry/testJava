using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace testJava.script.model {
    public class T1<T> : List<T> {
        public static void f1 () {
            string path = "Assets/data";
            DirectoryInfo dir = new DirectoryInfo (path);
            FileInfo[] fis = dir.GetFiles ();
            foreach (FileInfo fi in fis) {
                string json = File.ReadAllText (fi.ToString ());
                Type t = Type.GetType ("testJava.script.model.card.GovernmentCard");
                Type t2 = Activator.CreateInstance (typeof (List<>).MakeGenericType (t)).GetType ();
                // dynamic t1 = JsonConvert.DeserializeObject (json, t2);
                // allCardList.AddRange (t1);
            }

        }

    }
}