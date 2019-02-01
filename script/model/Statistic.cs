namespace testJava.script.model {
    public class Statistic {

        public int food;
        public int foodRaise;
        public int capacity;
        public int capacityRaise;
        public int science;
        public int scienceRaise;
        public int culture;
        public int cultureRaise;
        public int attack;
        public int defense;

        public Statistic minus (Statistic statistic) {
            Statistic result = new Statistic ();
            result.science = this.science - statistic.science;
            result.capacity = this.capacity - statistic.capacity;

            return result;
        }
    }
}