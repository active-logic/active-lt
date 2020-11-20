namespace Active.Core{

    public static class Logger{

        public static Console console;
        public static TreeView treeView;

        public static void Log(object src, string file, string member, int line){
            console?.Log($"{file}::{member} [{line}]\n", src);
        }

        public static void RequestPaint(){
            treeView?.RequestPaint();
        }

        public interface Console  { void Log(string message, object src); }
        public interface TreeView { void RequestPaint(); }
        
    }

}
