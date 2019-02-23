namespace testJava.script.model {
    public class Config {
        public int srcCardWidth { get; set; }
        public int srcCardHeight { get; set; }
        public int srcCardWidthGap { get; set; }
        public int cardScale { get; set; }

        public int rowCardLimitNum = 14;
        public int removeCardNum = 3;

        public float cardWidth;
        public float cardHeight;
        public float cardWidthAndGap;
        public float scale;

        public float cardMoveSpeed = 0.1f;
    }
}