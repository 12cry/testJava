using System.Collections.Generic;
using testCC.Assets.script;
using testJava.script.constant;
using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class StatisticUI {
        public StatisticUICtrl ctrl;

        public int food { get; set; }
        public int foodRaise { get; set; }
        public int capacity { get; set; }
        public int capacityRaise { get; set; }
        public int science { get; set; }
        public int scienceRaise { get; set; }
        public int culture { get; set; }
        public int cultureRaise { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }

        public void init () {
            Dictionary<string, int> dic = U.g.conf["statisticUI"];

            foreach (var d in dic) {
                this.GetType ().GetProperty (d.Key).SetValue (this, d.Value, null);
            }
            refresh ();
            // U.cpUI.warehouseUI.removeWarehouse (this.food);
            // U.cpUI.warehouseUI.removeWarehouse (this.capacity);

        }
        public void refresh () {
            string str = "{8}<sprite=0>  {9}<sprite=0>                   {0}<sprite=0>+{1}  {2}<sprite=1>+{3}  {4}<sprite=1>+{5}  {6}<sprite=1>+{7}  ";
            str = string.Format (str, new string[] {
                food.ToString (), foodRaise.ToString (), capacity.ToString (), capacityRaise.ToString (), science.ToString (), scienceRaise.ToString (), culture.ToString (), cultureRaise.ToString (), attack.ToString (), defense.ToString ()
            });

            ctrl.text.SetText (str);
        }

        public void evaluating () {
            this.updateFood (foodRaise);
            this.updateCapacity (capacityRaise);
            science += scienceRaise;
            culture += cultureRaise;
            refresh ();
        }
        public void reduce (Statistic statistic) {
            foodRaise -= statistic.foodRaise;
            capacityRaise -= statistic.capacityRaise;
            scienceRaise -= statistic.scienceRaise;
            cultureRaise -= statistic.cultureRaise;

            updateFood (-statistic.food);
            updateCapacity (-statistic.capacity);
            science -= statistic.science;
            culture -= statistic.culture;

            refresh ();
        }
        public void add (Statistic statistic) {
            foodRaise += statistic.foodRaise;
            capacityRaise += statistic.capacityRaise;
            scienceRaise += statistic.scienceRaise;
            cultureRaise += statistic.cultureRaise;

            updateFood (-statistic.food);
            updateCapacity (-statistic.capacity);
            science += statistic.science;
            culture += statistic.culture;

            refresh ();
        }
        public void computeMilitaryStatistic () {
            List<BuildingCard> cards = U.cpUI.getBuildingCards (CardType.MILITARY_BUILDING);
            foreach (var card in cards) {
                WorkerBuildingCard c = (WorkerBuildingCard) card;
                attack = c.workers.Count * c.buildIncome.attack;
                defense = c.workers.Count * c.buildIncome.defense;
                if (U.currentLeader == CardId.LEADER_MZD) {
                    if (c.id == CardId.WARRIOR) {
                        defense += c.workers.Count / 2;
                    }
                }
            }
            refresh ();
        }
        public bool enough (Statistic statistic) {
            if (statistic.science > this.science || statistic.capacity > this.capacity || statistic.food > this.food) {
                return false;
            }
            return true;
        }

        void updateFood (int value) {
            WarehouseUI warehouseUI = U.cpUI.warehouseUI;
            if (warehouseUI.warehouseNum < value) {
                value = warehouseUI.warehouseNum;
            }
            warehouseUI.updateWarehouse (-value);
            this.food += value;
        }

        void updateCapacity (int value) {
            WarehouseUI warehouseUI = U.cpUI.warehouseUI;
            if (warehouseUI.warehouseNum < value) {
                value = warehouseUI.warehouseNum;
            }
            warehouseUI.updateWarehouse (-value);

            this.capacity += value;
        }

    }
}