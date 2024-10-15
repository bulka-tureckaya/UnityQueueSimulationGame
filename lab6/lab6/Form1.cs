using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;


namespace lab6
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            timer.SynchronizingObject = this;
            timer.Interval = 1000;
            timer.Elapsed += onTimer;
            timer.AutoReset = true;
        }

        System.Timers.Timer timer = new System.Timers.Timer();
        static Random rng = new Random();
        int minutes = 0;

        List<Queue> cashRegisters = new List<Queue>();
        List<Element> people = new List<Element>();
        List<Element> shop_people = new List<Element>();
        List<int> Estimations = new List<int>();

        int chanceOfBuyerApperience = 0, numberOfCashRegisters=0;

        

        private void Form1_Load(object sender, EventArgs e) {}

        private void clearCharts()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
        }

        private Queue SelectQueue(List<Queue> queues) // выбор подходящей очереди
        {
            int min = 9999, indexOfQueue = -1;
            for(int i = 0; i < queues.Count; i++)
            {
                if(queues[i].count < min)
                {
                    min = queues[i].count;
                    indexOfQueue = i;
                }
            }
            return queues[indexOfQueue];
        }


        private int Estimate(int time) // оценка от покупателя
        {

            if (time < 5) return 5;
            else if (time >= 5 && time < 10) return 4;
            else if (time >= 10 && time < 13) return 3;
            else if (time >= 13 && time < 17) return 2;
            else return 1;
        }

        public void onTimer(object obj, ElapsedEventArgs e) // работа за единицу времени
        {
            clearCharts();
            textBox4.Text= minutes.ToString(); // счетчик времени
            textBox5.Text = shop_people.Count.ToString(); // количество выбирающих покупки

            while (true) // пока не попадется человек с шансом > заданного, т е не попал в вероятность
            {
                if (rng.Next(1,101) <= chanceOfBuyerApperience)
                {
                    Element newElement = new Element(people.Count);
                    newElement.time_shop = rng.Next(5, 21); // время покупок
                    people.Add(newElement);
                    shop_people.Add(newElement);
                }
                else break;
            }

            for(int i = 0; i < shop_people.Count; i++) // человек занимает очередь, где меньше всего людей
            {
                shop_people[i].time_shop--;
                if(shop_people[i].time_shop <= 0)
                {
                    SelectQueue(cashRegisters).Push(shop_people[i]);
                    shop_people.Remove(shop_people[i]);
                }
            }

            for(int i = 0; i < cashRegisters.Count; i++) // выход из очередей
            {
                if (cashRegisters[i].time_pop <= 0 && cashRegisters[i].count != 0)
                {
                    Estimations.Add(Estimate(cashRegisters[i].Pop().time_cash));
                    textBox3.Text += $"Покупатель номер {Estimations.Count - 1} оценивает свое пребвыание в магазине на { Estimations[Estimations.Count - 1]}"+ Environment.NewLine;
                    cashRegisters[i].time_pop = rng.Next(1, 6); // добавляется время обслуживания для следующего человека
                }
                else if(cashRegisters[i].count != 0) cashRegisters[i].time_pop--;
            }

            for(int i = 0; i < cashRegisters.Count; i++)
            {
                cashRegisters[i].CurrentTime();
                chart1.Series[0].Points.AddXY(i, cashRegisters[i].count);
            }

            for (int i = 0; i < Estimations.Count; i++)
            {
                chart2.Series[0].Points.AddXY(i, Estimations[i]);
            }

            minutes++;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            minutes = 0;
            textBox3.Text = "";
            cashRegisters.Clear();
            people.Clear();
            shop_people.Clear();
            Estimations.Clear();

            chanceOfBuyerApperience = Convert.ToInt32(textBox1.Text);
            numberOfCashRegisters = Convert.ToInt32(textBox2.Text);

            for(int i = 0; i<numberOfCashRegisters; i++)
            {
                Queue cashRegisterQueue = new Queue();
                cashRegisterQueue.id = i;
                cashRegisterQueue.time_pop = rng.Next(1,6);
                cashRegisters.Add(cashRegisterQueue);
            }
            
            timer.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
    }
}
