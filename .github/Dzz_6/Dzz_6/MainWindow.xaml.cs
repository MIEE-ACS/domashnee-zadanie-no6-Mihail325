using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dzz_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    class  Weapon 
        {
        public enum Type
        {
            сoldSteel,
            firearm,
            thermonuclearWeapon
        }
        public enum Subspecies
        {
            crossbow,
            autogun,
            ibm
        }
        public Type type;
        public Subspecies subspecies;

        protected static Random rand = new Random();
        virtual public int ECradius //радиус поражения
        {
           
            get;
        }
        virtual public int RateofFire //Скорострельность
        {
           
            get;
        }

        virtual public double RechargeRate //скорость перезарядки
        {
            get;
        }
        virtual public int SightingRange //прицельная дальность
        {
            
            get;
        }

        public override string ToString()
        {
            string type_name;
            string subspecies_name;
            switch (subspecies)
            {
                case Subspecies.crossbow:
                    subspecies_name = "Арбалет";
                    type = Type.сoldSteel;
                    type_name = "Холодное оружие";
                    break;
                case Subspecies.autogun:
                    subspecies_name = "Автомат";
                    type = Type.firearm;
                    type_name = "Огнестрельное оружие";
                    break;
                case Subspecies.ibm:
                    subspecies_name = "МБР";
                    type = Type.thermonuclearWeapon;
                    type_name = "Термояжерное оружие";
                    break;
                default:
                    type_name = "";
                    subspecies_name = "";
                    break;
            }
            return $"Тип оружия: {type_name}, вид оружия: {subspecies_name}, ";
        }
    }



    // переопределение типов
    class ColdSteel : Weapon
    {
        public override string ToString()
        {
            return base.ToString() + $"радиус поражения: точечные попадания, ";
        }
    }
    class Firearm : Weapon
    {
        public override string ToString()
        {
            return base.ToString() + $"радиус поражения: точечные попадания,";
        }

    }
    class ThermonuclearWeapon : Weapon
    {
        override public int RateofFire //Скорострельность
        {

            get 
            {
                return 1;
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"радиус поражения: {ECradius} км, скорострельность: возможен 1 выстрел, ";
        }
    }




    //Конкретнеые примеры
    class Crossbow : ColdSteel
    {
        int m_rateofire;
        int m_sightingrange;
        public Crossbow()
        {
            m_rateofire = rand.Next(4, 16);
            m_sightingrange = rand.Next(25,200);
        }
        public Crossbow(int rateofire, int sightingrange) 
        {
            subspecies = Weapon.Subspecies.crossbow;
            if (rateofire > 0 && rateofire <= 20)
                m_rateofire = rateofire;
            else
                throw new Exception("Такая скорострельность невозможна.\nПроверьте введёные значения");
            if (sightingrange > 0 && sightingrange < 200)
                m_sightingrange = sightingrange;
            else
                throw new Exception("Такая прицельная дальность невозможна.\nПроверьте введёные значения");
        }

        override public int RateofFire //Скорострельность
        {
            get
            {
                return m_rateofire;
            }
        }
        override public double RechargeRate  //скорость перезарядки
        {
            get
            {
                return 60 / m_rateofire;
            }
        }
        override public int SightingRange //прицельная дальность
        {
            get
            {
                return m_sightingrange;
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"скорострельнотсь: {RateofFire} выстр/мин, скорость перезарядки: {RechargeRate} сек., прицельная дальность: {SightingRange} м.";
        }
    }
    class Autogun : Firearm
    {
        double  m_rechargerate;
        int m_automatic_shot, m_sightingrange;
        public Autogun()
        {
            m_rechargerate = 2 + rand.NextDouble() * (6 - 2);
            m_sightingrange = rand.Next(400, 1000);
            m_automatic_shot = rand.Next(650, 1100);
        }
        public Autogun(double rechargerate, int automatic_shot, int sightingrange)
        {
            subspecies = Weapon.Subspecies.autogun;
            if (automatic_shot > 0 && automatic_shot < 2000)
                m_automatic_shot = automatic_shot;
            else
                throw new Exception("Такая скорострельность невозможна.\nПроверьте введёные значения");
            if (rechargerate > 0 && rechargerate < 60)//смена магазина как правило не занимает больше минуты
                m_rechargerate = rechargerate;
            else
                throw new Exception("Такая скорость перезарядки невозможна.\nПроверьте введёные значения");
            if (sightingrange > 0 && sightingrange < 2000)
                m_sightingrange = sightingrange;
            else
                throw new Exception("Такая прицельная дальность невозможна.\nПроверьте введёные значения");

        }
        override public int RateofFire  //Скорострельность
        {
            get
            {
                return m_automatic_shot;
            }
        }
        override public double RechargeRate  //скорость перезарядки
        {
            get
            {
                return m_rechargerate;
            }
        }
        override public int SightingRange //прицельная дальность
        {
            get
            {
                return m_sightingrange;
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"темп стрельбы: {RateofFire} выстр/мин, скорость перезарядки: {RechargeRate:0.00} сек., прицельная дальность: {SightingRange} м.";
        }
    }
    class IBM : ThermonuclearWeapon
    {
        double m_rechargerate;
        int m_sightingrange;
        int m_ecradius;
        public IBM()
        {
            m_rechargerate = rand.Next(4, 15);
            m_sightingrange = rand.Next(8000, 16000);
            m_ecradius = rand.Next(8, 100);
        }
        public IBM(double rechargerate, int sightingrange, int ecradius)
        {
            subspecies = Weapon.Subspecies.ibm;
            if (rechargerate > 0 && rechargerate <= 30)
                m_rechargerate = rechargerate;
            else
                throw new Exception("Такое время развёртывания невозможно.\nПроверьте введёные значения");
            if (sightingrange > 0)
                m_sightingrange = sightingrange;
            else
                throw new Exception("Такая прицельная дальность невозможна.\nПроверьте введёные значения");
            if (ecradius > 0 && ecradius <100)
                m_ecradius = ecradius;
            else
                throw new Exception("Такой радиус поражения невозможен.\nПроверьте введёные значения");
        }
        override public int ECradius //радиус поражения
        {
            get
            {
                return m_ecradius;
            }
        }
        override public double RechargeRate //время развёртывания
        {
            get
            {
                return m_rechargerate;
            }
        }
        override public int SightingRange //прицельная дальность
        {
            get
            {
                return m_sightingrange;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"время развёртывания {RechargeRate} мин., прицельная дальность: {SightingRange} км.";
        }
    }





    public partial class MainWindow : Window
    {
        List<Weapon> m_weapons = new List<Weapon>();
        public MainWindow()
        {
            InitializeComponent();

            m_weapons.Add(new Autogun() { subspecies = Weapon.Subspecies.autogun });
            m_weapons.Add(new Crossbow() { subspecies = Weapon.Subspecies.crossbow});
            m_weapons.Add(new IBM() { subspecies = Weapon.Subspecies.ibm });
            m_weapons.Add(new IBM() { subspecies = Weapon.Subspecies.ibm });
            m_weapons.Add(new IBM() { subspecies = Weapon.Subspecies.ibm });
            m_weapons.Add(new Crossbow() { subspecies = Weapon.Subspecies.crossbow });
            m_weapons.Add(new Autogun() { subspecies = Weapon.Subspecies.autogun });
            m_weapons.Add(new Crossbow() { subspecies = Weapon.Subspecies.crossbow });
            m_weapons.Add(new Autogun() { subspecies = Weapon.Subspecies.autogun });

            updateListBox(m_weapons);
        }

        void updateListBox(List<Weapon> weapons)
        {
            lbWeapons.Items.Clear();
            foreach (var weapon in weapons)
            {
                lbWeapons.Items.Add(weapon);
            }
        }

        private void tbCreat_Click(object sender, RoutedEventArgs e)
        {
            switch (cbSubspecies.SelectedIndex) 
            {
                case (int)Weapon.Subspecies.crossbow:
                    {
                        try
                        {
                            int rateofire = int.Parse(tbRateoffire.Text);
                            int sightingrange = int.Parse(tbSightingrange.Text);
                            Crossbow weapon = new Crossbow(rateofire, sightingrange);
                            m_weapons.Add(weapon);
                            updateListBox(m_weapons);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Проверьте введенные значения.\nВозможны только числа.", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                case (int)Weapon.Subspecies.autogun:
                    {
                        try
                        {
                            double rechargerate = double.Parse(tbRechargerate.Text);
                            int automatic_shot = int.Parse(tbRateoffire.Text);
                            int sightingrange = int.Parse(tbSightingrange.Text);
                            Autogun weapon = new Autogun(rechargerate, automatic_shot, sightingrange);
                            m_weapons.Add(weapon);
                            updateListBox(m_weapons);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Проверьте введенные значения.\nВозможны только числа.", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                case (int)Weapon.Subspecies.ibm:
                    {
                        try 
                        {
                            int ecradius = int.Parse(tbECradius.Text);
                            double rechargerate = int.Parse(tbRechargerate.Text);
                            int sightingrange = int.Parse(tbSightingrange.Text);
                            IBM weapon = new IBM(rechargerate, sightingrange, ecradius);
                            m_weapons.Add(weapon);
                            updateListBox(m_weapons);
                         }
                        catch (FormatException)
                        {
                            MessageBox.Show("Проверьте введенные значения.\nВозможны только числа.", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }

            }
        }


        private void bSortECradius_Click(object sender, RoutedEventArgs e)
        {
            var weapons = m_weapons.OrderByDescending(x => x.ECradius).ToList();
            updateListBox(weapons);
        }

        private void bFilter_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = (Weapon.Type)cbFilter.SelectedIndex;
            var weapons=m_weapons.Where(x => (x.type == selectedType)).ToList();
            updateListBox(weapons);
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            object[] itemsToRemove = new object[lbWeapons.SelectedItems.Count];
            lbWeapons.SelectedItems.CopyTo(itemsToRemove, 0);

            foreach (object item in itemsToRemove)
            {
                lbWeapons.Items.Remove(item);
            }
        }

        private void cbSubspecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSubspecies.SelectedIndex)
            {
                case (int)Weapon.Subspecies.crossbow:
                    {
                        tbRechargerate.Text = "Автоматическй расчёт";
                        tbECradius.Text = "Ввод данных не нужен";
                        tbECradius.IsEnabled = false;
                        tbRechargerate.IsEnabled = false;//блокируем потому что это значение расчитывается автоматически
                        tbRateoffire.IsEnabled = true;
                        tbRateoffire.Clear();
                        break;
                    }
                case (int)Weapon.Subspecies.autogun:
                    {
                        tbECradius.Text = "Ввод данных не нужен";
                        tbECradius.IsEnabled = false;
                        tbRechargerate.IsEnabled = true;
                        tbRateoffire.IsEnabled = true;
                        tbRateoffire.Clear();
                        tbRechargerate.Clear();
                        break;
                    }
                case (int)Weapon.Subspecies.ibm:
                    {
                        tbECradius.IsEnabled = true;
                        tbRechargerate.IsEnabled = true;
                        tbRateoffire.Text = "Возможен 1 выстрел";
                        tbECradius.Clear();
                        tbRechargerate.Clear();
                        tbRateoffire.IsEnabled = false;
                        break;
                    }

            }

        }


    }
}
