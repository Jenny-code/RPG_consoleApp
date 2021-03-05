using System;
using System.Collections.Generic;
using System.Linq;

namespace Jennifer_Unger_OOP_ASSIGNMENT
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StoryFunction();
        }
    }

    class Warrior
    {
        public string name { get; set; }
        public int attack { get; set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int defense { get; set; }

        public Warrior(string name, int attack, int maxHealth, int defense)
        {
            this.name = name;
            this.attack = attack;
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            this.defense = defense;
        }

        public void Attack(int attack)
        {
            currentHealth -= (attack - defense);
        }
    }
    class ArmourBag
    {
        public List<Armour> armourList = new List<Armour>();

        public ArmourBag()
        {
            armourList.Add(new Armour("Peasant clothes", 1));
        }

        public void addArmour(string name, int defense)
        {
            armourList.Add(new Armour(name, defense));
        }

        public Armour getArmour(int index)
        {
            return armourList.ElementAt(index);
        }

        public void ShowContents()
        {
            int index = 0;
            Console.WriteLine("\nArmour:\n");
            foreach (Armour armour in armourList)
            {
                Console.WriteLine(index + " - Armour name:" + armour.name + "  Armour defense:" + armour.defense);
                index++;
            }
        }
    }
    class WeaponBag
    {
        public List<Weapon> weaponList = new List<Weapon>();

        public WeaponBag()
        {
            weaponList.Add(new Weapon("Wooden Sword", 1));
        }

        public void addWeapon(string name, int strength)
        {
            weaponList.Add(new Weapon(name, strength));
        }

        public Weapon getWeapon(int index)
        {
            return weaponList.ElementAt(index);
        }

        public void ShowContents()
        {
            int index = 0;
            Console.WriteLine("\nWeapon:\n");
            foreach (Weapon weapon in weaponList)
            {
                Console.WriteLine(index + " - Weapon name:" + weapon.name + "  Weapon strength:" + weapon.strength);
                index++;
            }
        }
    }

    class Bestiary
    {
        public List<Monster> monsters = new List<Monster>();

        public Bestiary()
        {
            MoreMonsters("Bang", 1, 10, 2);
            MoreMonsters("Toog", 2, 15, 2);
            MoreMonsters("Rumf", 10, 30, 2);
            MoreMonsters("Blaft", 20, 40, 2);
        }
        public void MoreMonsters(string name, int strength, int maxHealth, int defense)
        {
            var monster = new Monster(name, strength, maxHealth, defense);
            monsters.Add(monster);
        }

        public Monster GetRandomMonster()
        {
            var random = new Random();
            Monster randomMonster = monsters[random.Next(0, 4)];
            return randomMonster;
        }

    }

    class Hero : Warrior
    {
        public Weapon weapon;
        public Armour armour;
        public WeaponBag weaponBag;
        public ArmourBag armourBag;

        public Hero(string name, int attack, int maxHealth, int defense) : base(name, attack, maxHealth, defense)
        {
            this.weaponBag = new WeaponBag();
            this.armourBag = new ArmourBag();

            this.weapon = this.weaponBag.getWeapon(0);
            this.armour = this.armourBag.getArmour(0);
        }


        public void PrintStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} stats:", this.name);
            Console.WriteLine("");
            Console.WriteLine("Attack value is: {0}", this.attack);
            Console.WriteLine("Max health value is: {0}", this.maxHealth);
            Console.WriteLine("Current health value is: {0}", this.currentHealth);
            Console.WriteLine("Currently equipped weapon " + this.weapon.name + " strength: " + this.weapon.strength);
            Console.WriteLine("Currently equipped armour " + this.armour.name + " defense: " + this.armour.defense);
            Console.ResetColor();
        }

        public int totalStrength()
        {
            return this.attack + this.weapon.strength;
        }
    }

    class Monster : Warrior
    {
        public Monster(string name, int attack, int maxHealth, int defense) : base(name, attack, maxHealth, defense)
        {
            this.name = name;
            this.attack = attack;
            this.maxHealth = maxHealth;
        }
    }

    class Weapon
    {
        public string name { get; set; }
        public int strength { get; set; }
        public Weapon(string name, int strength)
        {
            this.name = name;
            this.strength = strength;
        }
    }

    class Armour
    {
        public string name { get; set; }
        public int defense { get; set; }
        public Armour(string name, int defense)
        {
            this.name = name;
            this.defense = defense;
        }
    }

    class Fight
    {
        public Hero hero;
        public Monster monster;
        public Fight(Hero hero, Monster monster)
        {
            this.hero = hero;
            this.monster = monster;
        }

        public void StartFight()
        {
            while (this.ContinueFighting())
            {
                this.heroTurn();
                this.monsterTurn();
                this.printBattleStats();
            }
        }

        public void heroTurn()
        {
            if (this.hero.currentHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(hero.name + " attacks " + monster.name + " damage =  " + hero.totalStrength());
                Console.ResetColor();
                monster.Attack(hero.totalStrength());
            }
        }

        public void monsterTurn()
        {
            if (this.monster.currentHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(monster.name + " attacks " + hero.name + " damage = " + monster.attack);
                Console.ResetColor();
                hero.Attack(monster.attack);
            }
        }

        public bool ContinueFighting()
        {
            return ((this.hero.currentHealth > 0) && (this.monster.currentHealth > 0));
        }

        public void printBattleStats()
        {
            if (this.monster.currentHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You are the victor...for now.");
                Console.ResetColor();
            }
            if (this.hero.currentHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU LOSE, LOSER");
                Console.ResetColor();
            }
            if (this.ContinueFighting())
            {
                Console.WriteLine("Current Monster HP: " + this.monster.currentHealth + "/" + this.monster.maxHealth);
                Console.WriteLine("Current Hero HP: " + this.hero.currentHealth + "/" + this.hero.maxHealth);
            }
        }
    }

    class Game
    {
        public Hero hero;

        public void StoryFunction()
        {
            Console.WriteLine("Welcome\n");
            Console.WriteLine("What will you name your hero?\n");
            string heroName = Console.ReadLine();

            hero = new Hero(heroName, 8, 100, 5);
            Bestiary bestiary = new Bestiary();

            bool donePlaying = false;
            while (!donePlaying)
            {
                Console.WriteLine("\nMAIN\n \nOptions: \n1 - Print Stats \n2 - " +
                    "Show Inventory \n3 - Fight Monster \n4 - Exit game", heroName);
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        hero.PrintStats();
                        break;
                    case "2":
                        this.EnterInventory();
                        break;
                    case "3":
                        Fight fight = new Fight(hero, bestiary.GetRandomMonster());
                        fight.StartFight();
                        break;
                    case "4":
                        donePlaying = true;
                        break;
                }
            }
            Console.WriteLine("...goodbye");

            Console.ReadKey();
        }
        public void EnterInventory()
        {
            bool doneWithInventory = false;

            while (!doneWithInventory)
            {
                Console.WriteLine("\nINVENTORY\n \nOptions: \n1 - Weapon Bag Contents \n2 - " +
                    "Armour Bag Contents\n3 - Add Weapon To Bag \n4 - Add Armour To Bag\n5 - " +
                    "Equip Weapon \n6 - Equip Armour\n7 - Exit Inventory ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        hero.weaponBag.ShowContents();
                        break;
                    case "2":
                        hero.armourBag.ShowContents();
                        break;
                    case "3":
                        AddWeaponToBag();
                        break;
                    case "4":
                        AddArmourToBag();
                        break;
                    case "5":
                        hero.weaponBag.ShowContents();
                        EquipWeapon();
                        break;
                    case "6":
                        hero.armourBag.ShowContents();
                        EquipArmour();
                        break;
                    case "7":
                        doneWithInventory = true;
                        break;
                }
            }
        }

        public void AddWeaponToBag()
        {
            Console.WriteLine("What did you find?");
            string name = Console.ReadLine();
            Console.WriteLine("What is it's strength?");
            string strengthString = Console.ReadLine();
            int strength = Int32.Parse(strengthString);

            hero.weaponBag.addWeapon(name, strength);
        }
        public void AddArmourToBag()
        {
            Console.WriteLine("What did you find?");
            string name = Console.ReadLine();
            Console.WriteLine("What is it's defense?");
            string defenseString = Console.ReadLine();
            int defense = Int32.Parse(defenseString);

            hero.armourBag.addArmour(name, defense);
        }
        public void EquipWeapon()
        {
            Console.WriteLine("What number weapon will you be equipping?");
            int weaponIndex = Int32.Parse(Console.ReadLine());
            hero.weapon = hero.weaponBag.getWeapon(weaponIndex);
        }
        public void EquipArmour()
        {
            Console.WriteLine("What number armour will you be equipping?");
            int armourIndex = Int32.Parse(Console.ReadLine());
            hero.armour = hero.armourBag.getArmour(armourIndex);
        }
    }

}
