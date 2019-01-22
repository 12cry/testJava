using UnityEngine;

namespace testJava.script.command {
    public class Command {
        public virtual void undo () {
            CommandCtrl.instant.addRedoCommand (this);
        }
        public virtual void redo () {
            CommandCtrl.instant.addUndoCommand (this);
        }

    }
}