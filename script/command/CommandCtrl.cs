using System.Collections.Generic;
using testCC.Assets.script;

namespace testJava.script.command {
    public class CommandCtrl {

        private CommandCtrl () { }
        private static CommandCtrl _instant = new CommandCtrl ();
        public static CommandCtrl instant {
            get {
                return _instant;
            }
        }

        public Stack<Command> undoCommands = new Stack<Command> ();
        public Stack<Command> redoCommands = new Stack<Command> ();

        public void addCommand (Command command) {
            redoCommands.Clear ();
            undoCommands.Push (command);
            // U.cpUI.actionUI.setUndoButtonAble (true);
            // U.cpUI.actionUI.setRedoButtonAble (false);
        }
        public void addUndoCommand (Command command) {
            undoCommands.Push (command);
            // U.cpUI.actionUI.setUndoButtonAble (true);
            // U.cpUI.actionUI.setRedoButtonAble (redoCommands.Count != 0);
        }

        public void addRedoCommand (Command command) {
            redoCommands.Push (command);
            // U.cpUI.actionUI.setUndoButtonAble (undoCommands.Count != 0);
            // U.cpUI.actionUI.setRedoButtonAble (true);
        }

    }
}