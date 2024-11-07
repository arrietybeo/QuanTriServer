using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanTriServer
{
    public class Player
    {

        private long power;
        private string name;

        private int hp, maxHp, hpg;

        private int mp, maxMp, mpg;


        public Player(string name, int hp, int maxHp, int mp, int maxMp, long power)
        {
            this.name = name;
            this.hp = hp;
            this.maxHp = maxHp;
            this.mp = mp;
            this.maxMp = maxMp;
            this.power = power;
        }

        public int getHp()
        {
            return hp;
        }

        public void setHp(int hp)
        {
            this.hp = hp;
        }

        public int getMaxHp()
        {
            return maxHp;
        }

        public void setMaxHp(int maxHp)
        {
            this.maxHp = maxHp;
        }

        public int getMp()
        {
            return mp;
        }

        public void setMp(int mp)
        {
            this.mp = mp;
        }

        public int getMaxMp()
        {
            return maxMp;
        }

        public void setMaxMp(int maxMp)
        {
            this.maxMp = maxMp;
        }

        public long getPower()
        {
            return power;
        }

        public void setPower(long power)
        {
            this.power = power;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

    }
}
