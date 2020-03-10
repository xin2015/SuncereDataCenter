using SuncereDataCenter.Core.Sync;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuncereDataCenter.WindowsForms
{
    public partial class SuncereDataCenterForm : Form
    {
        DateTime startTime;
        DateTime endTime;

        public SuncereDataCenterForm()
        {
            InitializeComponent();
            comboBox1.Items.Add(typeof(CityHourlyAirQualitySync).Name);
            comboBox1.Items.Add(typeof(CityDailyAirQualitySync).Name);
            comboBox1.Items.Add(typeof(CityMonthlyAirQualitySync).Name);
            comboBox1.Items.Add(typeof(CityQuarterlyAirQualitySync).Name);
            comboBox1.Items.Add(typeof(CityYearlyAirQualitySync).Name);
        }

        private void GetTime()
        {
            if (!DateTime.TryParse(textBox1.Text, out startTime))
            {
                startTime = DateTime.Today;
            }
            if (!DateTime.TryParse(textBox2.Text, out endTime))
            {
                endTime = startTime;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GetTime();
                string syncClass = comboBox1.SelectedItem as string;
                using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
                {
                    Type[] types = Assembly.GetAssembly(typeof(ISync)).GetTypes();
                    Type syncType = types.FirstOrDefault(o => o.Name == syncClass);
                    ISync sync = (ISync)Activator.CreateInstance(syncType, entities);
                    sync.CheckQueue(startTime, endTime);
                    entities.SaveChanges();
                }
                textBox3.Text = string.Format("CheckQueue succeed.{0}", DateTime.Now);
            }
            catch (Exception ex)
            {
                textBox3.Text = string.Format("CheckQueue failed.{0} {1}", DateTime.Now, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string syncClass = comboBox1.SelectedItem as string;
                using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
                {
                    Type[] types = Assembly.GetAssembly(typeof(ISync)).GetTypes();
                    Type syncType = types.FirstOrDefault(o => o.Name == syncClass);
                    ISync sync = (ISync)Activator.CreateInstance(syncType, entities);
                    sync.Sync();
                    entities.SaveChanges();
                }
                textBox3.Text = string.Format("Sync succeed.{0}", DateTime.Now);
            }
            catch (Exception ex)
            {
                textBox3.Text = string.Format("Sync failed.{0} {1}", DateTime.Now, ex.Message);
            }
        }
    }
}
