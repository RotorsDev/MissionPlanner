using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MissionPlanner.MV04.Settings.SettingManager;

namespace MissionPlanner.MV04.Settings
{
    public partial class SettingForm : Form
    {
        internal Dictionary<Settings, object> returnData;

        internal SettingForm(Dictionary<Settings, object> formData)
        {
            InitializeComponent();

            // Set UI from formData
            comboBox_coordFormat.SelectedItem = formData[Settings.GPSType].ToString();
            comboBox_altFormat.SelectedItem = formData[Settings.AltFormat].ToString();
            comboBox_distFormat.SelectedItem = formData[Settings.DistFormat].ToString();
            comboBox_speedFormat.SelectedItem = formData[Settings.SpeedFormat].ToString();

            returnData = formData;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            // Save content and exit
            returnData[Settings.GPSType] = comboBox_coordFormat.SelectedItem.ToString();
            returnData[Settings.AltFormat] = comboBox_altFormat.SelectedItem.ToString();
            returnData[Settings.DistFormat] = comboBox_distFormat.SelectedItem.ToString();
            returnData[Settings.SpeedFormat] = comboBox_speedFormat.SelectedItem.ToString();

            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            // Just exit
            this.Close();
        }
    }
}
