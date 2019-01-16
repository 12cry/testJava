namespace testCC.Assets.script.model {
    public class Cost {
        public int science;
        public int capacity;

        public Cost minus (Cost cost) {
            Cost result = new Cost ();
            result.science = this.science - cost.science;
            result.capacity = this.capacity - cost.capacity;

            return result;
        }
    }
}