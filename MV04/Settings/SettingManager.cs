using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissionPlanner.Utilities;
using OpenTK.Audio.OpenAL;

namespace MissionPlanner.MV04.Settings
{
    internal enum Settings
    {
        GPSType,
        AltFormat,
        DistFormat,
        SpeedFormat
    }

    internal class SettingManager
    {
        private string FileName = MissionPlanner.Utilities.Settings.GetUserDataDirectory() + "MV04_settings.json";
        internal Dictionary<Settings, object> SettingCollection;

        public SettingManager()
        {
            // Define settings and default values
            SettingCollection = new Dictionary<Settings, object>()
            {
                {Settings.GPSType, "WGS84"},
                {Settings.AltFormat, "m"},
                {Settings.DistFormat, "m"},
                {Settings.SpeedFormat, "mps"}
            };

            // Load saved values
            Load();
        }

        public void Save()
        {
            File.WriteAllText(FileName, SettingCollection.ToJSON()); // create or owerwrite
        }

        public void Load()
        {
            try
            {
                // Read JSON
                SettingCollection = File.ReadAllText(FileName).FromJSON<Dictionary<Settings, object>>();
            }
            catch (Exception)
            {
                // If unsuccessful, ignore and save the default values
                Save();
            }
        }

        public async void OpenDialog()
        {
            // Open form
            using (SettingForm form = new SettingForm(SettingCollection))
            {
                // Show (async) dialog
                DialogResult dr = await Task.Run(() => form.ShowDialog());

                if (dr == DialogResult.OK)
                {
                    // Set values
                    SettingCollection = form.returnData;

                    // Save values
                    Save();
                }
            }
        }

    }
}
